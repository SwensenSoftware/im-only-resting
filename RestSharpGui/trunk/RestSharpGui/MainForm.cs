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
            List<string> validationErrors = new List<string>();
            if(String.IsNullOrWhiteSpace(txtUrl.Text))
                validationErrors.Add("Request URL may not be empty");

            var url = txtUrl.Text.Trim();

            var rbHttpMethod = grpHttpMethod.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            Method? method = null;
            if(rbHttpMethod == null)
                validationErrors.Add("Request HTTP Method must be selected");
            else
                method = (Method)rbHttpMethod.Tag;

            var requestHeaders = new Dictionary<string,string>();
            foreach (var line in txtRequestHeaders.Lines) {
                var kv = line.Split(':');
                if (kv.Length != 2)
                    validationErrors.Add("Invalid header line: " + line);
                else {
                    var key = kv[0].Trim();
                    var value = kv[1].Trim();
                    if (String.IsNullOrWhiteSpace(key) || String.IsNullOrWhiteSpace(value))
                        validationErrors.Add("Invalid header line: " + line);
                    else
                        requestHeaders.Add(key, value);
                }
            }

            var body = txtBody.Text;

            if (validationErrors.Count > 0)
                MessageBox.Show(this, String.Join(Environment.NewLine, validationErrors), "Request Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                var response = submitRequest(url, method.Value, body, requestHeaders);
                lblResponseStatusValue.Text = response.ResponseStatus == ResponseStatus.Completed ? string.Format("{0} - {1}", (int) response.StatusCode, response.StatusDescription) : response.ResponseStatus.ToString();
                rtResponseText.Text = prettyPrint(response.ContentType, response.Content);
                txtResponseHeaders.Text = String.Join(Environment.NewLine, response.Headers.Select(p => p.Name + ": " + p.Value));
            }
        }

        private IRestResponse submitRequest(string url, Method method, string body, Dictionary<string, string> requestHeaders) {
            if (url.StartsWith("www.")) //give 'em a break, add protocal if only www. given
                url = "http://" + url;

            var request = new RestRequest(url, method);
            foreach (var header in requestHeaders)
                request.AddHeader(header.Key, header.Value);
            
            var client = new RestClient();
            return client.Execute(request);
        }

        /// <summary>
        /// If contentType is an xml content type, then try to pretty print the rawContent. If that fails or otherwise, just return the rawContent
        /// </summary>
        private string prettyPrint(string contentType, string rawContent) {
            //see http://stackoverflow.com/a/2965701/236255 for list of xml content types (credit to http://stackoverflow.com/users/18936/bobince)
            if (contentType == "text/xml" || contentType == "application/xml" || contentType.EndsWith("+xml")) {
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
