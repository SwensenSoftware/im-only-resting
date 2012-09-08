using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using System.Net;
using System.Xml.Linq;
using System.IO;
using Swensen.RestSharpGui.Properties;

//examples response types:
//xml: http://www.w3schools.com/xml/note.asp
//json: http://api.geonames.org/postalCodeLookupJSON?postalcode=6600&country=AT&username=demo
namespace Swensen.RestSharpGui
{
    public partial class MainForm : Form
    {
        WebBrowser wbResponseBody;

        private string lastOpenedRequestFileName = null;
        private string lastOpenedRequestShortFileName {
            get {
                if (String.IsNullOrEmpty(lastOpenedRequestFileName))
                    return null;
                else {
                    var parts = lastOpenedRequestFileName.Split(Path.DirectorySeparatorChar);
                    return parts[parts.Length - 1];
                }
            }
        }

        private bool isLastOpenedRequestFileDirty = false;

        private ResponseViewModel lastResponseViewModel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try {
                rebuildWebBrowser();

                lastResponseViewModel = ResponseViewModel.Empty; //just to avoid np exceptions.
                bindResponseBodyOutputs();
                bindHttpMethods();
                bindRuntimeSettings();
                bindStartupSettings();
                setUpFileDialogs();
                ActiveControl = txtUrl;
            } catch(Exception ex) { //n.b. exceptions swallowed during main load since gui message pump not started
                showError("Error", "Unknown error, shutting down: " + Environment.NewLine + Environment.NewLine + ex.ToString());
                this.Close();
            }
        }

        private void bindRuntimeSettings() {
            var settings = Settings.Default;

            this.Width = settings.FormWidth;
            this.Height = settings.FormHeight;
            
            splitterMain.Orientation = Settings.Default.SplitterOrientation;
            updateSplitterDistance(); //must come after width and height and orientation updates
            rbGrpResponseBodyOutputs.First(x => (ResponseBodyOutput)x.Tag == Settings.Default.ResponseBodyOutput).Checked = true;
        }

        private void bindStartupSettings() {
            var settings = Settings.Default;
            if (!String.IsNullOrWhiteSpace(settings.DefaultRequestFilePath))
                openRequestFile(settings.DefaultRequestFilePath);
        }

        private void setIsLastOpenedRequestFileDirtyToTrue() {
            isLastOpenedRequestFileDirty = true;
            if(!this.Text.StartsWith("*"))
                this.Text = "*" + this.Text;
        }

        //todo: persist directory restore upon app start.
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
                    rtResponseText.Text = lastResponseViewModel.Content;
                    break;
                case ResponseBodyOutput.Pretty:
                    rtResponseText.Visible = true;
                    wbResponseBody.Visible = false;
                    rtResponseText.Text = lastResponseViewModel.PrettyPrintedContent;
                    break;
                case ResponseBodyOutput.Rendered:
                    rebuildWebBrowser();
                    wbResponseBody.Visible = true;
                    rtResponseText.Visible = false;

                    if ((lastResponseViewModel.ContentType.MediaTypeCategory == HttpMediaTypeCategory.Xml || lastResponseViewModel.ContentType.MediaTypeCategory == HttpMediaTypeCategory.Application) && lastResponseViewModel.ContentBytes != null && lastResponseViewModel.ContentBytes.Length > 0) {
                        var fullFileName = lastResponseViewModel.TemporaryFile;
                        wbResponseBody.Navigate(fullFileName);
                    } else {
                        wbResponseBody.Navigate("about:blank");
                        HtmlDocument doc = wbResponseBody.Document.OpenNew(true);

                        switch (lastResponseViewModel.ContentType.MediaTypeCategory) {
                            case HttpMediaTypeCategory.Html:
                                doc.Write(lastResponseViewModel.Content);
                                break;
                            default:
                                doc.Write(String.Format("<html><body><pre>{0}</pre></body></html>", RestSharp.Extensions.StringExtensions.HtmlEncode(lastResponseViewModel.PrettyPrintedContent)));
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
                Url = txtUrl.Text,
                Method = (Method)checkedHttpMethod.Tag,
                Headers = txtRequestHeaders.Lines.ToArray(),
                Body = txtRequestBody.Text
            };
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            //build the request view
            var requestVm = buildRequestViewModel();

            //attempt to build the request model from the request view: if we fail, show an error messages, else continue and make the request.
            RequestModel requestModel = null;
            var validationErrors = RequestModel.TryCreate(requestVm, out requestModel);
            if (validationErrors.Count > 0)
                showError("Request Validation Errors", String.Join(Environment.NewLine, validationErrors));
            else {
                //clear response view and show loading message status
                bind(ResponseViewModel.Loading);
                grpResponse.Update();

                //execute the request and get the response
                var restRequest = requestModel.ToRestRequest(Settings.Default.DefaultRequestContentType);
                var client = new RestClient();
                if(!String.IsNullOrWhiteSpace(Settings.Default.ProxyServer))
                    client.Proxy = new WebProxy(Settings.Default.ProxyServer, false); //make second arg a config option.

                var start = DateTime.Now;
                var restResponse = client.Execute(restRequest);
                var end = DateTime.Now;
                var responseVm = new ResponseViewModel(restResponse, start, end);

                //bind the response view
                bind(responseVm);
                grpResponse.Update();
            }
        }

