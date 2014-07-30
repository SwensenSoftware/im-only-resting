using System;
using System.IO;
using Swensen.Utils;
using NLog;

namespace Swensen.Ior.Properties {
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class HistorySettings {
        private static Logger log = LogManager.GetCurrentClassLogger();
        
        public HistorySettings() { }

        public void UpgradeAndSaveIfNeeded() {
            if (this.CallUpgrade) {
                log.Info("Upgrading history settings");
                this.Upgrade();
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
