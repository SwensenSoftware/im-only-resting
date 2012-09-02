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

        //todo this validation is adhoc, get plugged in with TypeConverters etc.
        void pgridOptions_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            var settings = Settings.Default;
            switch(e.ChangedItem.PropertyDescriptor.Name) {
                case "SplitterDistancePercent": {
                    var value = (ushort)e.ChangedItem.Value;
                    if (value > 100) {
                        showPropertyValidationError(string.Format("SplitterDistancePercent must be between 0 and 100 inclusive but was {0}", value));
                        settings.SplitterDistancePercent = (ushort)e.OldValue; //so user can see it in background, wait to change till after dialog shown
                        goto Cancel;
                    }
                    return;
                }
                case "DefaultRequestFilePath": {
                    var value = (string)e.ChangedItem.Value;
                    if (!String.IsNullOrWhiteSpace(value) && !File.Exists(value)) {
                        showPropertyValidationError(string.Format("DefaultRequestFilePath specified file does not exist"));
                        settings.DefaultRequestFilePath = (string)e.OldValue;
                        goto Cancel;
                    }
                    return;
                }
                case "SaveRequestFileDialogFolder": {
                    var value = (string)e.ChangedItem.Value;
                    if (!String.IsNullOrWhiteSpace(value) && !Directory.Exists(value)) {
                        showPropertyValidationError(string.Format("SaveRequestFileDialogFolder specified directory does not exist"));
                        settings.SaveRequestFileDialogFolder = (string)e.OldValue;
                        goto Cancel;
                    }
                    return;
                }
                case "ExportResponseFileDialogFolder": {
                    var value = (string)e.ChangedItem.Value;
                    if (!String.IsNullOrWhiteSpace(value) && !Directory.Exists(value)) {
                        showPropertyValidationError(string.Format("ExportResponseFileDialogFolder specified directory does not exist"));
                        settings.ExportResponseFileDialogFolder = (string)e.OldValue;
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