        private void bind(ResponseViewModel responseVm) {
            this.lastResponseViewModel = responseVm;

            lblResponseStatusValue.Text = responseVm.Status;
            lnkResponseStatusInfo.Visible = !String.IsNullOrWhiteSpace(responseVm.ErrorMessage);

            txtResponseHeaders.Text = responseVm.Headers;
            lblResponseTimeValue.Text = responseVm.ElapsedTime;
            
            updateResponseBodyOutput();
        }

        private void bind(RequestViewModel requestVm) {
            txtUrl.Text = requestVm.Url;
            
            var method = requestVm.Method;
            rbGrpHttpMethods.First(x => ((Method) x.Tag) == method).Checked = true;

            txtRequestHeaders.Lines = (requestVm.Headers ?? new string[0]).ToArray();
            txtRequestBody.Text = requestVm.Body;
        }

        private void btnClearRequest_Click(object sender, EventArgs e)
        {
            setIsLastOpenedRequestFileDirtyToTrue();
            bind(new RequestViewModel());
            bind(ResponseViewModel.Empty);
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
                requestSaveFileDialog.FileName = lastOpenedRequestShortFileName;
                setUpFileDialogs();
                if (requestSaveFileDialog.ShowDialog() == DialogResult.OK) {
                    fileName = requestSaveFileDialog.FileName;
                    Settings.Default.SaveRequestFileDialogFolder = requestOpenFileDialog.InitialDirectory;
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
            this.lastOpenedRequestFileName = fileName;
            this.isLastOpenedRequestFileDirty = false;
            this.Text = fileName + " - RestSharp GUI";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            save(lastOpenedRequestFileName);
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
                Settings.Default.SaveRequestFileDialogFolder = requestOpenFileDialog.InitialDirectory;
            }
        }

        private void openRequestFile(string fileName) {
            RequestViewModel requestVm;
            try {
                requestVm = RequestViewModel.Open(fileName);
            } catch {
                showError("File Open Error", "Error opening request file");
                return;
            }
            bind(ResponseViewModel.Empty); // clear the response.
            bind(requestVm);
            updateLastOpenedRequestFile(fileName);
        }

        private void exportResponseBodyToolStripMenuItem_Click(object sender, EventArgs e) {
            responseBodySaveFileDialog.FileName = null;
            
            //set filter based on inferred content type
            string filter = "All files|*.*";
            if (!String.IsNullOrWhiteSpace(lastResponseViewModel.ContentFileExtension)) {
                string ctExt = lastResponseViewModel.ContentFileExtension;
                filter = string.Format("{0}|*.{0}|{1}", ctExt, filter);
            }
            responseBodySaveFileDialog.Filter = filter;
            responseBodySaveFileDialog.FilterIndex = 1;
            
            if (responseBodySaveFileDialog.ShowDialog() == DialogResult.OK) {
                File.WriteAllBytes(responseBodySaveFileDialog.FileName, lastResponseViewModel.ContentBytes);
                Settings.Default.ExportResponseFileDialogFolder = responseBodySaveFileDialog.InitialDirectory;
            }
        }

        private void showError(string title, string text) {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private DialogResult showChallenge(string title, string text) {
            return MessageBox.Show(this, text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private DialogResult showYesNoCancel(string title, string text) {
            return MessageBox.Show(this, text, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private void txtUrl_TextChanged(object sender, EventArgs e) {
            setIsLastOpenedRequestFileDirtyToTrue();
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
            exportResponseBodyToolStripMenuItem.Enabled = !(lastResponseViewModel.ContentBytes == null || lastResponseViewModel.ContentBytes.Length == 0);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
            persistGuiSettings();
            using(var options = new OptionsDialog()) {
                if(DialogResult.OK == options.ShowDialog(this))
                    bindRuntimeSettings();
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
                var msg = String.IsNullOrWhiteSpace(lastOpenedRequestFileName) ? 
                            "Save changes to new request?" : 
                            String.Format("Save changes to {0}?", lastOpenedRequestFileName);

                var result = showYesNoCancel("Save Changes", msg);
                if (result == DialogResult.Yes) {
                    save(lastOpenedRequestFileName);
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
            splitterMain.Orientation = Settings.Default.SplitterOrientation;
            updateSplitterDistance();

            Settings.Default.Save();
        }

        //http://schotime.net/blog/index.php/2008/03/12/select-all-ctrla-for-textbox/
        private void txt_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A)) {
                (sender as TextBox).SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void lnkResponseStatusInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            showError("Response Error", lastResponseViewModel.ErrorMessage);
        }
    }
}
