namespace Swensen.Ior.Forms
{
    partial class LogViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer));
            this.filterPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFatal = new System.Windows.Forms.CheckBox();
            this.cbError = new System.Windows.Forms.CheckBox();
            this.cbWarn = new System.Windows.Forms.CheckBox();
            this.cbInfo = new System.Windows.Forms.CheckBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.cbTrace = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtLogViewer = new Swensen.Ior.Forms.StandardScintilla();
            this.filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // filterPanel
            // 
            this.filterPanel.AutoSize = true;
            this.filterPanel.Controls.Add(this.label1);
            this.filterPanel.Controls.Add(this.cbFatal);
            this.filterPanel.Controls.Add(this.cbError);
            this.filterPanel.Controls.Add(this.cbWarn);
            this.filterPanel.Controls.Add(this.cbInfo);
            this.filterPanel.Controls.Add(this.cbDebug);
            this.filterPanel.Controls.Add(this.cbTrace);
            this.filterPanel.Controls.Add(this.btnRefresh);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(800, 26);
            this.filterPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log Levels:";
            // 
            // cbFatal
            // 
            this.cbFatal.AutoSize = true;
            this.cbFatal.Checked = true;
            this.cbFatal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFatal.Location = new System.Drawing.Point(71, 6);
            this.cbFatal.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbFatal.Name = "cbFatal";
            this.cbFatal.Size = new System.Drawing.Size(49, 17);
            this.cbFatal.TabIndex = 1;
            this.cbFatal.Text = "Fatal";
            this.cbFatal.UseVisualStyleBackColor = true;
            // 
            // cbError
            // 
            this.cbError.AutoSize = true;
            this.cbError.Checked = true;
            this.cbError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbError.Location = new System.Drawing.Point(126, 6);
            this.cbError.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbError.Name = "cbError";
            this.cbError.Size = new System.Drawing.Size(48, 17);
            this.cbError.TabIndex = 2;
            this.cbError.Text = "Error";
            this.cbError.UseVisualStyleBackColor = true;
            // 
            // cbWarn
            // 
            this.cbWarn.AutoSize = true;
            this.cbWarn.Checked = true;
            this.cbWarn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWarn.Location = new System.Drawing.Point(180, 6);
            this.cbWarn.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbWarn.Name = "cbWarn";
            this.cbWarn.Size = new System.Drawing.Size(52, 17);
            this.cbWarn.TabIndex = 3;
            this.cbWarn.Text = "Warn";
            this.cbWarn.UseVisualStyleBackColor = true;
            // 
            // cbInfo
            // 
            this.cbInfo.AutoSize = true;
            this.cbInfo.Location = new System.Drawing.Point(238, 6);
            this.cbInfo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbInfo.Name = "cbInfo";
            this.cbInfo.Size = new System.Drawing.Size(44, 17);
            this.cbInfo.TabIndex = 4;
            this.cbInfo.Text = "Info";
            this.cbInfo.UseVisualStyleBackColor = true;
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.Location = new System.Drawing.Point(288, 6);
            this.cbDebug.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(58, 17);
            this.cbDebug.TabIndex = 5;
            this.cbDebug.Text = "Debug";
            this.cbDebug.UseVisualStyleBackColor = true;
            // 
            // cbTrace
            // 
            this.cbTrace.AutoSize = true;
            this.cbTrace.Location = new System.Drawing.Point(352, 6);
            this.cbTrace.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbTrace.Name = "cbTrace";
            this.cbTrace.Size = new System.Drawing.Size(54, 17);
            this.cbTrace.TabIndex = 6;
            this.cbTrace.Text = "Trace";
            this.cbTrace.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(412, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(62, 20);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtLogViewer
            // 
            this.txtLogViewer.AutoComplete.FillUpCharacters = ".([";
            this.txtLogViewer.AutoComplete.IsCaseSensitive = false;
            this.txtLogViewer.AutoComplete.ListString = resources.GetString("txtLogViewer.AutoComplete.ListString");
            this.txtLogViewer.AutoComplete.SingleLineAccept = true;
            this.txtLogViewer.ConfigurationManager.Language = "js";
            this.txtLogViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogViewer.Indentation.IndentWidth = 2;
            this.txtLogViewer.Indentation.SmartIndentType = ScintillaNET.SmartIndent.CPP;
            this.txtLogViewer.Indentation.TabWidth = 4;
            this.txtLogViewer.Indentation.UseTabs = false;
            this.txtLogViewer.IsReadOnly = true;
            this.txtLogViewer.Lexing.Lexer = ScintillaNET.Lexer.Cpp;
            this.txtLogViewer.Lexing.LexerName = "cpp";
            this.txtLogViewer.Lexing.LineCommentPrefix = "//";
            this.txtLogViewer.Lexing.StreamCommentPrefix = "/* ";
            this.txtLogViewer.Lexing.StreamCommentSufix = " */";
            this.txtLogViewer.Location = new System.Drawing.Point(0, 26);
            this.txtLogViewer.Name = "txtLogViewer";
            this.txtLogViewer.Size = new System.Drawing.Size(800, 271);
            this.txtLogViewer.Styles.BraceBad.FontName = "Courier New";
            this.txtLogViewer.Styles.BraceBad.Size = 10F;
            this.txtLogViewer.Styles.BraceLight.Bold = true;
            this.txtLogViewer.Styles.BraceLight.FontName = "Courier New";
            this.txtLogViewer.Styles.BraceLight.ForeColor = System.Drawing.Color.Red;
            this.txtLogViewer.Styles.BraceLight.Size = 10F;
            this.txtLogViewer.Styles.CallTip.FontName = "Courier New";
            this.txtLogViewer.Styles.ControlChar.FontName = "Courier New";
            this.txtLogViewer.Styles.ControlChar.Size = 10F;
            this.txtLogViewer.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.txtLogViewer.Styles.Default.FontName = "Courier New";
            this.txtLogViewer.Styles.Default.Size = 10F;
            this.txtLogViewer.Styles.IndentGuide.FontName = "Courier New";
            this.txtLogViewer.Styles.IndentGuide.Size = 10F;
            this.txtLogViewer.Styles.LastPredefined.FontName = "Courier New";
            this.txtLogViewer.Styles.LastPredefined.Size = 10F;
            this.txtLogViewer.Styles.LineNumber.FontName = "Courier New";
            this.txtLogViewer.Styles.LineNumber.Size = 10F;
            this.txtLogViewer.Styles.Max.FontName = "Courier New";
            this.txtLogViewer.Styles.Max.Size = 10F;
            this.txtLogViewer.TabIndex = 4;
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 297);
            this.Controls.Add(this.txtLogViewer);
            this.Controls.Add(this.filterPanel);
            this.Name = "LogViewer";
            this.Text = "Log Viewer";
            this.Load += new System.EventHandler(this.LogViewer_Load);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel filterPanel;
        private StandardScintilla txtLogViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbFatal;
        private System.Windows.Forms.CheckBox cbError;
        private System.Windows.Forms.CheckBox cbWarn;
        private System.Windows.Forms.CheckBox cbInfo;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.CheckBox cbTrace;
        private System.Windows.Forms.Button btnRefresh;

    }
}