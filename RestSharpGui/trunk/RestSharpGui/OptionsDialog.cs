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

        void pgridOptions_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            var settings = Settings.Default;
            switch(e.ChangedItem.PropertyDescriptor.Name) {
                case "SplitterDistancePercent": {
                    var value = (ushort)e.ChangedItem.Value;
                    if (value > 100) {
                        settings.SplitterDistancePercent = (ushort) e.OldValue;
                        showPropertyValidationError(string.Format("SplitterDistancePercent must be between 0 and 100 inclusive but was {0}", value));
                        goto Cancel;
                    }
                    return;
                }
                case "DefaultRequestFilePath": {
                    var value = (string)e.ChangedItem.Value;
                    if (!String.IsNullOrWhiteSpace(value) && !File.Exists(value)) {
                        settings.DefaultRequestFilePath = (string)e.OldValue;
                        showPropertyValidationError(string.Format("DefaultRequestFilePath does not exist"));
                        goto Cancel;
                    }
                    return;
                }
                default: return;
            }

            Cancel: {
                pgridOptions.Refresh();
            }
        }

        private void showPropertyValidationError(string msg) {
            MessageBox.Show(this, msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
