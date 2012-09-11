using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Swensen.RestSharpGui {

    //possible attributes
    //[Category("Test")]
    //[DisplayName("Test Property")]
    //[Description("This is the description that shows up")]

    /// <summary>
    /// View model for Settings to bind to the property grid.
    /// </summary>
    public class SettingsViewModel {
        [DisplayName("Proxy Server")]
        [Description("Proxy server used by requests. If blank, no proxy server is used.")]
        public string ProxyServer { get; set; }

        [DisplayName("Default Request File Path")]
        [Description("The path for the default request file loaded upon startup.")]
        public string DefaultRequestFilePath { get; set; }

        [DisplayName("Save Request File Dialog Folder")]
        [Description("The default folder set for the Save Request file dialog. This location gets overwritten automatically whenever a request is saved to a different location.")]
        public string SaveRequestFileDialogFolder { get; set; }

        [DisplayName("Export Response Body File Dialog Folder")]
        [Description("The default folder set for the Export Response Body file dialog. This location gets overwritten automatically whenever a request body is saved to a different location.")]
        public string ExportResponseFileDialogFolder { get; set; }

        [DisplayName("Default Request Content-Type")]
        [Description("The default request Content-Type used when none is otherwise explicitly specified.")]
        public string DefaultRequestContentType { get; set; }
    }
}
