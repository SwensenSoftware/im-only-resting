using System;
using System.IO;
using Swensen.Utils;

namespace Swensen.Ior.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {
        
        public Settings() {
            SetupCalculatedDefaultSettings();
        }

        private void SetupCalculatedDefaultSettings() {
            if (this.SaveRequestFileDialogFolder.IsBlank())
                this.SaveRequestFileDialogFolder =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Http Saved Requests";

            if (this.ExportResponseFileDialogFolder.IsBlank())
                this.ExportResponseFileDialogFolder =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Http Exported Responses";
        }

        public void UpgradeAndSaveIfNeeded() {
            if (this.CallUpgrade) {
                this.Upgrade();
                this.SetupCalculatedDefaultSettings();
                this.CallUpgrade = false;
                this.Save();
            }
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
