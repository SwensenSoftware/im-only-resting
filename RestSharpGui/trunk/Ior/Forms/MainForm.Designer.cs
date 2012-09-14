namespace Swensen.Ior.Forms
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
            this.splitterMain = new System.Windows.Forms.SplitContainer();
            this.grpRequest = new System.Windows.Forms.GroupBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtRequestBody = new Swensen.Ior.Forms.StandardTextBox();
            this.pnlTopHalfOfRequest = new System.Windows.Forms.Panel();
            this.pnlRequestMethodAndHeaders = new System.Windows.Forms.Panel();
            this.grpHeaders = new System.Windows.Forms.GroupBox();
            this.txtRequestHeaders = new Swensen.Ior.Forms.StandardTextBox();
            this.grpHttpMethod = new System.Windows.Forms.GroupBox();
            this.rbHttpPatch = new System.Windows.Forms.RadioButton();
            this.rbHttpHead = new System.Windows.Forms.RadioButton();
            this.rbHttpDelete = new System.Windows.Forms.RadioButton();
            this.rbHttpOptions = new System.Windows.Forms.RadioButton();
            this.rbHttpPut = new System.Windows.Forms.RadioButton();
            this.rbHttpGet = new System.Windows.Forms.RadioButton();
            this.rbHttpPost = new System.Windows.Forms.RadioButton();
            this.pnlRequestUrlAndButtons = new System.Windows.Forms.Panel();
            this.pnlUrl = new System.Windows.Forms.Panel();
            this.cbRequestUrl = new System.Windows.Forms.ComboBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnRequestButtons = new System.Windows.Forms.Panel();
            this.btnClearRequest = new System.Windows.Forms.Button();
            this.btnSubmitRequest = new System.Windows.Forms.Button();
            this.grpResponse = new System.Windows.Forms.GroupBox();
            this.tcResponse = new System.Windows.Forms.TabControl();
            this.tpResponseText = new System.Windows.Forms.TabPage();
            this.pnlResponseContent = new System.Windows.Forms.Panel();
            this.rtResponseText = new System.Windows.Forms.RichTextBox();
            this.grpResponseBodyOutput = new System.Windows.Forms.GroupBox();
            this.rbResponseBodyOutputBrowser = new System.Windows.Forms.RadioButton();
            this.rbResponseBodyOutputPretty = new System.Windows.Forms.RadioButton();
            this.rbResponseBodyOutputRaw = new System.Windows.Forms.RadioButton();
            this.tpResponseHeaders = new System.Windows.Forms.TabPage();
            this.txtResponseHeaders = new System.Windows.Forms.TextBox();
            this.pnlResponseStatusAndTime = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResponseStatus = new System.Windows.Forms.Panel();
            this.lnkCancelRequest = new System.Windows.Forms.LinkLabel();
            this.lnkResponseStatusInfo = new System.Windows.Forms.LinkLabel();
            this.lblResponseStatusValue = new System.Windows.Forms.Label();
            this.lblResponseStatus = new System.Windows.Forms.Label();
            this.pnlResponseTime = new System.Windows.Forms.Panel();
            this.lblResponseTimeValue = new System.Windows.Forms.Label();
            this.lblResponseTime = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportResponseBodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.requestOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.responseBodySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
            this.splitterMain.Panel1.SuspendLayout();
            this.splitterMain.Panel2.SuspendLayout();
            this.splitterMain.SuspendLayout();
            this.grpRequest.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.pnlTopHalfOfRequest.SuspendLayout();
            this.pnlRequestMethodAndHeaders.SuspendLayout();
            this.grpHeaders.SuspendLayout();
            this.grpHttpMethod.SuspendLayout();
            this.pnlRequestUrlAndButtons.SuspendLayout();
            this.pnlUrl.SuspendLayout();
            this.btnRequestButtons.SuspendLayout();
            this.grpResponse.SuspendLayout();
            this.tcResponse.SuspendLayout();
            this.tpResponseText.SuspendLayout();
            this.pnlResponseContent.SuspendLayout();
            this.grpResponseBodyOutput.SuspendLayout();
            this.tpResponseHeaders.SuspendLayout();
            this.pnlResponseStatusAndTime.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlResponseStatus.SuspendLayout();
            this.pnlResponseTime.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterMain
            // 
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterMain.Location = new System.Drawing.Point(0, 24);
            this.splitterMain.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.splitterMain.Name = "splitterMain";
            // 
            // splitterMain.Panel1
            // 
            this.splitterMain.Panel1.Controls.Add(this.grpRequest);
            this.splitterMain.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitterMain.Panel2
            // 
            this.splitterMain.Panel2.Controls.Add(this.grpResponse);
            this.splitterMain.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitterMain.Size = new System.Drawing.Size(726, 500);
            this.splitterMain.SplitterDistance = 366;
            this.splitterMain.SplitterWidth = 8;
            this.splitterMain.TabIndex = 3;
            this.splitterMain.DoubleClick += new System.EventHandler(this.splitterMain_DoubleClick);
            // 
            // grpRequest
            // 
            this.grpRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpRequest.Controls.Add(this.grpBody);
            this.grpRequest.Controls.Add(this.pnlTopHalfOfRequest);
            this.grpRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRequest.Location = new System.Drawing.Point(3, 3);
            this.grpRequest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpRequest.Name = "grpRequest";
            this.grpRequest.Size = new System.Drawing.Size(360, 494);
            this.grpRequest.TabIndex = 2;
            this.grpRequest.TabStop = false;
            this.grpRequest.Text = "Request";
            // 
            // grpBody
            // 
            this.grpBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpBody.Controls.Add(this.txtRequestBody);
            this.grpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBody.Location = new System.Drawing.Point(3, 174);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(354, 317);
            this.grpBody.TabIndex = 10;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "Body";
            // 
            // txtRequestBody
            // 
            this.txtRequestBody.AcceptsReturn = true;
            this.txtRequestBody.AcceptsTab = true;
            this.txtRequestBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestBody.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestBody.Location = new System.Drawing.Point(3, 16);
            this.txtRequestBody.Multiline = true;
            this.txtRequestBody.Name = "txtRequestBody";
            this.txtRequestBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestBody.Size = new System.Drawing.Size(348, 298);
            this.txtRequestBody.TabIndex = 7;
            this.txtRequestBody.WordWrap = false;
            this.txtRequestBody.TextChanged += new System.EventHandler(this.txtRequestBody_TextChanged);
            // 
            // pnlTopHalfOfRequest
            // 
            this.pnlTopHalfOfRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlTopHalfOfRequest.Controls.Add(this.pnlRequestMethodAndHeaders);
            this.pnlTopHalfOfRequest.Controls.Add(this.pnlRequestUrlAndButtons);
            this.pnlTopHalfOfRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHalfOfRequest.Location = new System.Drawing.Point(3, 16);
            this.pnlTopHalfOfRequest.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTopHalfOfRequest.Name = "pnlTopHalfOfRequest";
            this.pnlTopHalfOfRequest.Size = new System.Drawing.Size(354, 158);
            this.pnlTopHalfOfRequest.TabIndex = 9;
            // 
            // pnlRequestMethodAndHeaders
            // 
            this.pnlRequestMethodAndHeaders.Controls.Add(this.grpHeaders);
            this.pnlRequestMethodAndHeaders.Controls.Add(this.grpHttpMethod);
            this.pnlRequestMethodAndHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRequestMethodAndHeaders.Location = new System.Drawing.Point(0, 33);
            this.pnlRequestMethodAndHeaders.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRequestMethodAndHeaders.Name = "pnlRequestMethodAndHeaders";
            this.pnlRequestMethodAndHeaders.Size = new System.Drawing.Size(354, 125);
            this.pnlRequestMethodAndHeaders.TabIndex = 6;
            // 
            // grpHeaders
            // 
            this.grpHeaders.Controls.Add(this.txtRequestHeaders);
            this.grpHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHeaders.Location = new System.Drawing.Point(131, 0);
            this.grpHeaders.Name = "grpHeaders";
            this.grpHeaders.Size = new System.Drawing.Size(223, 125);
            this.grpHeaders.TabIndex = 6;
            this.grpHeaders.TabStop = false;
            this.grpHeaders.Text = "Headers";
            // 
            // txtRequestHeaders
            // 
            this.txtRequestHeaders.AcceptsReturn = true;
            this.txtRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestHeaders.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestHeaders.Location = new System.Drawing.Point(3, 16);
            this.txtRequestHeaders.Multiline = true;
            this.txtRequestHeaders.Name = "txtRequestHeaders";
            this.txtRequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestHeaders.Size = new System.Drawing.Size(217, 106);
            this.txtRequestHeaders.TabIndex = 6;
            this.txtRequestHeaders.WordWrap = false;
            this.txtRequestHeaders.TextChanged += new System.EventHandler(this.txtRequestHeaders_TextChanged);
            // 
            // grpHttpMethod
            // 
            this.grpHttpMethod.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpHttpMethod.Controls.Add(this.rbHttpPatch);
            this.grpHttpMethod.Controls.Add(this.rbHttpHead);
            this.grpHttpMethod.Controls.Add(this.rbHttpDelete);
            this.grpHttpMethod.Controls.Add(this.rbHttpOptions);
            this.grpHttpMethod.Controls.Add(this.rbHttpPut);
            this.grpHttpMethod.Controls.Add(this.rbHttpGet);
            this.grpHttpMethod.Controls.Add(this.rbHttpPost);
            this.grpHttpMethod.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpHttpMethod.Location = new System.Drawing.Point(0, 0);
            this.grpHttpMethod.Name = "grpHttpMethod";
            this.grpHttpMethod.Size = new System.Drawing.Size(131, 125);
            this.grpHttpMethod.TabIndex = 5;
            this.grpHttpMethod.TabStop = false;
            this.grpHttpMethod.Text = "Method";
            // 
            // rbHttpPatch
            // 
            this.rbHttpPatch.AutoSize = true;
            this.rbHttpPatch.Location = new System.Drawing.Point(67, 19);
            this.rbHttpPatch.Margin = new System.Windows.Forms.Padding(2);
            this.rbHttpPatch.Name = "rbHttpPatch";
            this.rbHttpPatch.Size = new System.Drawing.Size(53, 17);
            this.rbHttpPatch.TabIndex = 7;
            this.rbHttpPatch.TabStop = true;
            this.rbHttpPatch.Text = "Patch";
            this.rbHttpPatch.UseVisualStyleBackColor = true;
            // 
            // rbHttpHead
            // 
            this.rbHttpHead.AutoSize = true;
            this.rbHttpHead.Location = new System.Drawing.Point(67, 64);
            this.rbHttpHead.Margin = new System.Windows.Forms.Padding(2);
            this.rbHttpHead.Name = "rbHttpHead";
            this.rbHttpHead.Size = new System.Drawing.Size(51, 17);
            this.rbHttpHead.TabIndex = 6;
            this.rbHttpHead.TabStop = true;
            this.rbHttpHead.Text = "Head";
            this.rbHttpHead.UseVisualStyleBackColor = true;
            // 
            // rbHttpDelete
            // 
            this.rbHttpDelete.AutoSize = true;
            this.rbHttpDelete.Location = new System.Drawing.Point(6, 64);
            this.rbHttpDelete.Name = "rbHttpDelete";
            this.rbHttpDelete.Size = new System.Drawing.Size(56, 17);
            this.rbHttpDelete.TabIndex = 3;
            this.rbHttpDelete.Text = "Delete";
            this.rbHttpDelete.UseVisualStyleBackColor = true;
            // 
            // rbHttpOptions
            // 
            this.rbHttpOptions.AutoSize = true;
            this.rbHttpOptions.Location = new System.Drawing.Point(67, 41);
            this.rbHttpOptions.Name = "rbHttpOptions";
            this.rbHttpOptions.Size = new System.Drawing.Size(61, 17);
            this.rbHttpOptions.TabIndex = 5;
            this.rbHttpOptions.Text = "Options";
            this.rbHttpOptions.UseVisualStyleBackColor = true;
            // 
            // rbHttpPut
            // 
            this.rbHttpPut.AutoSize = true;
            this.rbHttpPut.Location = new System.Drawing.Point(6, 87);
            this.rbHttpPut.Name = "rbHttpPut";
            this.rbHttpPut.Size = new System.Drawing.Size(41, 17);
            this.rbHttpPut.TabIndex = 4;
            this.rbHttpPut.Text = "Put";
            this.rbHttpPut.UseVisualStyleBackColor = true;
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
            // rbHttpPost
            // 
            this.rbHttpPost.AutoSize = true;
            this.rbHttpPost.Location = new System.Drawing.Point(6, 41);
            this.rbHttpPost.Name = "rbHttpPost";
            this.rbHttpPost.Size = new System.Drawing.Size(46, 17);
            this.rbHttpPost.TabIndex = 2;
            this.rbHttpPost.Text = "Post";
            this.rbHttpPost.UseVisualStyleBackColor = true;
            // 
            // pnlRequestUrlAndButtons
            // 
            this.pnlRequestUrlAndButtons.Controls.Add(this.pnlUrl);
            this.pnlRequestUrlAndButtons.Controls.Add(this.btnRequestButtons);
            this.pnlRequestUrlAndButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRequestUrlAndButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlRequestUrlAndButtons.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRequestUrlAndButtons.Name = "pnlRequestUrlAndButtons";
            this.pnlRequestUrlAndButtons.Size = new System.Drawing.Size(354, 33);
            this.pnlRequestUrlAndButtons.TabIndex = 5;
            // 
            // pnlUrl
            // 
            this.pnlUrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlUrl.Controls.Add(this.cbRequestUrl);
            this.pnlUrl.Controls.Add(this.lblUrl);
            this.pnlUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUrl.Location = new System.Drawing.Point(0, 0);
            this.pnlUrl.Name = "pnlUrl";
            this.pnlUrl.Padding = new System.Windows.Forms.Padding(6);
            this.pnlUrl.Size = new System.Drawing.Size(260, 33);
            this.pnlUrl.TabIndex = 6;
            // 
            // cbRequestUrl
            // 
            this.cbRequestUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbRequestUrl.FormattingEnabled = true;
            this.cbRequestUrl.Location = new System.Drawing.Point(29, 6);
            this.cbRequestUrl.Margin = new System.Windows.Forms.Padding(2);
            this.cbRequestUrl.Name = "cbRequestUrl";
            this.cbRequestUrl.Size = new System.Drawing.Size(225, 21);
            this.cbRequestUrl.TabIndex = 1;
            this.cbRequestUrl.SelectionChangeCommitted += new System.EventHandler(this.cbRequestUrl_SelectionChangeCommitted);
            this.cbRequestUrl.TextUpdate += new System.EventHandler(this.cbRequestUrl_TextUpdate);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUrl.Location = new System.Drawing.Point(6, 6);
            this.lblUrl.Margin = new System.Windows.Forms.Padding(3);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(23, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Url:";
            // 
            // btnRequestButtons
            // 
            this.btnRequestButtons.Controls.Add(this.btnClearRequest);
            this.btnRequestButtons.Controls.Add(this.btnSubmitRequest);
            this.btnRequestButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRequestButtons.Location = new System.Drawing.Point(260, 0);
            this.btnRequestButtons.Margin = new System.Windows.Forms.Padding(2);
            this.btnRequestButtons.Name = "btnRequestButtons";
            this.btnRequestButtons.Size = new System.Drawing.Size(94, 33);
            this.btnRequestButtons.TabIndex = 5;
            // 
            // btnClearRequest
            // 
            this.btnClearRequest.AutoSize = true;
            this.btnClearRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearRequest.Location = new System.Drawing.Point(51, 4);
            this.btnClearRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearRequest.Name = "btnClearRequest";
            this.btnClearRequest.Size = new System.Drawing.Size(41, 23);
            this.btnClearRequest.TabIndex = 9;
            this.btnClearRequest.Text = "Clear";
            this.btnClearRequest.UseVisualStyleBackColor = true;
            this.btnClearRequest.Click += new System.EventHandler(this.btnClearRequest_Click);
            // 
            // btnSubmitRequest
            // 
            this.btnSubmitRequest.AutoSize = true;
            this.btnSubmitRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSubmitRequest.Location = new System.Drawing.Point(0, 4);
            this.btnSubmitRequest.Name = "btnSubmitRequest";
            this.btnSubmitRequest.Size = new System.Drawing.Size(49, 23);
            this.btnSubmitRequest.TabIndex = 8;
            this.btnSubmitRequest.Text = "Submit";
            this.btnSubmitRequest.UseVisualStyleBackColor = true;
            this.btnSubmitRequest.Click += new System.EventHandler(this.btnSubmitRequest_Click);
            // 
            // grpResponse
            // 
            this.grpResponse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpResponse.Controls.Add(this.tcResponse);
            this.grpResponse.Controls.Add(this.pnlResponseStatusAndTime);
            this.grpResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResponse.Location = new System.Drawing.Point(3, 3);
            this.grpResponse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpResponse.Name = "grpResponse";
            this.grpResponse.Size = new System.Drawing.Size(346, 494);
            this.grpResponse.TabIndex = 0;
            this.grpResponse.TabStop = false;
            this.grpResponse.Text = "Response";
            // 
            // tcResponse
            // 
            this.tcResponse.Controls.Add(this.tpResponseText);
            this.tcResponse.Controls.Add(this.tpResponseHeaders);
            this.tcResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcResponse.Location = new System.Drawing.Point(3, 41);
            this.tcResponse.Name = "tcResponse";
            this.tcResponse.SelectedIndex = 0;
            this.tcResponse.Size = new System.Drawing.Size(340, 450);
            this.tcResponse.TabIndex = 6;
            // 
            // tpResponseText
            // 
            this.tpResponseText.BackColor = System.Drawing.SystemColors.Control;
            this.tpResponseText.Controls.Add(this.pnlResponseContent);
            this.tpResponseText.Controls.Add(this.grpResponseBodyOutput);
            this.tpResponseText.Location = new System.Drawing.Point(4, 22);
            this.tpResponseText.Name = "tpResponseText";
            this.tpResponseText.Padding = new System.Windows.Forms.Padding(3);
            this.tpResponseText.Size = new System.Drawing.Size(332, 424);
            this.tpResponseText.TabIndex = 0;
            this.tpResponseText.Text = "Body";
            // 
            // pnlResponseContent
            // 
            this.pnlResponseContent.Controls.Add(this.rtResponseText);
            this.pnlResponseContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResponseContent.Location = new System.Drawing.Point(3, 44);
            this.pnlResponseContent.Margin = new System.Windows.Forms.Padding(2);
            this.pnlResponseContent.Name = "pnlResponseContent";
            this.pnlResponseContent.Padding = new System.Windows.Forms.Padding(3);
            this.pnlResponseContent.Size = new System.Drawing.Size(326, 377);
            this.pnlResponseContent.TabIndex = 15;
            // 
            // rtResponseText
            // 
            this.rtResponseText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtResponseText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtResponseText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtResponseText.Location = new System.Drawing.Point(3, 3);
            this.rtResponseText.Name = "rtResponseText";
            this.rtResponseText.ReadOnly = true;
            this.rtResponseText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtResponseText.Size = new System.Drawing.Size(320, 371);
            this.rtResponseText.TabIndex = 13;
            this.rtResponseText.Text = "";
            this.rtResponseText.WordWrap = false;
            this.rtResponseText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtResponseText_LinkClicked);
            // 
            // grpResponseBodyOutput
            // 
            this.grpResponseBodyOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpResponseBodyOutput.Controls.Add(this.rbResponseBodyOutputBrowser);
            this.grpResponseBodyOutput.Controls.Add(this.rbResponseBodyOutputPretty);
            this.grpResponseBodyOutput.Controls.Add(this.rbResponseBodyOutputRaw);
            this.grpResponseBodyOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpResponseBodyOutput.Location = new System.Drawing.Point(3, 3);
            this.grpResponseBodyOutput.Margin = new System.Windows.Forms.Padding(2);
            this.grpResponseBodyOutput.Name = "grpResponseBodyOutput";
            this.grpResponseBodyOutput.Padding = new System.Windows.Forms.Padding(2);
            this.grpResponseBodyOutput.Size = new System.Drawing.Size(326, 41);
            this.grpResponseBodyOutput.TabIndex = 11;
            this.grpResponseBodyOutput.TabStop = false;
            this.grpResponseBodyOutput.Text = "Output";
            // 
            // rbResponseBodyOutputBrowser
            // 
            this.rbResponseBodyOutputBrowser.AutoSize = true;
            this.rbResponseBodyOutputBrowser.Location = new System.Drawing.Point(105, 17);
            this.rbResponseBodyOutputBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.rbResponseBodyOutputBrowser.Name = "rbResponseBodyOutputBrowser";
            this.rbResponseBodyOutputBrowser.Size = new System.Drawing.Size(72, 17);
            this.rbResponseBodyOutputBrowser.TabIndex = 2;
            this.rbResponseBodyOutputBrowser.TabStop = true;
            this.rbResponseBodyOutputBrowser.Text = "Rendered";
            this.rbResponseBodyOutputBrowser.UseVisualStyleBackColor = true;
            // 
            // rbResponseBodyOutputPretty
            // 
            this.rbResponseBodyOutputPretty.AutoSize = true;
            this.rbResponseBodyOutputPretty.Location = new System.Drawing.Point(51, 17);
            this.rbResponseBodyOutputPretty.Margin = new System.Windows.Forms.Padding(2);
            this.rbResponseBodyOutputPretty.Name = "rbResponseBodyOutputPretty";
            this.rbResponseBodyOutputPretty.Size = new System.Drawing.Size(52, 17);
            this.rbResponseBodyOutputPretty.TabIndex = 1;
            this.rbResponseBodyOutputPretty.TabStop = true;
            this.rbResponseBodyOutputPretty.Text = "Pretty";
            this.rbResponseBodyOutputPretty.UseVisualStyleBackColor = true;
            // 
            // rbResponseBodyOutputRaw
            // 
            this.rbResponseBodyOutputRaw.AutoSize = true;
            this.rbResponseBodyOutputRaw.Checked = true;
            this.rbResponseBodyOutputRaw.Location = new System.Drawing.Point(4, 17);
            this.rbResponseBodyOutputRaw.Margin = new System.Windows.Forms.Padding(2);
            this.rbResponseBodyOutputRaw.Name = "rbResponseBodyOutputRaw";
            this.rbResponseBodyOutputRaw.Size = new System.Drawing.Size(47, 17);
            this.rbResponseBodyOutputRaw.TabIndex = 0;
            this.rbResponseBodyOutputRaw.TabStop = true;
            this.rbResponseBodyOutputRaw.Text = "Raw";
            this.rbResponseBodyOutputRaw.UseVisualStyleBackColor = true;
            // 
            // tpResponseHeaders
            // 
            this.tpResponseHeaders.BackColor = System.Drawing.SystemColors.Control;
            this.tpResponseHeaders.Controls.Add(this.txtResponseHeaders);
            this.tpResponseHeaders.Location = new System.Drawing.Point(4, 22);
            this.tpResponseHeaders.Name = "tpResponseHeaders";
            this.tpResponseHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpResponseHeaders.Size = new System.Drawing.Size(332, 424);
            this.tpResponseHeaders.TabIndex = 1;
            this.tpResponseHeaders.Text = "Headers";
            // 
            // txtResponseHeaders
            // 
            this.txtResponseHeaders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeaders.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResponseHeaders.Location = new System.Drawing.Point(3, 3);
            this.txtResponseHeaders.Multiline = true;
            this.txtResponseHeaders.Name = "txtResponseHeaders";
            this.txtResponseHeaders.ReadOnly = true;
            this.txtResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseHeaders.Size = new System.Drawing.Size(325, 419);
            this.txtResponseHeaders.TabIndex = 11;
            this.txtResponseHeaders.WordWrap = false;
            // 
            // pnlResponseStatusAndTime
            // 
            this.pnlResponseStatusAndTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlResponseStatusAndTime.Controls.Add(this.tableLayoutPanel1);
            this.pnlResponseStatusAndTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResponseStatusAndTime.Location = new System.Drawing.Point(3, 16);
            this.pnlResponseStatusAndTime.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResponseStatusAndTime.Name = "pnlResponseStatusAndTime";
            this.pnlResponseStatusAndTime.Size = new System.Drawing.Size(340, 25);
            this.pnlResponseStatusAndTime.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnlResponseStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlResponseTime, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlResponseStatus
            // 
            this.pnlResponseStatus.Controls.Add(this.lnkCancelRequest);
            this.pnlResponseStatus.Controls.Add(this.lnkResponseStatusInfo);
            this.pnlResponseStatus.Controls.Add(this.lblResponseStatusValue);
            this.pnlResponseStatus.Controls.Add(this.lblResponseStatus);
            this.pnlResponseStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResponseStatus.Location = new System.Drawing.Point(2, 2);
            this.pnlResponseStatus.Margin = new System.Windows.Forms.Padding(2);
            this.pnlResponseStatus.Name = "pnlResponseStatus";
            this.pnlResponseStatus.Size = new System.Drawing.Size(166, 21);
            this.pnlResponseStatus.TabIndex = 0;
            // 
            // lnkCancelRequest
            // 
            this.lnkCancelRequest.AutoSize = true;
            this.lnkCancelRequest.Dock = System.Windows.Forms.DockStyle.Left;
            this.lnkCancelRequest.Location = new System.Drawing.Point(53, 0);
            this.lnkCancelRequest.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkCancelRequest.Name = "lnkCancelRequest";
            this.lnkCancelRequest.Size = new System.Drawing.Size(40, 13);
            this.lnkCancelRequest.TabIndex = 3;
            this.lnkCancelRequest.TabStop = true;
            this.lnkCancelRequest.Text = "Cancel";
            this.lnkCancelRequest.Visible = false;
            this.lnkCancelRequest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCancelRequest_LinkClicked);
            // 
            // lnkResponseStatusInfo
            // 
            this.lnkResponseStatusInfo.AutoSize = true;
            this.lnkResponseStatusInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lnkResponseStatusInfo.Location = new System.Drawing.Point(40, 0);
            this.lnkResponseStatusInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkResponseStatusInfo.Name = "lnkResponseStatusInfo";
            this.lnkResponseStatusInfo.Size = new System.Drawing.Size(13, 13);
            this.lnkResponseStatusInfo.TabIndex = 2;
            this.lnkResponseStatusInfo.TabStop = true;
            this.lnkResponseStatusInfo.Text = "?";
            this.lnkResponseStatusInfo.Visible = false;
            this.lnkResponseStatusInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResponseStatusInfo_LinkClicked);
            // 
            // lblResponseStatusValue
            // 
            this.lblResponseStatusValue.AutoSize = true;
            this.lblResponseStatusValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseStatusValue.Location = new System.Drawing.Point(40, 0);
            this.lblResponseStatusValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResponseStatusValue.Name = "lblResponseStatusValue";
            this.lblResponseStatusValue.Size = new System.Drawing.Size(0, 13);
            this.lblResponseStatusValue.TabIndex = 1;
            // 
            // lblResponseStatus
            // 
            this.lblResponseStatus.AutoSize = true;
            this.lblResponseStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseStatus.Location = new System.Drawing.Point(0, 0);
            this.lblResponseStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResponseStatus.Name = "lblResponseStatus";
            this.lblResponseStatus.Size = new System.Drawing.Size(40, 13);
            this.lblResponseStatus.TabIndex = 0;
            this.lblResponseStatus.Text = "Status:";
            // 
            // pnlResponseTime
            // 
            this.pnlResponseTime.Controls.Add(this.lblResponseTimeValue);
            this.pnlResponseTime.Controls.Add(this.lblResponseTime);
            this.pnlResponseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResponseTime.Location = new System.Drawing.Point(172, 2);
            this.pnlResponseTime.Margin = new System.Windows.Forms.Padding(2);
            this.pnlResponseTime.Name = "pnlResponseTime";
            this.pnlResponseTime.Size = new System.Drawing.Size(166, 21);
            this.pnlResponseTime.TabIndex = 1;
            // 
            // lblResponseTimeValue
            // 
            this.lblResponseTimeValue.AutoSize = true;
            this.lblResponseTimeValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseTimeValue.Location = new System.Drawing.Point(33, 0);
            this.lblResponseTimeValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResponseTimeValue.Name = "lblResponseTimeValue";
            this.lblResponseTimeValue.Size = new System.Drawing.Size(0, 13);
            this.lblResponseTimeValue.TabIndex = 1;
            // 
            // lblResponseTime
            // 
            this.lblResponseTime.AutoSize = true;
            this.lblResponseTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseTime.Location = new System.Drawing.Point(0, 0);
            this.lblResponseTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResponseTime.Name = "lblResponseTime";
            this.lblResponseTime.Size = new System.Drawing.Size(33, 13);
            this.lblResponseTime.TabIndex = 0;
            this.lblResponseTime.Text = "Time:";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(726, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exportResponseBodyToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 6);
            // 
            // exportResponseBodyToolStripMenuItem
            // 
            this.exportResponseBodyToolStripMenuItem.Name = "exportResponseBodyToolStripMenuItem";
            this.exportResponseBodyToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.exportResponseBodyToolStripMenuItem.Text = "Export Response Body...";
            this.exportResponseBodyToolStripMenuItem.Click += new System.EventHandler(this.exportResponseBodyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(196, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // requestSaveFileDialog
            // 
            this.requestSaveFileDialog.Filter = "Request XML files|*.xml|All files|*.*";
            this.requestSaveFileDialog.RestoreDirectory = true;
            // 
            // requestOpenFileDialog
            // 
            this.requestOpenFileDialog.Filter = "Request XML files|*.xml|All files|*.*";
            this.requestOpenFileDialog.RestoreDirectory = true;
            // 
            // responseBodySaveFileDialog
            // 
            this.responseBodySaveFileDialog.Filter = "All files|*.*";
            this.responseBodySaveFileDialog.RestoreDirectory = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSubmitRequest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 524);
            this.Controls.Add(this.splitterMain);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "I\'m Only Resting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitterMain.Panel1.ResumeLayout(false);
            this.splitterMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
            this.splitterMain.ResumeLayout(false);
            this.grpRequest.ResumeLayout(false);
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            this.pnlTopHalfOfRequest.ResumeLayout(false);
            this.pnlRequestMethodAndHeaders.ResumeLayout(false);
            this.grpHeaders.ResumeLayout(false);
            this.grpHeaders.PerformLayout();
            this.grpHttpMethod.ResumeLayout(false);
            this.grpHttpMethod.PerformLayout();
            this.pnlRequestUrlAndButtons.ResumeLayout(false);
            this.pnlUrl.ResumeLayout(false);
            this.pnlUrl.PerformLayout();
            this.btnRequestButtons.ResumeLayout(false);
            this.btnRequestButtons.PerformLayout();
            this.grpResponse.ResumeLayout(false);
            this.tcResponse.ResumeLayout(false);
            this.tpResponseText.ResumeLayout(false);
            this.pnlResponseContent.ResumeLayout(false);
            this.grpResponseBodyOutput.ResumeLayout(false);
            this.grpResponseBodyOutput.PerformLayout();
            this.tpResponseHeaders.ResumeLayout(false);
            this.tpResponseHeaders.PerformLayout();
            this.pnlResponseStatusAndTime.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlResponseStatus.ResumeLayout(false);
            this.pnlResponseStatus.PerformLayout();
            this.pnlResponseTime.ResumeLayout(false);
            this.pnlResponseTime.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitterMain;
        private System.Windows.Forms.GroupBox grpRequest;
        private System.Windows.Forms.GroupBox grpResponse;
        private System.Windows.Forms.TabControl tcResponse;
        private System.Windows.Forms.TabPage tpResponseText;
        private System.Windows.Forms.TabPage tpResponseHeaders;
        private System.Windows.Forms.TextBox txtResponseHeaders;
        private System.Windows.Forms.Panel pnlResponseStatusAndTime;
        private System.Windows.Forms.Panel pnlTopHalfOfRequest;
        private System.Windows.Forms.Panel pnlRequestMethodAndHeaders;
        private System.Windows.Forms.GroupBox grpHeaders;
        private StandardTextBox txtRequestHeaders;
        private System.Windows.Forms.GroupBox grpHttpMethod;
        private System.Windows.Forms.RadioButton rbHttpGet;
        private System.Windows.Forms.RadioButton rbHttpPost;
        private System.Windows.Forms.Panel pnlRequestUrlAndButtons;
        private System.Windows.Forms.Panel pnlUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Panel btnRequestButtons;
        private System.Windows.Forms.Button btnClearRequest;
        private System.Windows.Forms.Button btnSubmitRequest;
        private System.Windows.Forms.GroupBox grpBody;
        private StandardTextBox txtRequestBody;
        private System.Windows.Forms.RadioButton rbHttpDelete;
        private System.Windows.Forms.RadioButton rbHttpOptions;
        private System.Windows.Forms.RadioButton rbHttpPut;
        private System.Windows.Forms.RadioButton rbHttpPatch;
        private System.Windows.Forms.RadioButton rbHttpHead;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog requestSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog requestOpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem exportResponseBodyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.SaveFileDialog responseBodySaveFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlResponseStatus;
        private System.Windows.Forms.Panel pnlResponseTime;
        private System.Windows.Forms.Label lblResponseStatus;
        private System.Windows.Forms.Label lblResponseStatusValue;
        private System.Windows.Forms.Label lblResponseTimeValue;
        private System.Windows.Forms.Label lblResponseTime;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpResponseBodyOutput;
        private System.Windows.Forms.RadioButton rbResponseBodyOutputBrowser;
        private System.Windows.Forms.RadioButton rbResponseBodyOutputPretty;
        private System.Windows.Forms.RadioButton rbResponseBodyOutputRaw;
        private System.Windows.Forms.RichTextBox rtResponseText;
        private System.Windows.Forms.Panel pnlResponseContent;
        private System.Windows.Forms.LinkLabel lnkResponseStatusInfo;
        private System.Windows.Forms.LinkLabel lnkCancelRequest;
        private System.Windows.Forms.ComboBox cbRequestUrl;

    }
}

