using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using System.Xml.Linq;
using System.IO;
using Swensen.Utils;
using Swensen.Ior.Core;
using Swensen.Ior.Properties;

//examples response types:
//xml: http://www.w3schools.com/xml/note.asp
//json: http://api.geonames.org/postalCodeLookupJSON?postalcode=6600&country=AT&username=demo
namespace Swensen.Ior.Forms
{
    public partial class MainForm : Form
    {
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
        private RestRequestAsyncHandle requestAsyncHandle = null;

        private HistoryList<RequestResponseHistoryItem> snapshots;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {                           
            try {
                Settings.Default.UpgradeAndSaveIfNeeded();
                initTxtRequestBody();
                rebuildWebBrowser();
                bindResponseBodyOutputs();
                bindHttpMethods();
                bindSettings();
                setUpFileDialogs();
                ActiveControl = txtRequestUrl;
            } catch(Exception ex) { //n.b. exceptions swallowed during main load since gui message pump not started
                showError("Error", "Unknown error, shutting down: " + Environment.NewLine + Environment.NewLine + ex.ToString());
                this.Close();
            }
        }

        private void bindSettings() {
            var settings = Settings.Default;

            this.Width = settings.FormWidth;
            this.Height = settings.FormHeight;

            splitterMain.Orientation = Settings.Default.SplitterOrientation;
            updateSplitterDistance(); //must come after width and height and orientation updates
            rbGrpResponseBodyOutputs.First(x => (ResponseBodyOutput)x.Tag == Settings.Default.ResponseBodyOutput).Checked = true;

            if (!settings.DefaultRequestFilePath.IsBlank())
                openRequestFile(settings.DefaultRequestFilePath);

            snapshots = new HistoryList<RequestResponseHistoryItem>(Settings.Default.MaxSnapshots);
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
            rbHttpDelete.Tag = Method.DELETE;
            rbHttpGet.Tag = Method.GET;
            rbHttpHead.Tag = Method.HEAD;
            rbHttpOptions.Tag = Method.OPTIONS;
            rbHttpPatch.Tag = Method.PATCH;
            rbHttpPost.Tag = Method.POST;
            rbHttpPut.Tag = Method.PUT;
            
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
                    rtResponseText.Visible = true;
                    wbResponseBody.Visible = false;
                    rtResponseText.Text = lastResponseModel.Content;
                    break;
                case ResponseBodyOutput.Pretty:
                    rtResponseText.Visible = true;
                    wbResponseBody.Visible = false;
                    rtResponseText.Text = lastResponseModel.PrettyPrintedContent;
                    break;
                case ResponseBodyOutput.Rendered:
                    rebuildWebBrowser();
                    wbResponseBody.Visible = true;
                    rtResponseText.Visible = false;

                    if ((lastResponseModel.ContentType.MediaTypeCategory == HttpMediaTypeCategory.Xml || 
                        lastResponseModel.ContentType.MediaTypeCategory == HttpMediaTypeCategory.Application) && 
                        lastResponseModel.ContentBytes != null && 
                        lastResponseModel.ContentBytes.Length > 0) {
                        var fullFileName = lastResponseModel.TemporaryFile;
                        wbResponseBody.Navigate(fullFileName);
                    } else {
                        wbResponseBody.Navigate("about:blank");
                        HtmlDocument doc = wbResponseBody.Document.OpenNew(true);

                        switch (lastResponseModel.ContentType.MediaTypeCategory) {
                            case HttpMediaTypeCategory.Html:
                                doc.Write(lastResponseModel.Content);
                                break;
                            default:
                                doc.Write(String.Format("<html><body><pre>{0}</pre></body></html>", RestSharp.Extensions.StringExtensions.HtmlEncode(lastResponseModel.PrettyPrintedContent)));
                                break;
                        }
                    }

                    wbResponseBody.Refresh();
                    break;                    
            }
        }

        private RequestViewModel buildRequestViewModel() {
            //build the request view
            var checkedHttpMethod = rbGrpHttpMethods.Where(x => x.Checked).First();
            return new RequestViewModel() {
                Url = txtRequestUrl.Text,
                Method = (Method)checkedHttpMethod.Tag,
                Headers = txtRequestHeaders.Lines.ToArray(),
                Body = txtRequestBody.Text
            };
        }

        private void cancelAsyncRequest() {
            if (requestAsyncHandle != null) {
                requestAsyncHandle.Abort();
                requestAsyncHandle = null;
                bind(ResponseModel.Empty);
            }
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            //build the request view
            var requestVm = buildRequestViewModel();

            //attempt to build the request model from the request view: if we fail, show an error messages, else continue and make the request.
            RequestModel requestModel = null;
            var validationErrors = RequestModel.TryCreate(requestVm, out requestModel);
            if (validationErrors.Count > 0)
                showWarning("Request Validation Errors", String.Join(Environment.NewLine, validationErrors));
            else {
                cancelAsyncRequest();
                //clear response view and show loading message status
                bind(ResponseModel.Loading);
                grpResponse.Update();

                var client = new HttpClient(Settings.Default.DefaultRequestContentType, Settings.Default.ProxyServer);
                this.requestAsyncHandle = client.ExecuteAsync(requestModel, responseModel => {
                    this.Invoke((MethodInvoker) delegate {
                        this.requestAsyncHandle = null;
                        //bind the response view
                        bind(responseModel);
                        addRequestResponseHistoryItem(requestVm, responseModel);                        
                        grpResponse.Update();
                    });
                });
            }
        }

