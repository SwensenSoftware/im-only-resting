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
                bind(new ResponseViewModel());//clear response view and show loading message status
                grpResponse.Update();
                var restRequest = requestModel.ToRestRequest();
                var client = new RestClient();
                var restResponse = client.Execute(restRequest);
                var responseVm = new ResponseViewModel(restResponse);
                bind(responseVm);
                grpResponse.Update();
            }
        }

        private void bind(ResponseViewModel responseVm) {
            lblResponseStatusValue.Text = responseVm.ResponseStatus;
            rtResponseText.Text = responseVm.PrettyPrintedContent;
            txtResponseHeaders.Text = responseVm.Headers;            
        }
    }
}
