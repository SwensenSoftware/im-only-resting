using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Http;
using System.IO;
using Swensen.Utils;
using Swensen.Ior.Core;
using Swensen.Ior.Properties;
using NLog;
using System.Xml;
using System.Xml.Serialization;

//examples response types:
//xml: http://www.w3schools.com/xml/note.asp
//json: http://api.geonames.org/postalCodeLookupJSON?postalcode=6600&country=AT&username=demo
namespace Swensen.Ior.Forms
{
    public partial class MainForm : Form {
        #region LOG_NOTIFICATIONS
        public delegate void MessageLoggedEventHandler(LogLevel level, string message);
        private static event MessageLoggedEventHandler MessageLogged;

        public static void LogMessageNotification(string level, string message) {
            if(MessageLogged != null)
                MessageLogged(LogLevel.FromString(level), message);
        }

        private static readonly object logStatsSyncRoot = new object();
        private Dictionary<LogLevel, int> logStats = new Dictionary<LogLevel,int>();

        private void updateLogStats(LogLevel level) {
            if(!this.IsHandleCreated)
                return;

            lock (logStatsSyncRoot) {
                if(logStats.ContainsKey(level))
                    logStats[level] = logStats[level] + 1;
                else
                    logStats[level] = 1;
            }

            MethodInvoker update = (MethodInvoker) delegate {
                lock (logStatsSyncRoot) {
                    var labelText = 
                        logStats
                        .OrderByDescending(kvp => kvp.Key.Ordinal)
                        .Select(kvp => string.Format("{0}({1})", kvp.Key.ToString().ToLower(), kvp.Value))
                        .Join(" ");
                    lblLogNotifications.Text = labelText;
                    lblLogNotifications.Visible = true;
                }
            };

            this.BeginInvoke(update);
        }

        private void resetLogStats() {
            lock (logStatsSyncRoot) {
                logStats.Clear();
            }

            this.Invoke((MethodInvoker) delegate {
                lblLogNotifications.Text = "";
                lblLogNotifications.Visible = false;
            });
        }
        #endregion

        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private static Bitmap cameraIconAsBitmap = Properties.Resources.Camera.ToBitmap();

        /// <summary>
        /// We dynamically create and add a webbrowser object each time due to quirky behavior otherwise (only works first Navigate in some cases).
        /// </summary>
        WebBrowser wbResponseBody;

        private string lastOpenedRequestFilePath = null;

        /// <summary>
        /// Track whether there have been any modifications to the request since opening a request file.
        /// </summary>
        private bool isLastOpenedRequestFileDirty = false;

        /// <summary>
        /// Hold on to the last responseModel so we can do things like Export Response Body and toggle between response body output modes.
        /// </summary>
        private ResponseModel lastResponseModel = ResponseModel.Empty; //just to avoid np exceptions.

        /// <summary>
        /// Handel on the currently executing async request, allows us to cancel. null if no currently executing request.
        /// </summary>
        private CancellationTokenSource requestAsyncHandle = null;

        private HistoryList<RequestResponseSnapshot> snapshots;

