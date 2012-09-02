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
            this.splitterMain = new System.Windows.Forms.SplitContainer();
            this.grpRequest = new System.Windows.Forms.GroupBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtRequestBody = new System.Windows.Forms.TextBox();
            this.pnlTopHalfOfRequest = new System.Windows.Forms.Panel();
            this.pnlRequestMethodAndHeaders = new System.Windows.Forms.Panel();
            this.grpHeaders = new System.Windows.Forms.GroupBox();
            this.txtRequestHeaders = new System.Windows.Forms.TextBox();
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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnRequestButtons = new System.Windows.Forms.Panel();
            this.btnClearRequest = new System.Windows.Forms.Button();
            this.btnSubmitRequest = new System.Windows.Forms.Button();
            this.grpResponse = new System.Windows.Forms.GroupBox();
            this.tcResponse = new System.Windows.Forms.TabControl();
            this.tpResponseText = new System.Windows.Forms.TabPage();
            this.rtResponseText = new System.Windows.Forms.RichTextBox();
            this.tpResponseHeaders = new System.Windows.Forms.TabPage();
            this.txtResponseHeaders = new System.Windows.Forms.TextBox();
            this.pnlResponseStatusAndTime = new System.Windows.Forms.Panel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportResponseBodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.requestOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.responseBodySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResponseStatus = new System.Windows.Forms.Panel();
            this.pnlResponseTime = new System.Windows.Forms.Panel();
            this.lblResponseStatus = new System.Windows.Forms.Label();
            this.lblResponseStatusValue = new System.Windows.Forms.Label();
            this.lblResponseTime = new System.Windows.Forms.Label();
            this.lblResponseTimeValue = new System.Windows.Forms.Label();
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
            this.tpResponseHeaders.SuspendLayout();
            this.pnlResponseStatusAndTime.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlResponseStatus.SuspendLayout();
            this.pnlResponseTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterMain
            // 
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterMain.Location = new System.Drawing.Point(0, 28);
            this.splitterMain.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.splitterMain.Name = "splitterMain";
            // 
            // splitterMain.Panel1
            // 
            this.splitterMain.Panel1.Controls.Add(this.grpRequest);
            this.splitterMain.Panel1.Padding = new System.Windows.Forms.Padding(4);
            // 
            // splitterMain.Panel2
            // 
            this.splitterMain.Panel2.Controls.Add(this.grpResponse);
            this.splitterMain.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.splitterMain.Size = new System.Drawing.Size(1435, 617);
            this.splitterMain.SplitterDistance = 728;
            this.splitterMain.SplitterWidth = 8;
            this.splitterMain.TabIndex = 3;
            // 
            // grpRequest
            // 
            this.grpRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpRequest.Controls.Add(this.grpBody);
            this.grpRequest.Controls.Add(this.pnlTopHalfOfRequest);
            this.grpRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRequest.Location = new System.Drawing.Point(4, 4);
            this.grpRequest.Margin = new System.Windows.Forms.Padding(6);
            this.grpRequest.Name = "grpRequest";
            this.grpRequest.Padding = new System.Windows.Forms.Padding(4);
            this.grpRequest.Size = new System.Drawing.Size(720, 609);
            this.grpRequest.TabIndex = 2;
            this.grpRequest.TabStop = false;
            this.grpRequest.Text = "Request";
            // 
            // grpBody
            // 
            this.grpBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpBody.Controls.Add(this.txtRequestBody);
            this.grpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBody.Location = new System.Drawing.Point(4, 213);
            this.grpBody.Margin = new System.Windows.Forms.Padding(4);
            this.grpBody.Name = "grpBody";
            this.grpBody.Padding = new System.Windows.Forms.Padding(4);
            this.grpBody.Size = new System.Drawing.Size(712, 392);
            this.grpBody.TabIndex = 10;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "Body";
            // 
            // txtRequestBody
            // 
            this.txtRequestBody.AcceptsReturn = true;
            this.txtRequestBody.AcceptsTab = true;
            this.txtRequestBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestBody.Location = new System.Drawing.Point(4, 19);
            this.txtRequestBody.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestBody.Multiline = true;
            this.txtRequestBody.Name = "txtRequestBody";
            this.txtRequestBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestBody.Size = new System.Drawing.Size(704, 369);
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
            this.pnlTopHalfOfRequest.Location = new System.Drawing.Point(4, 19);
            this.pnlTopHalfOfRequest.Name = "pnlTopHalfOfRequest";
            this.pnlTopHalfOfRequest.Size = new System.Drawing.Size(712, 194);
            this.pnlTopHalfOfRequest.TabIndex = 9;
            // 
            // pnlRequestMethodAndHeaders
            // 
            this.pnlRequestMethodAndHeaders.Controls.Add(this.grpHeaders);
            this.pnlRequestMethodAndHeaders.Controls.Add(this.grpHttpMethod);
            this.pnlRequestMethodAndHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRequestMethodAndHeaders.Location = new System.Drawing.Point(0, 41);
            this.pnlRequestMethodAndHeaders.Name = "pnlRequestMethodAndHeaders";
            this.pnlRequestMethodAndHeaders.Size = new System.Drawing.Size(712, 153);
            this.pnlRequestMethodAndHeaders.TabIndex = 6;
            // 
            // grpHeaders
            // 
            this.grpHeaders.Controls.Add(this.txtRequestHeaders);
            this.grpHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHeaders.Location = new System.Drawing.Point(187, 0);
            this.grpHeaders.Margin = new System.Windows.Forms.Padding(4);
            this.grpHeaders.Name = "grpHeaders";
            this.grpHeaders.Padding = new System.Windows.Forms.Padding(4);
            this.grpHeaders.Size = new System.Drawing.Size(525, 153);
            this.grpHeaders.TabIndex = 6;
            this.grpHeaders.TabStop = false;
            this.grpHeaders.Text = "Headers";
            // 
            // txtRequestHeaders
            // 
            this.txtRequestHeaders.AcceptsReturn = true;
            this.txtRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestHeaders.Location = new System.Drawing.Point(4, 19);
            this.txtRequestHeaders.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestHeaders.Multiline = true;
            this.txtRequestHeaders.Name = "txtRequestHeaders";
            this.txtRequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestHeaders.Size = new System.Drawing.Size(517, 130);
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
            this.grpHttpMethod.Margin = new System.Windows.Forms.Padding(4);
            this.grpHttpMethod.Name = "grpHttpMethod";
            this.grpHttpMethod.Padding = new System.Windows.Forms.Padding(4);
            this.grpHttpMethod.Size = new System.Drawing.Size(187, 153);
            this.grpHttpMethod.TabIndex = 5;
            this.grpHttpMethod.TabStop = false;
            this.grpHttpMethod.Text = "Method";
            // 
            // rbHttpPatch
            // 
            this.rbHttpPatch.AutoSize = true;
            this.rbHttpPatch.Location = new System.Drawing.Point(100, 23);
            this.rbHttpPatch.Name = "rbHttpPatch";
            this.rbHttpPatch.Size = new System.Drawing.Size(65, 21);
            this.rbHttpPatch.TabIndex = 7;
            this.rbHttpPatch.TabStop = true;
            this.rbHttpPatch.Text = "Patch";
            this.rbHttpPatch.UseVisualStyleBackColor = true;
            // 
            // rbHttpHead
            // 
            this.rbHttpHead.AutoSize = true;
            this.rbHttpHead.Location = new System.Drawing.Point(100, 81);
            this.rbHttpHead.Name = "rbHttpHead";
            this.rbHttpHead.Size = new System.Drawing.Size(63, 21);
            this.rbHttpHead.TabIndex = 6;
            this.rbHttpHead.TabStop = true;
            this.rbHttpHead.Text = "Head";
            this.rbHttpHead.UseVisualStyleBackColor = true;
            // 
            // rbHttpDelete
            // 
            this.rbHttpDelete.AutoSize = true;
            this.rbHttpDelete.Location = new System.Drawing.Point(9, 81);
            this.rbHttpDelete.Margin = new System.Windows.Forms.Padding(4);
            this.rbHttpDelete.Name = "rbHttpDelete";
            this.rbHttpDelete.Size = new System.Drawing.Size(70, 21);
            this.rbHttpDelete.TabIndex = 3;
            this.rbHttpDelete.Text = "Delete";
            this.rbHttpDelete.UseVisualStyleBackColor = true;
            // 
            // rbHttpOptions
            // 
            this.rbHttpOptions.AutoSize = true;
            this.rbHttpOptions.Location = new System.Drawing.Point(100, 52);
            this.rbHttpOptions.Margin = new System.Windows.Forms.Padding(4);
            this.rbHttpOptions.Name = "rbHttpOptions";
            this.rbHttpOptions.Size = new System.Drawing.Size(78, 21);
            this.rbHttpOptions.TabIndex = 5;
            this.rbHttpOptions.Text = "Options";
            this.rbHttpOptions.UseVisualStyleBackColor = true;
            // 
            // rbHttpPut
            // 
            this.rbHttpPut.AutoSize = true;
            this.rbHttpPut.Location = new System.Drawing.Point(8, 110);
            this.rbHttpPut.Margin = new System.Windows.Forms.Padding(4);
            this.rbHttpPut.Name = "rbHttpPut";
            this.rbHttpPut.Size = new System.Drawing.Size(50, 21);
            this.rbHttpPut.TabIndex = 4;
            this.rbHttpPut.Text = "Put";
            this.rbHttpPut.UseVisualStyleBackColor = true;
            // 
            // rbHttpGet
            // 
            this.rbHttpGet.AutoSize = true;
            this.rbHttpGet.Checked = true;
            this.rbHttpGet.Location = new System.Drawing.Point(8, 23);
            this.rbHttpGet.Margin = new System.Windows.Forms.Padding(4);
            this.rbHttpGet.Name = "rbHttpGet";
            this.rbHttpGet.Size = new System.Drawing.Size(52, 21);
            this.rbHttpGet.TabIndex = 1;
            this.rbHttpGet.TabStop = true;
            this.rbHttpGet.Text = "Get";
            this.rbHttpGet.UseVisualStyleBackColor = true;
            // 
            // rbHttpPost
            // 
            this.rbHttpPost.AutoSize = true;
            this.rbHttpPost.Location = new System.Drawing.Point(8, 52);
            this.rbHttpPost.Margin = new System.Windows.Forms.Padding(4);
            this.rbHttpPost.Name = "rbHttpPost";
            this.rbHttpPost.Size = new System.Drawing.Size(57, 21);
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
            this.pnlRequestUrlAndButtons.Name = "pnlRequestUrlAndButtons";
            this.pnlRequestUrlAndButtons.Size = new System.Drawing.Size(712, 41);
            this.pnlRequestUrlAndButtons.TabIndex = 5;
            // 
            // pnlUrl
            // 
            this.pnlUrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlUrl.Controls.Add(this.txtUrl);
            this.pnlUrl.Controls.Add(this.lblUrl);
            this.pnlUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUrl.Location = new System.Drawing.Point(0, 0);
            this.pnlUrl.Margin = new System.Windows.Forms.Padding(4);
            this.pnlUrl.Name = "pnlUrl";
            this.pnlUrl.Padding = new System.Windows.Forms.Padding(8);
            this.pnlUrl.Size = new System.Drawing.Size(565, 41);
            this.pnlUrl.TabIndex = 6;
            // 
            // txtUrl
            // 
            this.txtUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUrl.Location = new System.Drawing.Point(38, 8);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(4);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(519, 22);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.WordWrap = false;
            this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUrl.Location = new System.Drawing.Point(8, 8);
            this.lblUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(30, 17);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Url:";
            // 
            // btnRequestButtons
            // 
            this.btnRequestButtons.Controls.Add(this.btnClearRequest);
            this.btnRequestButtons.Controls.Add(this.btnSubmitRequest);
            this.btnRequestButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRequestButtons.Location = new System.Drawing.Point(565, 0);
            this.btnRequestButtons.Name = "btnRequestButtons";
            this.btnRequestButtons.Size = new System.Drawing.Size(147, 41);
            this.btnRequestButtons.TabIndex = 5;
            // 
            // btnClearRequest
            // 
            this.btnClearRequest.AutoSize = true;
            this.btnClearRequest.Location = new System.Drawing.Point(79, 4);
            this.btnClearRequest.Name = "btnClearRequest";
            this.btnClearRequest.Size = new System.Drawing.Size(61, 27);
            this.btnClearRequest.TabIndex = 9;
            this.btnClearRequest.Text = "Clear";
            this.btnClearRequest.UseVisualStyleBackColor = true;
            this.btnClearRequest.Click += new System.EventHandler(this.btnClearRequest_Click);
            // 
            // btnSubmitRequest
            // 
            this.btnSubmitRequest.AutoSize = true;
            this.btnSubmitRequest.Location = new System.Drawing.Point(4, 4);
            this.btnSubmitRequest.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmitRequest.Name = "btnSubmitRequest";
            this.btnSubmitRequest.Size = new System.Drawing.Size(68, 27);
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
            this.grpResponse.Location = new System.Drawing.Point(4, 4);
            this.grpResponse.Margin = new System.Windows.Forms.Padding(6);
            this.grpResponse.Name = "grpResponse";
            this.grpResponse.Padding = new System.Windows.Forms.Padding(4);
            this.grpResponse.Size = new System.Drawing.Size(691, 609);
            this.grpResponse.TabIndex = 0;
            this.grpResponse.TabStop = false;
            this.grpResponse.Text = "Response";
            // 
            // tcResponse
            // 
            this.tcResponse.Controls.Add(this.tpResponseText);
            this.tcResponse.Controls.Add(this.tpResponseHeaders);
            this.tcResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcResponse.Location = new System.Drawing.Point(4, 50);
            this.tcResponse.Margin = new System.Windows.Forms.Padding(4);
            this.tcResponse.Name = "tcResponse";
            this.tcResponse.SelectedIndex = 0;
            this.tcResponse.Size = new System.Drawing.Size(683, 555);
            this.tcResponse.TabIndex = 6;
            // 
            // tpResponseText
            // 
            this.tpResponseText.Controls.Add(this.rtResponseText);
            this.tpResponseText.Location = new System.Drawing.Point(4, 25);
            this.tpResponseText.Margin = new System.Windows.Forms.Padding(4);
            this.tpResponseText.Name = "tpResponseText";
            this.tpResponseText.Padding = new System.Windows.Forms.Padding(4);
            this.tpResponseText.Size = new System.Drawing.Size(675, 526);
            this.tpResponseText.TabIndex = 0;
            this.tpResponseText.Text = "Body";
            this.tpResponseText.UseVisualStyleBackColor = true;
            // 
            // rtResponseText
            // 
            this.rtResponseText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtResponseText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtResponseText.Location = new System.Drawing.Point(4, 4);
            this.rtResponseText.Margin = new System.Windows.Forms.Padding(4);
            this.rtResponseText.Name = "rtResponseText";
            this.rtResponseText.ReadOnly = true;
            this.rtResponseText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtResponseText.Size = new System.Drawing.Size(667, 518);
            this.rtResponseText.TabIndex = 10;
            this.rtResponseText.Text = "";
            this.rtResponseText.WordWrap = false;
            this.rtResponseText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtResponseText_LinkClicked);
            // 
            // tpResponseHeaders
            // 
            this.tpResponseHeaders.Controls.Add(this.txtResponseHeaders);
            this.tpResponseHeaders.Location = new System.Drawing.Point(4, 25);
            this.tpResponseHeaders.Margin = new System.Windows.Forms.Padding(4);
            this.tpResponseHeaders.Name = "tpResponseHeaders";
            this.tpResponseHeaders.Padding = new System.Windows.Forms.Padding(4);
            this.tpResponseHeaders.Size = new System.Drawing.Size(675, 527);
            this.tpResponseHeaders.TabIndex = 1;
            this.tpResponseHeaders.Text = "Headers";
            this.tpResponseHeaders.UseVisualStyleBackColor = true;
            // 
            // txtResponseHeaders
            // 
            this.txtResponseHeaders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeaders.Location = new System.Drawing.Point(4, 4);
            this.txtResponseHeaders.Margin = new System.Windows.Forms.Padding(4);
            this.txtResponseHeaders.Multiline = true;
            this.txtResponseHeaders.Name = "txtResponseHeaders";
            this.txtResponseHeaders.ReadOnly = true;
            this.txtResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseHeaders.Size = new System.Drawing.Size(667, 519);
            this.txtResponseHeaders.TabIndex = 11;
            this.txtResponseHeaders.WordWrap = false;
            // 
            // pnlResponseStatusAndTime
            // 
            this.pnlResponseStatusAndTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlResponseStatusAndTime.Controls.Add(this.tableLayoutPanel1);
            this.pnlResponseStatusAndTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResponseStatusAndTime.Location = new System.Drawing.Point(4, 19);
            this.pnlResponseStatusAndTime.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResponseStatusAndTime.Name = "pnlResponseStatusAndTime";
            this.pnlResponseStatusAndTime.Size = new System.Drawing.Size(683, 31);
            this.pnlResponseStatusAndTime.TabIndex = 5;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1435, 28);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(232, 6);
            // 
            // exportResponseBodyToolStripMenuItem
            // 
            this.exportResponseBodyToolStripMenuItem.Name = "exportResponseBodyToolStripMenuItem";
            this.exportResponseBodyToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
            this.exportResponseBodyToolStripMenuItem.Text = "Export Response Body...";
            this.exportResponseBodyToolStripMenuItem.Click += new System.EventHandler(this.exportResponseBodyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(232, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 31);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlResponseStatus
            // 
            this.pnlResponseStatus.Controls.Add(this.lblResponseStatusValue);
            this.pnlResponseStatus.Controls.Add(this.lblResponseStatus);
            this.pnlResponseStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResponseStatus.Location = new System.Drawing.Point(3, 3);
            this.pnlResponseStatus.Name = "pnlResponseStatus";
            this.pnlResponseStatus.Size = new System.Drawing.Size(335, 25);
            this.pnlResponseStatus.TabIndex = 0;
            // 
            // pnlResponseTime
            // 
            this.pnlResponseTime.Controls.Add(this.lblResponseTimeValue);
            this.pnlResponseTime.Controls.Add(this.lblResponseTime);
            this.pnlResponseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResponseTime.Location = new System.Drawing.Point(344, 3);
            this.pnlResponseTime.Name = "pnlResponseTime";
            this.pnlResponseTime.Size = new System.Drawing.Size(336, 25);
            this.pnlResponseTime.TabIndex = 1;
            // 
            // lblResponseStatus
            // 
            this.lblResponseStatus.AutoSize = true;
            this.lblResponseStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseStatus.Location = new System.Drawing.Point(0, 0);
            this.lblResponseStatus.Name = "lblResponseStatus";
            this.lblResponseStatus.Size = new System.Drawing.Size(52, 17);
            this.lblResponseStatus.TabIndex = 0;
            this.lblResponseStatus.Text = "Status:";
            // 
            // lblResponseStatusValue
            // 
            this.lblResponseStatusValue.AutoSize = true;
            this.lblResponseStatusValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseStatusValue.Location = new System.Drawing.Point(52, 0);
            this.lblResponseStatusValue.Name = "lblResponseStatusValue";
            this.lblResponseStatusValue.Size = new System.Drawing.Size(82, 17);
            this.lblResponseStatusValue.TabIndex = 1;
            this.lblResponseStatusValue.Text = "statusValue";
            // 
            // lblResponseTime
            // 
            this.lblResponseTime.AutoSize = true;
            this.lblResponseTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseTime.Location = new System.Drawing.Point(0, 0);
            this.lblResponseTime.Name = "lblResponseTime";
            this.lblResponseTime.Size = new System.Drawing.Size(43, 17);
            this.lblResponseTime.TabIndex = 0;
            this.lblResponseTime.Text = "Time:";
            // 
            // lblResponseTimeValue
            // 
            this.lblResponseTimeValue.AutoSize = true;
            this.lblResponseTimeValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResponseTimeValue.Location = new System.Drawing.Point(43, 0);
            this.lblResponseTimeValue.Name = "lblResponseTimeValue";
            this.lblResponseTimeValue.Size = new System.Drawing.Size(70, 17);
            this.lblResponseTimeValue.TabIndex = 1;
            this.lblResponseTimeValue.Text = "timeValue";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSubmitRequest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 645);
            this.Controls.Add(this.splitterMain);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "RestSharp GUI";
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
            this.tpResponseHeaders.ResumeLayout(false);
            this.tpResponseHeaders.PerformLayout();
            this.pnlResponseStatusAndTime.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlResponseStatus.ResumeLayout(false);
            this.pnlResponseStatus.PerformLayout();
            this.pnlResponseTime.ResumeLayout(false);
            this.pnlResponseTime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitterMain;
        private System.Windows.Forms.GroupBox grpRequest;
        private System.Windows.Forms.GroupBox grpResponse;
        private System.Windows.Forms.TabControl tcResponse;
        private System.Windows.Forms.TabPage tpResponseText;
        private System.Windows.Forms.RichTextBox rtResponseText;
        private System.Windows.Forms.TabPage tpResponseHeaders;
        private System.Windows.Forms.TextBox txtResponseHeaders;
        private System.Windows.Forms.Panel pnlResponseStatusAndTime;
        private System.Windows.Forms.Panel pnlTopHalfOfRequest;
        private System.Windows.Forms.Panel pnlRequestMethodAndHeaders;
        private System.Windows.Forms.GroupBox grpHeaders;
        private System.Windows.Forms.TextBox txtRequestHeaders;
        private System.Windows.Forms.GroupBox grpHttpMethod;
        private System.Windows.Forms.RadioButton rbHttpGet;
        private System.Windows.Forms.RadioButton rbHttpPost;
        private System.Windows.Forms.Panel pnlRequestUrlAndButtons;
        private System.Windows.Forms.Panel pnlUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Panel btnRequestButtons;
        private System.Windows.Forms.Button btnClearRequest;
        private System.Windows.Forms.Button btnSubmitRequest;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TextBox txtRequestBody;
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

    }
}

