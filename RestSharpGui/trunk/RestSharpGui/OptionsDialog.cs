using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Swensen.RestSharpGui.Properties;

namespace Swensen.RestSharpGui {
    public partial class OptionsDialog : Form {
        public OptionsDialog() {
            InitializeComponent();
        }

        private void frmOptionsDialog_Load(object sender, EventArgs e) {
            pgridOptions.SelectedObject = Settings.Default;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Settings.Default.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Settings.Default.Reload();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
