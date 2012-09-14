using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Swensen.Ior.Core;
using Swensen.Ior.Properties;
using System.IO;

namespace Swensen.Ior.Forms {
    public partial class SettingsDialog : Form {
        public SettingsDialog() {
            InitializeComponent();
        }

        private void frmOptionsDialog_Load(object sender, EventArgs e) {
            this.pgridOptions.PropertyValueChanged += new PropertyValueChangedEventHandler(pgridOptions_PropertyValueChanged);
            pgridOptions.SelectedObject = new SettingsViewModel(Settings.Default);
            pgridOptions.AutoSizeProperties = true;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Settings.Default.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); //n.b. we call Settings.Default.Reload(); in the FormClosing event
        }

        //todo this validation is adhoc, get plugged in with TypeConverters etc.
        void pgridOptions_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            var vm = pgridOptions.SelectedObject as SettingsViewModel;

            var lastValidationError = vm.DequeueLastValidationError();
            if(e.ChangedItem.PropertyDescriptor.Name == lastValidationError.Item1) {
                showPropertyValidationWarning(lastValidationError.Item2);
                pgridOptions.Refresh();
            }
        }

        private void showPropertyValidationWarning(string msg) {
            MessageBox.Show(this, msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void SettingsDialog_FormClosing(object sender, FormClosingEventArgs e) {
            if(e.CloseReason == CloseReason.UserClosing && this.DialogResult != System.Windows.Forms.DialogResult.OK)
                Settings.Default.Reload();
        }
    }
}