        private string launchFilePath;
        private readonly string programName = "I'm Only Resting";

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string launchFilePath) : this() { 
            this.launchFilePath = launchFilePath;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {                           
            try {
                MessageLogged += (level, message) => updateLogStats(level);

                Settings.Default.UpgradeAndSaveIfNeeded();
                HistorySettings.Default.UpgradeAndSaveIfNeeded();
                
                txtRequestHeaders.FindReplace.Window.Text = "Find / Replace - Request Headers";
                txtRequestHeaders.Margins[0].Width = 12;
                
                txtResponseText.FindReplace.Window.Text = "Find - Response Text";
                txtResponseText.DisableReplace();
                txtResponseText.Margins[0].Width = 32;

                initTxtRequestBody();
                rebuildWebBrowser();
                bindResponseBodyOutputs();
                bindHttpMethods();
                bindSettings();
                bindHistorySettings();
                setUpFileDialogs();
                ActiveControl = txtRequestUrl;
            } catch(Exception ex) { //n.b. exceptions swallowed during main load since gui message pump not started
                log.Fatal("Exception in main, shutting down", ex);
                showError("Error", "Unknown error, shutting down: " + Environment.NewLine + Environment.NewLine + ex.ToString());
                this.Close();
            }
        }

        private void bindHistorySettings() {
            try {
                var rs = new XmlReaderSettings {IgnoreWhitespace = false};
                var xml = HistorySettings.Default.HistoryList;
                using (var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(xml))) 
                using (var reader = XmlReader.Create(stream, rs)) { 
                    XmlSerializer serializer = new XmlSerializer(typeof(List<RequestViewModel>), new XmlRootAttribute("History"));
                    var results = (List<RequestViewModel>)serializer.Deserialize(reader);
                    results.Reverse<RequestViewModel>().Each(rvm => {
                        var snapshot = new RequestResponseSnapshot {
                            request = rvm,
                            response = ResponseModel.Empty
                        };
                        snapshots.Add(snapshot);
                    });
                    bindSnapshots();
                }
            } catch(Exception ex) {
                log.Error(ex);
                var historySettings = Properties.HistorySettings.Default;
                historySettings.HistoryList = "<History></History>";
                historySettings.Save();
                showError("Error - " + programName, "Error loading History due to possible data corruption. Your History has been cleared in order to ensure normal application function.");
            }
        }

        private void bindSettings() {
            var settings = Settings.Default;

            log.Debug("binding form size to settings with height={0}, width={1}...", settings.FormWidth, settings.FormHeight);
            this.Width = settings.FormWidth;
            this.Height = settings.FormHeight;
            log.Debug("form size is now bound with height={0}, width={1}", this.Width, this.Height);

            splitterMain.Orientation = Settings.Default.SplitterOrientation;
            updateSplitterDistance(); //must come after width and height and orientation updates
            rbGrpResponseBodyOutputs.First(x => (ResponseBodyOutput)x.Tag == Settings.Default.ResponseBodyOutput).Checked = true;

            if (!launchFilePath.IsBlank())
                openRequestFile(launchFilePath);
            else if (!settings.DefaultRequestFilePath.IsBlank())
                openRequestFile(settings.DefaultRequestFilePath);

            snapshots = new HistoryList<RequestResponseSnapshot>(Settings.Default.MaxSnapshots);
        }

        private void setIsLastOpenedRequestFileDirtyToTrue() {
            isLastOpenedRequestFileDirty = true;
            if(!this.Text.StartsWith("*"))
                this.Text = "*" + this.Text;
        }

        private void setUpFileDialogs() {
            var initialSavedRequestsDir = Settings.Default.SaveRequestFileDialogFolder;
            Directory.CreateDirectory(initialSavedRequestsDir);
            requestSaveFileDialog.InitialDirectory = initialSavedRequestsDir;
            requestOpenFileDialog.InitialDirectory = initialSavedRequestsDir;

            var initalExportedResponsesDir = Settings.Default.ExportResponseFileDialogFolder;
            Directory.CreateDirectory(initalExportedResponsesDir);
            responseBodySaveFileDialog.InitialDirectory = initalExportedResponsesDir; 
        }

        private IEnumerable<RadioButton> rbGrpHttpMethods { get { return grpHttpMethod.Controls.OfType<RadioButton>(); } }

        private void bindHttpMethods() {
            rbHttpDelete.Tag = HttpMethod.Delete;
            rbHttpGet.Tag = HttpMethod.Get;
            rbHttpHead.Tag = HttpMethod.Head;
            rbHttpOptions.Tag = HttpMethod.Options;
            rbHttpTrace.Tag = HttpMethod.Trace;
            rbHttpPost.Tag = HttpMethod.Post;
            rbHttpPut.Tag = HttpMethod.Put;
            
            foreach (var rb in rbGrpHttpMethods) {
                rb.CheckedChanged += new EventHandler(rbGrpHttp_CheckedChanged);
            }
        }

