namespace Swensen.Ior.Forms
{
	partial class AboutBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.linkDescription = new System.Windows.Forms.LinkLabel();
            this.okButton = new System.Windows.Forms.Button();
            this.btnPayPal = new System.Windows.Forms.Button();
            this.paypalImageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.linkDescription, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.okButton, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.btnPayPal, 0, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(334, 177);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelProductName
            // 
            this.labelProductName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelProductName.AutoSize = true;
            this.labelProductName.Location = new System.Drawing.Point(129, 6);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(75, 13);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(146, 31);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(42, 13);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(141, 56);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(51, 13);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkDescription
            // 
            this.linkDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkDescription.AutoSize = true;
            this.linkDescription.Location = new System.Drawing.Point(110, 81);
            this.linkDescription.Margin = new System.Windows.Forms.Padding(6);
            this.linkDescription.Name = "linkDescription";
            this.linkDescription.Size = new System.Drawing.Size(114, 13);
            this.linkDescription.TabIndex = 25;
            this.linkDescription.TabStop = true;
            this.linkDescription.Text = "Description (home link)";
            this.linkDescription.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDescription_LinkClicked);
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.AutoSize = true;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(256, 151);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // btnPayPal
            // 
            this.btnPayPal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPayPal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPayPal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPayPal.ImageIndex = 0;
            this.btnPayPal.ImageList = this.paypalImageList;
            this.btnPayPal.Location = new System.Drawing.Point(183, 113);
            this.btnPayPal.Name = "btnPayPal";
            this.btnPayPal.Size = new System.Drawing.Size(148, 32);
            this.btnPayPal.TabIndex = 26;
            this.btnPayPal.Text = "Pay What You Want";
            this.btnPayPal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayPal.UseVisualStyleBackColor = true;
            this.btnPayPal.Click += new System.EventHandler(this.btnPayPal_Click);
            // 
            // paypalImageList
            // 
            this.paypalImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("paypalImageList.ImageStream")));
            this.paypalImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.paypalImageList.Images.SetKeyName(0, "pp_cc_mark_37x23.jpg");
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(352, 195);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.LinkLabel linkDescription;
        private System.Windows.Forms.Button btnPayPal;
        private System.Windows.Forms.ImageList paypalImageList;
	}
}
