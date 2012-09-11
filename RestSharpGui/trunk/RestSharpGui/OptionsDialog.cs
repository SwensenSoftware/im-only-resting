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
using System.IO;

namespace Swensen.RestSharpGui {
    public partial class OptionsDialog : Form {
        public OptionsDialog() {
            InitializeComponent();
        }

        private void frmOptionsDialog_Load(object sender, EventArgs e) {
            this.pgridOptions.PropertyValueChanged += new PropertyValueChangedEventHandler(pgridOptions_PropertyValueChanged);
            pgridOptions.SelectedObject = new SettingsViewModel(Settings.Default);
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

        //todo this validation is adhoc, get plugged in with TypeConverters etc.
        void pgridOptions_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            var vm = pgridOptions.SelectedObject as SettingsViewModel;

            var lastValidationError = vm.DequeueLastValidationError();
            if(e.ChangedItem.PropertyDescriptor.Name == lastValidationError.Item1) {
                showPropertyValidationError(lastValidationError.Item2);
                pgridOptions.Refresh();
            }
        }

        private void showPropertyValidationError(string msg) {
            MessageBox.Show(this, msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