        void rbGrpHttp_CheckedChanged(object sender, EventArgs e) {
            var rb = sender as RadioButton;
            if (rb.Checked) {
                setIsLastOpenedRequestFileDirtyToTrue();
            }
        }

        private IEnumerable<RadioButton> rbGrpResponseBodyOutputs { get { return grpResponseBodyOutput.Controls.OfType<RadioButton>(); } }

        private void bindResponseBodyOutputs() {
            rbResponseBodyOutputRaw.Tag = ResponseBodyOutput.Raw;
            rbResponseBodyOutputPretty.Tag = ResponseBodyOutput.Pretty;
            rbResponseBodyOutputBrowser.Tag = ResponseBodyOutput.Rendered;

            foreach (var rb in rbGrpResponseBodyOutputs) {
                rb.CheckedChanged += new EventHandler(rbGrpResponseBodyOutput_CheckedChanged);
            }
        }

        void rbGrpResponseBodyOutput_CheckedChanged(object sender, EventArgs e) {
            var rb = sender as RadioButton;
            if (rb.Checked) {
                Settings.Default.ResponseBodyOutput = (ResponseBodyOutput)rb.Tag;
                Settings.Default.Save();
                updateResponseBodyOutput();
            }
        }

        private void updateResponseBodyOutput() {
            switch (Settings.Default.ResponseBodyOutput) {
                case ResponseBodyOutput.Raw:
                    txtResponseText.Visible = true;
                    wbResponseBody.Visible = false;
                    txtResponseText.Text = lastResponseModel.Content;
                    break;
                case ResponseBodyOutput.Pretty:
                    txtResponseText.Visible = true;
                    wbResponseBody.Visible = false;
                    txtResponseText.Text = lastResponseModel.PrettyPrintedContent;
                    var mtc = lastResponseModel.ContentType.MediaTypeCategory;
                    txtResponseText.ConfigurationManager.Language =
                        mtc == IorMediaTypeCategory.Html ? "html" :
                        mtc == IorMediaTypeCategory.Xml ? "xml" :
                        (mtc == IorMediaTypeCategory.Json || mtc == IorMediaTypeCategory.Javascript) ? "js" :
                        "";
                    txtResponseText.ConfigurationManager.Configure();
                    break;
                case ResponseBodyOutput.Rendered:
                    rebuildWebBrowser();
                    wbResponseBody.Visible = true;
                    txtResponseText.Visible = false;

                    if ((lastResponseModel.ContentType.MediaTypeCategory == IorMediaTypeCategory.Xml || 
                        lastResponseModel.ContentType.MediaTypeCategory == IorMediaTypeCategory.Application) && 
                        lastResponseModel.ContentBytes != null && 
                        lastResponseModel.ContentBytes.Length > 0) {
                        var fullFileName = lastResponseModel.TemporaryFile;
                        wbResponseBody.Navigate(fullFileName);
                    } else {
                        wbResponseBody.Navigate("about:blank");
                        HtmlDocument doc = wbResponseBody.Document.OpenNew(true);

                        switch (lastResponseModel.ContentType.MediaTypeCategory) {
                            case IorMediaTypeCategory.Html:
                                doc.Write(lastResponseModel.Content);
                                break;
                            default:
                                doc.Write(String.Format("<html><body><pre>{0}</pre></body></html>", System.Net.WebUtility.HtmlEncode(lastResponseModel.PrettyPrintedContent)));
                                break;
                        }
                    }

                    wbResponseBody.Refresh();
                    break;                    
            }
        }

        private RequestViewModel buildRequestViewModel() {
            //build the request view
            var checkedHttpMethod = rbGrpHttpMethods.First(x => x.Checked);
            return new RequestViewModel() {
                Url = txtRequestUrl.Text,
                Method = ((HttpMethod)checkedHttpMethod.Tag).Method,
                Headers = txtRequestHeaders.Text.Split(new [] {txtRequestHeaders.EndOfLine.EolString}, StringSplitOptions.RemoveEmptyEntries),
                Body = txtRequestBody.Text
            };
        }

