namespace Swensen.Ior.Forms {
    partial class SettingsDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pgridOptions = new Swensen.Ior.Forms.PropertyGridEx();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 324);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(627, 28);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Location = new System.Drawing.Point(575, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Location = new System.Drawing.Point(533, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(42, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pgridOptions
            // 
            this.pgridOptions.CommandsVisibleIfAvailable = false;
            // 
            // 
            // 
            this.pgridOptions.DocCommentDescription.Location = new System.Drawing.Point(3, 18);
            this.pgridOptions.DocCommentDescription.Name = "";
            this.pgridOptions.DocCommentDescription.Size = new System.Drawing.Size(621, 37);
            this.pgridOptions.DocCommentDescription.TabIndex = 1;
            this.pgridOptions.DocCommentImage = null;
            // 
            // 
            // 
            this.pgridOptions.DocCommentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgridOptions.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.pgridOptions.DocCommentTitle.Name = "";
            this.pgridOptions.DocCommentTitle.Size = new System.Drawing.Size(621, 15);
            this.pgridOptions.DocCommentTitle.TabIndex = 0;
            this.pgridOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgridOptions.Location = new System.Drawing.Point(0, 0);
            this.pgridOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pgridOptions.Name = "pgridOptions";
            this.pgridOptions.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.pgridOptions.Size = new System.Drawing.Size(627, 324);
            this.pgridOptions.TabIndex = 1;
            this.pgridOptions.ToolbarVisible = false;
            // 
            // 
            // 
            this.pgridOptions.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pgridOptions.ToolStrip.Name = "";
            this.pgridOptions.ToolStrip.Size = new System.Drawing.Size(875, 25);
            this.pgridOptions.ToolStrip.TabIndex = 1;
            this.pgridOptions.ToolStrip.Visible = false;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 352);
            this.Controls.Add(this.pgridOptions);
            this.Controls.Add(this.pnlButtons);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsDialog_FormClosing);
            this.Load += new System.EventHandler(this.frmOptionsDialog_Load);
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private Swensen.Ior.Forms.PropertyGridEx pgridOptions;
    }
}