        private void addRequestResponseHistoryItem(RequestViewModel requestVm, ResponseModel responseModel) {
            //update the model
            snapshots.Add(new RequestResponseHistoryItem() { request = requestVm, response = responseModel });

            //update the view
            bindSnapshots();
        }

        private void bindSnapshots() {
            snapshotsToolStripMenuItem.DropDownItems.Clear();
            if (snapshots.Count > 0) {
                snapshotsToolStripMenuItem.Enabled = true;
                foreach (var historyItem in snapshots) {
                    var mi = snapshotsToolStripMenuItem.DropDownItems.Add(historyItem.request.Url.ToString());
                    mi.ToolTipText = historyItem.response.Start.ToString() + " - Status: " + historyItem.response.Status;
                    var freshHistoryItemPointer = historyItem; //otherwise the closure captures the single historyItem pointer, which always ends up being the last item
                    mi.Click += (sender, e) => {
                        bind(freshHistoryItemPointer);
                    };
                }
            } else {
                snapshotsToolStripMenuItem.Enabled = false;
            }
        }

        private void bind(ResponseModel responseVm) {
            this.lastResponseModel = responseVm;

            lblResponseStatusValue.Text = responseVm.Status;
            lnkResponseStatusInfo.Visible = !responseVm.ErrorMessage.IsBlank();
            lnkCancelRequest.Visible = responseVm.Status == ResponseModel.Loading.Status;

            txtResponseHeaders.Text = responseVm.Headers;
            lblResponseTimeValue.Text = responseVm.ElapsedTime;
            
            updateResponseBodyOutput();
        }

        private void bind(RequestViewModel requestVm) {
            txtRequestUrl.Text = requestVm.Url;
            
            var method = requestVm.Method;
            rbGrpHttpMethods.First(x => ((Method) x.Tag) == method).Checked = true;

            txtRequestHeaders.Lines = (requestVm.Headers ?? new string[0]).ToArray();
            txtRequestBody.Text = requestVm.Body;
        }

        private void btnClearRequest_Click(object sender, EventArgs e)
        {
            setIsLastOpenedRequestFileDirtyToTrue();
            bind(new RequestViewModel());
            bind(ResponseModel.Empty);
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
            this.Text = FilePathFormatter.Format(fileName, FilePathFormat.ShortFileFullDir) + " - I'm Only Resting";
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
            RequestViewModel requestVm;
            try {
                requestVm = RequestViewModel.Open(fileName);
            } catch {
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

        private void rtResponseText_LinkClicked(object sender, LinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(e.LinkText);
        }

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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.isLastOpenedRequestFileDirty) {
                var msg = lastOpenedRequestFilePath.IsBlank() ? 
                            "Save changes to new request?" : 
                            String.Format("Save changes to {0}?", lastOpenedRequestFilePath);

                var result = showYesNoCancel("Save Changes", msg);
                if (result == DialogResult.Yes) {
                    save(lastOpenedRequestFilePath);
                } else if (result == DialogResult.Cancel) {
                    e.Cancel = true;
                    return;
                } //else is No; follow through to the end
            }

            //swallow
            try {
                persistGuiSettings();
            } catch { }
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
            var cm = txtRequestBody.ContextMenu;
            cm.MenuItems.Add("-");
            Action<TextBox, HttpMediaTypeCategory> format = (tb, hmtc) => {
                if (tb.SelectionLength > 0)
                    tb.SelectedText = HttpContentType.GetPrettyPrintedContent(hmtc, tb.SelectedText);
                else
                    tb.Text = HttpContentType.GetPrettyPrintedContent(hmtc, tb.Text);
            };

            {
                var miFx = new MenuItem("Format XML", (s, ea) => format(txtRequestBody, HttpMediaTypeCategory.Xml));
                miFx.Shortcut = Shortcut.CtrlShiftX;
                miFx.ShowShortcut = true;
                cm.MenuItems.Add(miFx);
            } 
            {
                var miFj = new MenuItem("Format JSON", (s, ea) => format(txtRequestBody, HttpMediaTypeCategory.Json));
                miFj.Shortcut = Shortcut.CtrlShiftJ;
                miFj.ShowShortcut = true;
                cm.MenuItems.Add(miFj);
            }
        }

        private void bind(RequestResponseHistoryItem historyItem) {
            if (historyItem != null) {
                bind(historyItem.request);
                bind(historyItem.response);
                txtRequestUrl.Text = historyItem.request.Url;
            }
        }

        private void txtRequestUrl_TextChanged(object sender, EventArgs e) {
            setIsLastOpenedRequestFileDirtyToTrue();
        }
    }
}