        private void cancelAsyncRequest() {
            if (requestAsyncHandle != null) {
                requestAsyncHandle.Cancel();
                requestAsyncHandle = null;
                bind(ResponseModel.Empty);
            }
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            resetLogStats();

            //build the request view
            var requestVm = buildRequestViewModel();

            //attempt to build the request model from the request view: if we fail, show an error messages, else continue and make the request.
            RequestModel requestModel = null;
            var validationErrors = RequestModel.TryCreate(requestVm, out requestModel);
            if (validationErrors.Count > 0) {
                //todo: move this to common dialog functionality
                //add bullets if more than one
                if (validationErrors.Count > 1) {
                    var buffer = new byte[] { 149 };
                    string bullet = Encoding.GetEncoding(1252).GetString(buffer);
                    validationErrors = validationErrors.Select(x => bullet + " " + x).ToList();
                }

                showWarning("Request Validation Errors", String.Join(Environment.NewLine, validationErrors));
            } else {
                cancelAsyncRequest();
                //clear response view and show loading message status
                bind(ResponseModel.Loading);
                grpResponse.Update();

                try {
                    var client = new IorClient(Settings.Default.DefaultRequestContentType, Settings.Default.ProxyServer, Settings.Default.IncludeBomInUtf8RequestContent, Settings.Default.FollowRedirects);
                    this.requestAsyncHandle = client.ExecuteAsync(requestModel, responseModel =>
                        this.Invoke((MethodInvoker)delegate {
                            this.requestAsyncHandle = null;
                            //bind the response view
                            bind(responseModel);
                            addRequestResponseSnapshot(requestVm, responseModel);
                            grpResponse.Update();
                        })
                    );
                } catch (Exception ex) {
                    log.Error("There was an error executing the request", ex);
                    showError("Error", "There was an error executing the request: " + Environment.NewLine + Environment.NewLine + ex.ToString());

                    //reset the response view (i.e. roll-back loading message")
                    bind(ResponseModel.Empty);
                    grpResponse.Update();
                }
            }
        }

        private void addRequestResponseSnapshot(RequestViewModel requestVm, ResponseModel responseModel) {
            //update the model
            snapshots.Add(new RequestResponseSnapshot() { request = requestVm, response = responseModel });

            //update the view
            bindSnapshots();
        }

        private void bindSnapshots() {
            snapshotsToolStripMenuItem.DropDownItems.Clear();
            if (snapshots.Count > 0) {
                snapshotsToolStripMenuItem.Enabled = true;
                foreach (var snapshot in snapshots) {
                    var mi = snapshotsToolStripMenuItem.DropDownItems.Add(snapshot.request.Url.ToString());
                    mi.ToolTipText = snapshot.response.Start.ToString() + " - Status: " + snapshot.response.Status;
                    if(snapshot.response != ResponseModel.Empty)
                        mi.Image = cameraIconAsBitmap;

                    var freshSnapshotPointer = snapshot; //otherwise the closure captures the single snapshot pointer, which always ends up being the last item
                    mi.Click += (sender, e) => bind(freshSnapshotPointer);
                }
                snapshotsToolStripMenuItem.DropDownItems.Add("-");
                var miClear = snapshotsToolStripMenuItem.DropDownItems.Add("Clear");
                miClear.Click += (sender, e) => clearSnapshots();

            } else {
                snapshotsToolStripMenuItem.Enabled = false;
            }
        }

        private void clearSnapshots() {
            var historySettings =  Properties.HistorySettings.Default;
            historySettings.HistoryList = "<History></History>";
            historySettings.Save();
            snapshots.Clear();
            bindSnapshots();
        }

        private void bind(ResponseModel responseVm) {
            this.lastResponseModel = responseVm;

            lblResponseStatusValue.Text = responseVm.Status;
            lnkResponseStatusInfo.Visible = !responseVm.ErrorMessage.IsBlank();
            lnkCancelRequest.Visible = responseVm.Status == ResponseModel.Loading.Status;

            txtResponseHeaders.Text = responseVm.Headers;
            var elapsedMs = responseVm.ElapsedMilliseconds;
            lblResponseTimeValue.Text = elapsedMs == 0 ? "" : elapsedMs + " ms";
            
            updateResponseBodyOutput();
        }

