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

//example text/xml response: http://www.w3schools.com/xml/note.asp
namespace Swensen.RestSharpGui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rbHttpGet.Tag = Method.GET;
            rbHttpPost.Tag = Method.POST;
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            var checkedHttpMethod = grpHttpMethod.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            var requestVm = new RequestViewModel() {
                Url = txtUrl.Text,
                Method = checkedHttpMethod == null ? null : ((Method?)checkedHttpMethod.Tag),
                Headers = txtRequestHeaders.Lines.ToArray(),
                Body = txtRequestBody.Text
            };

            RequestModel requestModel = null;
            var validationErrors = RequestModel.TryCreate(requestVm, out requestModel);
            if (validationErrors.Count > 0)
                MessageBox.Show(this, String.Join(Environment.NewLine, validationErrors), "Request Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                var restRequest = requestModel.ToRestRequest();
                var client = new RestClient();
                var response = client.Execute(restRequest);

                lblResponseStatusValue.Text = response.ResponseStatus == ResponseStatus.Completed ? string.Format("{0} {1}", (int) response.StatusCode, response.StatusDescription) : response.ResponseStatus.ToString();
                rtResponseText.Text = prettyPrint(response.ContentType, response.Content);
                txtResponseHeaders.Text = String.Join(Environment.NewLine, response.Headers.Select(p => p.Name + ": " + p.Value));
            }
        }

        /// <summary>
        /// If contentType is an xml content type, then try to pretty print the rawContent. If that fails or otherwise, just return the rawContent
        /// </summary>
        private string prettyPrint(string contentType, string rawContent) {
            //see http://stackoverflow.com/a/2965701/236255 for list of xml content types (credit to http://stackoverflow.com/users/18936/bobince)
            if (contentType != null && (contentType == "text/xml" || contentType == "application/xml" || contentType.EndsWith("+xml"))) {
                try {
                    return XDocument.Parse(rawContent).ToString();
                } catch {
                    return rawContent;
                }
            } else
                return rawContent;
        }
    }
}
