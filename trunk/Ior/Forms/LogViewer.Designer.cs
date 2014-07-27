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
            this.txtLogViewer = new Swensen.Ior.Forms.StandardScintilla();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogViewer)).BeginInit();
            this.SuspendLayout();
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
            this.txtLogViewer.Location = new System.Drawing.Point(0, 0);
            this.txtLogViewer.Name = "txtLogViewer";
            this.txtLogViewer.Size = new System.Drawing.Size(622, 297);
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
            this.txtLogViewer.TabIndex = 0;
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 297);
            this.Controls.Add(this.txtLogViewer);
            this.Name = "LogViewer";
            this.Text = "LogViewer";
            this.Load += new System.EventHandler(this.LogViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLogViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private StandardScintilla txtLogViewer;
    }
}