        private void bind(RequestViewModel requestVm) {
            txtRequestUrl.Text = requestVm.Url;
            
            var method = requestVm.Method;
            (rbGrpHttpMethods.FirstOrDefault(x => ((HttpMethod) x.Tag).Method == method) ?? rbHttpGet).Checked = true;

            txtRequestHeaders.Text = (requestVm.Headers ?? new string[0]).Join(txtRequestHeaders.EndOfLine.EolString);
            txtRequestBody.Text = requestVm.Body;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            using(var box = new AboutBox()) 
            {
                box.StartPosition = FormStartPosition.CenterParent;
                box.ShowDialog(this);
            }
        }

        private void save(string fileName) {
            if (fileName == null) {
                requestSaveFileDialog.FileName = FilePathFormatter.Format(lastOpenedRequestFilePath, FilePathFormat.Short);
                setUpFileDialogs();
                if (requestSaveFileDialog.ShowDialog() == DialogResult.OK) {
                    fileName = requestSaveFileDialog.FileName;
                    Settings.Default.SaveRequestFileDialogFolder = FilePathFormatter.Format(fileName, FilePathFormat.FullDir);
                } else {
                    return;
                }
            }

            if (fileName.ToUpper() == Settings.Default.DefaultRequestFilePath.ToUpper() && 
                DialogResult.No == showChallenge("Overwrite Default Request File", "Are you sure you want to overwrite the default request file?")) {
                return;                
            }

            var requestVm = buildRequestViewModel();
            requestVm.Save(fileName);
            updateLastOpenedRequestFile(fileName);
        }

        private void updateLastOpenedRequestFile(string fileName) {
            this.lastOpenedRequestFilePath = fileName;
            this.isLastOpenedRequestFileDirty = false;
            this.Text = 
                fileName == null ? 
                programName :
                FilePathFormatter.Format(fileName, FilePathFormat.ShortFileFullDir) + " - " + programName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            save(lastOpenedRequestFilePath);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            save(null);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            requestOpenFileDialog.FileName = null;
            setUpFileDialogs();
            if (requestOpenFileDialog.ShowDialog() == DialogResult.OK) {
                var fileName = requestOpenFileDialog.FileName;
                openRequestFile(fileName);
                Settings.Default.SaveRequestFileDialogFolder = FilePathFormatter.Format(fileName, FilePathFormat.FullDir);
            }
        }

        private void openRequestFile(string fileName) {
            resetLogStats();
            RequestViewModel requestVm;
            try {
                requestVm = RequestViewModel.Load(fileName);
            } catch(Exception ex) {
                log.Warn("Error opening request file", ex);
                showWarning("File Open Error", "Error opening request file");
                return;
            }
            bind(ResponseModel.Empty); // clear the response.
            bind(requestVm);
            updateLastOpenedRequestFile(fileName);
        }

        private void exportResponseBodyToolStripMenuItem_Click(object sender, EventArgs e) {
            responseBodySaveFileDialog.FileName = null;
            
            //set filter based on inferred content type
            string filter = "All files|*.*";
            if (!lastResponseModel.ContentFileExtension.IsBlank()) {
                string ctExt = lastResponseModel.ContentFileExtension;
                filter = string.Format("{0}|*.{0}|{1}", ctExt, filter);
            }
            responseBodySaveFileDialog.Filter = filter;
            responseBodySaveFileDialog.FilterIndex = 1;
            
            if (responseBodySaveFileDialog.ShowDialog() == DialogResult.OK) {
                File.WriteAllBytes(responseBodySaveFileDialog.FileName, lastResponseModel.ContentBytes);
                Settings.Default.ExportResponseFileDialogFolder = responseBodySaveFileDialog.InitialDirectory;
            }
        }

