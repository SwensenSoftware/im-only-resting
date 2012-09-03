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
        private string lastOpenedRequestFile = null;
        private bool isLastOpenedRequestFileDirty = false;

        private ResponseViewModel lastResponseViewModel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try {
                wbResponseBody.ScriptErrorsSuppressed = true;
                wbResponseBody.Navigate("about:none"); //load the initial document

                lastResponseViewModel = new ResponseViewModel(); //just to avoid np exceptions.
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
            setCheckedResponseBodyOutputRb();
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
                rb.Click += new EventHandler(rbGrpHttp_Click);
            }
        }

        void rbGrpHttp_Click(object sender, EventArgs e) {
            setIsLastOpenedRequestFileDirtyToTrue();
        }

        private IEnumerable<RadioButton> rbGrpResponseBodyOutputs { get { return grpResponseBodyOutput.Controls.OfType<RadioButton>(); } }

        private void bindResponseBodyOutputs() {
            rbResponseBodyOutputRaw.Tag = ResponseBodyOutput.Raw;
            rbResponseBodyOutputPretty.Tag = ResponseBodyOutput.Pretty;
            rbResponseBodyOutputBrowser.Tag = ResponseBodyOutput.Browser;

            foreach (var rb in rbGrpResponseBodyOutputs) {
                rb.Click += new EventHandler(rbGrpResponseBodyOutput_Click);
            }
        }

        void rbGrpResponseBodyOutput_Click(object sender, EventArgs e) {
            Settings.Default.ResponseBodyOutput = (ResponseBodyOutput)((RadioButton)sender).Tag;
            Settings.Default.Save();
            updateResponseBodyOutputDisplay();
            updateResponseBodyOutput(lastResponseViewModel);
        }

        private void setCheckedResponseBodyOutputRb() {
            rbGrpResponseBodyOutputs.First(x => (ResponseBodyOutput)x.Tag == Settings.Default.ResponseBodyOutput).Checked = true;
            updateResponseBodyOutputDisplay();
            updateResponseBodyOutput(lastResponseViewModel);
        }

        private void updateResponseBodyOutputDisplay() {
            switch (Settings.Default.ResponseBodyOutput) {
                case ResponseBodyOutput.Raw:
                    rtResponseText.Visible = true;
                    rtResponseText.Dock = DockStyle.Fill;
                    wbResponseBody.Visible = false;
                    wbResponseBody.Dock = DockStyle.None;
                    break;
                case ResponseBodyOutput.Pretty:
                    rtResponseText.Visible = true;
                    rtResponseText.Dock = DockStyle.Fill;
                    wbResponseBody.Visible = false;
                    wbResponseBody.Dock = DockStyle.None;
                    break;
                case ResponseBodyOutput.Browser:
                    wbResponseBody.Visible = true;
                    wbResponseBody.Dock = DockStyle.Fill;
                    rtResponseText.Visible = false;
                    rtResponseText.Dock = DockStyle.None;
                    break;
            }
        }

        private void updateResponseBodyOutput(ResponseViewModel responseVm) {
            switch (Settings.Default.ResponseBodyOutput) {
                case ResponseBodyOutput.Raw:
                    rtResponseText.Text = responseVm.Content;
                    break;
                case ResponseBodyOutput.Pretty:
                    rtResponseText.Text = responseVm.PrettyPrintedContent;
                    break;
                case ResponseBodyOutput.Browser:
                    //http://stackoverflow.com/a/8592117/236255
                    HtmlDocument doc = wbResponseBody.Document.OpenNew(true);

                    switch (responseVm.InferredContentType) {
                        case InferredContentType.Html:
                            doc.Write(responseVm.Content);
                            break;
                        case InferredContentType.Xml:
                            doc.Write(String.Format("<html><body><xmp>{0}</xmp></body></html>", responseVm.PrettyPrintedContent));
                            break;
                        default:
                            doc.Write(String.Format("<html><body><pre>{0}</pre></body></html>", responseVm.PrettyPrintedContent));
                            break;
                    }
                    break;
            }
        }

        private RequestViewModel buildRequestViewModel() {
            //build the request view
            var checkedHttpMethod = rbGrpHttpMethods.Where(x => x.Checked).FirstOrDefault();
            return new RequestViewModel() {
                Url = txtUrl.Text,
                Method = checkedHttpMethod == null ? null : ((Method?)checkedHttpMethod.Tag),
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
                bind(new ResponseViewModel("Loading..."));
                grpResponse.Update();

                //execute the request and get the response
                var restRequest = requestModel.ToRestRequest();
                var client = new RestClient();
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
            txtResponseHeaders.Text = responseVm.Headers;
            lblResponseTimeValue.Text = responseVm.ElapsedTime;
            updateResponseBodyOutput(lastResponseViewModel);
        }

        private void bind(RequestViewModel requestVm) {
            txtUrl.Text = requestVm.Url;
            
            var method = requestVm.Method ?? Method.GET;
            rbGrpHttpMethods.First(x => ((Method) x.Tag) == method).Checked = true;

            txtRequestHeaders.Lines = (requestVm.Headers ?? new string[0]).ToArray();
            txtRequestBody.Text = requestVm.Body;
        }

        private void btnClearRequest_Click(object sender, EventArgs e)
        {
            setIsLastOpenedRequestFileDirtyToTrue();
            bind(new RequestViewModel());
            bind(new ResponseViewModel());
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
                requestSaveFileDialog.FileName = null;
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
            this.lastOpenedRequestFile = fileName;
            this.isLastOpenedRequestFileDirty = false;
            this.Text = fileName + " - RestSharp GUI";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            save(lastOpenedRequestFile);
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
            bind(new ResponseViewModel("")); // clear the response.
            bind(requestVm);
            updateLastOpenedRequestFile(fileName);
        }

        private void exportResponseBodyToolStripMenuItem_Click(object sender, EventArgs e) {
            responseBodySaveFileDialog.FileName = null;
            
            //set filter based on inferred content type
            string filter = "All files|*.*";
            if(lastResponseViewModel.InferredContentType != InferredContentType.Other) {
                string ctExt = InferredContentTypeUtils.FileExtension(lastResponseViewModel.InferredContentType);
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
            persistGuiSettings();
        }
    }
}
