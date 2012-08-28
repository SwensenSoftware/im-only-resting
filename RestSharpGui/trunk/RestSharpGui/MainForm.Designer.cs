namespace Swensen.RestSharpGui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSubmitRequest = new System.Windows.Forms.Button();
            this.grpRequest = new System.Windows.Forms.GroupBox();
            this.grpHttpMethod = new System.Windows.Forms.GroupBox();
            this.rbHttpPost = new System.Windows.Forms.RadioButton();
            this.rbHttpGet = new System.Windows.Forms.RadioButton();
            this.grpHeaders = new System.Windows.Forms.GroupBox();
            this.txtRequestHeaders = new System.Windows.Forms.TextBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.grpResponse = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblResponseStatusValue = new System.Windows.Forms.Label();
            this.tcResponse = new System.Windows.Forms.TabControl();
            this.tpResponseText = new System.Windows.Forms.TabPage();
            this.tpResponseHeaders = new System.Windows.Forms.TabPage();
            this.txtResponseHeaders = new System.Windows.Forms.TextBox();
            this.rtResponseText = new System.Windows.Forms.RichTextBox();
            this.grpRequest.SuspendLayout();
            this.grpHttpMethod.SuspendLayout();
            this.grpHeaders.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.grpResponse.SuspendLayout();
            this.tcResponse.SuspendLayout();
            this.tpResponseText.SuspendLayout();
            this.tpResponseHeaders.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(6, 20);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(23, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Url:";
            // 
            // txtUrl
            // 
            this.txtUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtUrl.Location = new System.Drawing.Point(35, 17);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(667, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.WordWrap = false;
            // 
            // btnSubmitRequest
            // 
            this.btnSubmitRequest.Location = new System.Drawing.Point(711, 12);
            this.btnSubmitRequest.Name = "btnSubmitRequest";
            this.btnSubmitRequest.Size = new System.Drawing.Size(78, 28);
            this.btnSubmitRequest.TabIndex = 4;
            this.btnSubmitRequest.Text = "Submit";
            this.btnSubmitRequest.UseVisualStyleBackColor = true;
            this.btnSubmitRequest.Click += new System.EventHandler(this.btnSubmitRequest_Click);
            // 
            // grpRequest
            // 
            this.grpRequest.Controls.Add(this.grpBody);
            this.grpRequest.Controls.Add(this.grpHeaders);
            this.grpRequest.Controls.Add(this.grpHttpMethod);
            this.grpRequest.Controls.Add(this.btnSubmitRequest);
            this.grpRequest.Controls.Add(this.lblUrl);
            this.grpRequest.Controls.Add(this.txtUrl);
            this.grpRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpRequest.Location = new System.Drawing.Point(0, 0);
            this.grpRequest.Name = "grpRequest";
            this.grpRequest.Size = new System.Drawing.Size(801, 453);
            this.grpRequest.TabIndex = 1;
            this.grpRequest.TabStop = false;
            this.grpRequest.Text = "Request";
            // 
            // grpHttpMethod
            // 
            this.grpHttpMethod.Controls.Add(this.rbHttpGet);
            this.grpHttpMethod.Controls.Add(this.rbHttpPost);
            this.grpHttpMethod.Location = new System.Drawing.Point(10, 52);
            this.grpHttpMethod.Name = "grpHttpMethod";
            this.grpHttpMethod.Size = new System.Drawing.Size(71, 68);
            this.grpHttpMethod.TabIndex = 2;
            this.grpHttpMethod.TabStop = false;
            this.grpHttpMethod.Text = "Method";
            // 
            // rbHttpPost
            // 
            this.rbHttpPost.AutoSize = true;
            this.rbHttpPost.Location = new System.Drawing.Point(6, 42);
            this.rbHttpPost.Name = "rbHttpPost";
            this.rbHttpPost.Size = new System.Drawing.Size(46, 17);
            this.rbHttpPost.TabIndex = 0;
            this.rbHttpPost.Tag = "";
            this.rbHttpPost.Text = "Post";
            this.rbHttpPost.UseVisualStyleBackColor = true;
            // 
            // rbHttpGet
            // 
            this.rbHttpGet.AutoSize = true;
            this.rbHttpGet.Checked = true;
            this.rbHttpGet.Location = new System.Drawing.Point(6, 19);
            this.rbHttpGet.Name = "rbHttpGet";
            this.rbHttpGet.Size = new System.Drawing.Size(42, 17);
            this.rbHttpGet.TabIndex = 1;
            this.rbHttpGet.TabStop = true;
            this.rbHttpGet.Text = "Get";
            this.rbHttpGet.UseVisualStyleBackColor = true;
            // 
            // grpHeaders
            // 
            this.grpHeaders.Controls.Add(this.txtRequestHeaders);
            this.grpHeaders.Location = new System.Drawing.Point(94, 52);
            this.grpHeaders.Name = "grpHeaders";
            this.grpHeaders.Size = new System.Drawing.Size(695, 144);
            this.grpHeaders.TabIndex = 4;
            this.grpHeaders.TabStop = false;
            this.grpHeaders.Text = "Headers";
            // 
            // txtRequestHeaders
            // 
            this.txtRequestHeaders.AcceptsReturn = true;
            this.txtRequestHeaders.AcceptsTab = true;
            this.txtRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestHeaders.Location = new System.Drawing.Point(3, 16);
            this.txtRequestHeaders.Multiline = true;
            this.txtRequestHeaders.Name = "txtRequestHeaders";
            this.txtRequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestHeaders.Size = new System.Drawing.Size(689, 125);
            this.txtRequestHeaders.TabIndex = 3;
            this.txtRequestHeaders.WordWrap = false;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtBody);
            this.grpBody.Location = new System.Drawing.Point(9, 210);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(779, 232);
            this.grpBody.TabIndex = 5;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "Body";
            // 
            // txtBody
            // 
            this.txtBody.AcceptsReturn = true;
            this.txtBody.AcceptsTab = true;
            this.txtBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBody.Location = new System.Drawing.Point(3, 16);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBody.Size = new System.Drawing.Size(773, 213);
            this.txtBody.TabIndex = 0;
            this.txtBody.WordWrap = false;
            // 
            // grpResponse
            // 
            this.grpResponse.Controls.Add(this.tcResponse);
            this.grpResponse.Controls.Add(this.lblResponseStatusValue);
            this.grpResponse.Controls.Add(this.lblStatus);
            this.grpResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResponse.Location = new System.Drawing.Point(0, 453);
            this.grpResponse.Name = "grpResponse";
            this.grpResponse.Size = new System.Drawing.Size(801, 337);
            this.grpResponse.TabIndex = 2;
            this.grpResponse.TabStop = false;
            this.grpResponse.Text = "Response";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status:";
            this.lblStatus.UseWaitCursor = true;
            // 
            // lblResponseStatusValue
            // 
            this.lblResponseStatusValue.AutoSize = true;
            this.lblResponseStatusValue.Location = new System.Drawing.Point(60, 21);
            this.lblResponseStatusValue.Name = "lblResponseStatusValue";
            this.lblResponseStatusValue.Size = new System.Drawing.Size(0, 13);
            this.lblResponseStatusValue.TabIndex = 1;
            // 
            // tcResponse
            // 
            this.tcResponse.Controls.Add(this.tpResponseText);
            this.tcResponse.Controls.Add(this.tpResponseHeaders);
            this.tcResponse.Location = new System.Drawing.Point(6, 43);
            this.tcResponse.Name = "tcResponse";
            this.tcResponse.SelectedIndex = 0;
            this.tcResponse.Size = new System.Drawing.Size(789, 283);
            this.tcResponse.TabIndex = 3;
            // 
            // tpResponseText
            // 
            this.tpResponseText.Controls.Add(this.rtResponseText);
            this.tpResponseText.Location = new System.Drawing.Point(4, 22);
            this.tpResponseText.Name = "tpResponseText";
            this.tpResponseText.Padding = new System.Windows.Forms.Padding(3);
            this.tpResponseText.Size = new System.Drawing.Size(781, 257);
            this.tpResponseText.TabIndex = 0;
            this.tpResponseText.Text = "Content";
            this.tpResponseText.UseVisualStyleBackColor = true;
            // 
            // tpResponseHeaders
            // 
            this.tpResponseHeaders.Controls.Add(this.txtResponseHeaders);
            this.tpResponseHeaders.Location = new System.Drawing.Point(4, 22);
            this.tpResponseHeaders.Name = "tpResponseHeaders";
            this.tpResponseHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpResponseHeaders.Size = new System.Drawing.Size(781, 257);
            this.tpResponseHeaders.TabIndex = 1;
            this.tpResponseHeaders.Text = "Headers";
            this.tpResponseHeaders.UseVisualStyleBackColor = true;
            // 
            // txtResponseHeaders
            // 
            this.txtResponseHeaders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeaders.Location = new System.Drawing.Point(3, 3);
            this.txtResponseHeaders.Multiline = true;
            this.txtResponseHeaders.Name = "txtResponseHeaders";
            this.txtResponseHeaders.ReadOnly = true;
            this.txtResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseHeaders.Size = new System.Drawing.Size(775, 251);
            this.txtResponseHeaders.TabIndex = 0;
            this.txtResponseHeaders.WordWrap = false;
            // 
            // rtResponseText
            // 
            this.rtResponseText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtResponseText.Location = new System.Drawing.Point(3, 3);
            this.rtResponseText.Name = "rtResponseText";
            this.rtResponseText.ReadOnly = true;
            this.rtResponseText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtResponseText.Size = new System.Drawing.Size(775, 251);
            this.rtResponseText.TabIndex = 0;
            this.rtResponseText.Text = "";
            this.rtResponseText.WordWrap = false;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSubmitRequest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 790);
            this.Controls.Add(this.grpResponse);
            this.Controls.Add(this.grpRequest);
            this.Name = "MainForm";
            this.Text = "RestSharp GUI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpRequest.ResumeLayout(false);
            this.grpRequest.PerformLayout();
            this.grpHttpMethod.ResumeLayout(false);
            this.grpHttpMethod.PerformLayout();
            this.grpHeaders.ResumeLayout(false);
            this.grpHeaders.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            this.grpResponse.ResumeLayout(false);
            this.grpResponse.PerformLayout();
            this.tcResponse.ResumeLayout(false);
            this.tpResponseText.ResumeLayout(false);
            this.tpResponseHeaders.ResumeLayout(false);
            this.tpResponseHeaders.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSubmitRequest;
        private System.Windows.Forms.GroupBox grpRequest;
        private System.Windows.Forms.GroupBox grpHttpMethod;
        private System.Windows.Forms.RadioButton rbHttpPost;
        private System.Windows.Forms.GroupBox grpHeaders;
        private System.Windows.Forms.TextBox txtRequestHeaders;
        private System.Windows.Forms.RadioButton rbHttpGet;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.GroupBox grpResponse;
        private System.Windows.Forms.Label lblResponseStatusValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tcResponse;
        private System.Windows.Forms.TabPage tpResponseText;
        private System.Windows.Forms.TabPage tpResponseHeaders;
        private System.Windows.Forms.TextBox txtResponseHeaders;
        private System.Windows.Forms.RichTextBox rtResponseText;
    }
}