        private void showError(string title, string text) {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showWarning(string title, string text) {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void showInfo(string title, string text) {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DialogResult showChallenge(string title, string text) {
            return MessageBox.Show(this, text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private DialogResult showYesNoCancel(string title, string text) {
            return MessageBox.Show(this, text, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private void txtRequestBody_TextChanged(object sender, EventArgs e) {
            setIsLastOpenedRequestFileDirtyToTrue();
        }

        private void txtRequestHeaders_TextChanged(object sender, EventArgs e) {
            setIsLastOpenedRequestFileDirtyToTrue();
        }

        //scintilla does not support link clicked event
        //private void txtResponseText_LinkClicked(object sender, LinkClickedEventArgs e) {
        //    System.Diagnostics.Process.Start(e.LinkText);
        //}

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            saveToolStripMenuItem.Enabled = this.isLastOpenedRequestFileDirty;
            exportResponseBodyToolStripMenuItem.Enabled = !(lastResponseModel.ContentBytes == null || lastResponseModel.ContentBytes.Length == 0);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            persistGuiSettings();
            using(var options = new SettingsDialog()) {
                if (DialogResult.OK == options.ShowDialog(this)) {
                    snapshots.MaxHistory = Settings.Default.MaxSnapshots;
                    bindSnapshots();
                }
            }
        }

        private void updateSplitterDistance() {
            var pct = (Settings.Default.SplitterDistancePercent / 100.0);
            log.Debug("updating splitter distance with percent={0}, orientation={3}, and ClientSize dimensions={1},{2}", pct, this.ClientSize.Width, this.ClientSize.Height, splitterMain.Orientation);
            splitterMain.SplitterDistance =
                (int)Math.Round(pct * (splitterMain.Orientation == Orientation.Vertical ? this.ClientSize.Width : this.ClientSize.Height));
        }

        private void persistGuiSettings() {
            //gets wacky if we allow min or max
            if (this.WindowState == FormWindowState.Normal) {
                Settings.Default.FormWidth = (ushort)this.Width;
                Settings.Default.FormHeight = (ushort)this.Height;
            }
            
            //splitter percent
            var pct = Math.Round((splitterMain.SplitterDistance / (double)(splitterMain.Orientation == Orientation.Vertical ? this.ClientSize.Width : this.ClientSize.Height)) * 100);
            Settings.Default.SplitterDistancePercent = (ushort)pct;

            //directories are set at time of dialog OK closing

            Settings.Default.Save();
        }

        private void persistHistorySettings() {
            var historySettings = Properties.HistorySettings.Default;
            var rvms = snapshots.Select(snapshot => snapshot.request).ToList();
            var ws = new XmlWriterSettings {NewLineHandling = NewLineHandling.Entitize};
            using (var output = new StringWriter())
            using (var writer = XmlWriter.Create(output, ws)) {
                XmlSerializer serializer = new XmlSerializer(typeof(List<RequestViewModel>), new XmlRootAttribute("History"));
                serializer.Serialize(writer, rvms);
                var xml = output.ToString();
                historySettings.HistoryList = xml;
                historySettings.Save();
            }
        }

        /// <summary>
        /// Prompt user for save if needed (i.e. user request operation which would
        /// lose changes)
        /// </summary>
        /// <returns>false indicates user cancel</returns>
        private bool promptForSaveIfNeeded() {
            if (this.isLastOpenedRequestFileDirty) {
                var msg = lastOpenedRequestFilePath.IsBlank() ? 
                            "Save changes to new request?" : 
                            String.Format("Save changes to {0}?", lastOpenedRequestFilePath);

                var result = showYesNoCancel("Save Changes", msg);
                if (result == DialogResult.Yes) {
                    save(lastOpenedRequestFilePath);
                } else if (result == DialogResult.Cancel) {
                    return false;
                } //else is No; follow through to the end
            }
            return true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!promptForSaveIfNeeded()) {
                e.Cancel = true;
                return;
            }

            try {
                persistGuiSettings();
                persistHistorySettings();
            } catch(Exception ex) { 
                log.Error(ex);
                //intentional swallow, it is non-critical to persist settings
            }
        }

        //hack!
        private void rebuildWebBrowser() {
            if (wbResponseBody != null) {
                pnlResponseContent.Controls.Remove(wbResponseBody);
                wbResponseBody.Dispose();
            }

            wbResponseBody = new WebBrowser();
            wbResponseBody.ScriptErrorsSuppressed = true;
            wbResponseBody.AllowNavigation = false;
            wbResponseBody.AllowWebBrowserDrop = false;
            wbResponseBody.Location = new System.Drawing.Point(2, 300);
            wbResponseBody.MinimumSize = new System.Drawing.Size(20, 20);
            wbResponseBody.Name = "wbResponseBody";
            wbResponseBody.Size = new System.Drawing.Size(398, 208);
            wbResponseBody.TabIndex = 14;
            wbResponseBody.Visible = true;
            wbResponseBody.Dock = DockStyle.Fill;

            pnlResponseContent.Controls.Add(wbResponseBody);
        }                                                                                      

        private void splitterMain_DoubleClick(object sender, EventArgs e) {
            persistGuiSettings();
            
            Settings.Default.SplitterOrientation = splitterMain.Orientation == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical;

            this.SuspendDrawing(() => {
                splitterMain.Orientation = Settings.Default.SplitterOrientation;
                updateSplitterDistance();
            });

            Settings.Default.Save();
        }

        private void lnkResponseStatusInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            showInfo("Response Error", lastResponseModel.ErrorMessage);
        }

        private void lnkCancelRequest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            cancelAsyncRequest();
        }

        private void initTxtRequestBody() {
            txtRequestBody.FindReplace.Window.Text = "Find / Replace - Request Body";
            txtRequestBody.Margins[0].Width = 22;

            //note: style 32 is default style

            /*ns.StyleSetFore(1, ScintillaNET.Utilities.ColorToRgb(Color.Black));
            ns.StyleSetFont(1, "Monospace");
            ns.StyleSetSize(1, 12);
            ns.setst*/

            var cm = txtRequestBody.ContextMenu;
            cm.MenuItems.Add("-");
            Action<ScintillaNET.Scintilla, IorMediaTypeCategory> format = (tb, hmtc) => {
                if (tb.Selection.Length > 0)
                    tb.Selection.Text = IorContentType.GetPrettyPrintedContent(hmtc, tb.Selection.Text);
                else
                    tb.Text = IorContentType.GetPrettyPrintedContent(hmtc, tb.Text);
            };

            var miFx = new MenuItem("Format XML", (s, ea) => { resetLogStats(); format(txtRequestBody, IorMediaTypeCategory.Xml); });
            miFx.Shortcut = Shortcut.CtrlShiftX;
            miFx.ShowShortcut = true;
            cm.MenuItems.Add(miFx);

            var miFj = new MenuItem("Format JSON", (s, ea) => { resetLogStats(); format(txtRequestBody, IorMediaTypeCategory.Json); });
            miFj.Shortcut = Shortcut.CtrlShiftJ;
            miFj.ShowShortcut = true;
            cm.MenuItems.Add(miFj);
        }

        private void bind(RequestResponseSnapshot snapshot) {
            if (snapshot != null) {
                bind(snapshot.request);
                bind(snapshot.response);
                txtRequestUrl.Text = snapshot.request.Url;
            }
        }

        private void txtRequestUrl_TextChanged(object sender, EventArgs e) {
            setIsLastOpenedRequestFileDirtyToTrue();
        }

        private void showLogViewer() {
            using(var box = new LogViewer())
            {
                box.StartPosition = FormStartPosition.CenterParent;
                box.ShowDialog(this);
            }
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e) {
            showLogViewer();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!promptForSaveIfNeeded())
                return;

            resetLogStats();
            bind(new RequestViewModel());
            bind(ResponseModel.Empty);
            updateLastOpenedRequestFile(null);
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!promptForSaveIfNeeded())
                return;

            var appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            System.Diagnostics.Process.Start(appPath);
        }

        private void lblLogNotifications_Click(object sender, EventArgs e) {
            resetLogStats();
            showLogViewer();
        }
    }
}
