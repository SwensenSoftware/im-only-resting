using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Swensen.Ior.Properties;
using System.IO;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Net.Mime;
using Swensen.Utils;

namespace Swensen.Ior.Core {

    /// <summary>
    /// View model for Settings to bind to the property grid.
    /// </summary>
    public class SettingsViewModel {
        private readonly Settings settings;

        public SettingsViewModel(Settings settings) {
            this.settings = settings;            
        }

        //[Browsable(false)]
        private Tuple<string, string> lastValidationError = Tuple.Create("","");

        /// <summary>
        /// Return the last validation error, or Tuple.Create("","") if none, and reset the last validation error to Tuple.Create("","")
        /// </summary>
        public Tuple<string, string> DequeueLastValidationError() {
            var temp = lastValidationError;
            lastValidationError = Tuple.Create("","");
            return temp;
        }

        //possible have PeakLastValidationError

        [Category("Request")]
        [DisplayName("Save Request File Dialog Folder")]
        [Description("The default folder set for the Save Request file dialog. This location gets overwritten automatically whenever a request is saved to a different location.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string SaveRequestFileDialogFolder {
            get { return settings.SaveRequestFileDialogFolder; }
            set { 
                if (!value.IsBlank() && !Directory.Exists(value))
                    lastValidationError = Tuple.Create("SaveRequestFileDialogFolder", "Specified directory does not exist");
                else
                    settings.SaveRequestFileDialogFolder = (value ?? "").Trim(); 
            }
        }

        [Category("Request")]
        [DisplayName("Default Request File Path")]
        [Description("The path for the default request file loaded upon startup.")]
        [EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string DefaultRequestFilePath {
            get { return settings.DefaultRequestFilePath; }
            set {
                if (!value.IsBlank() && !File.Exists(value))
                    lastValidationError = Tuple.Create("DefaultRequestFilePath", "Specified file does not exist");
                else
                    settings.DefaultRequestFilePath = (value ?? "").Trim();
            }
        }

        [Category("Request")]
        [DisplayName("Default Request Content-Type")]
        [Description("The default request Content-Type used when none is otherwise explicitly specified.")]
        public string DefaultRequestContentType {
            get { return settings.DefaultRequestContentType; }
            set {
                if (value.IsBlank())
                    settings.DefaultRequestContentType = "";
                else {
                    try {
                        var ct = new ContentType(value);
                        settings.DefaultRequestContentType = ct.ToString();
                    } catch {
                        lastValidationError = Tuple.Create("DefaultRequestContentType", "Content-Type is invalid");    
                    }
                }
            }
        }

        [Category("Request")]
        [DisplayName("Encode UTF-8 Content with BOM")]
        [Description("Indicates whether or not UTF-8 content should be encoded with the optional BOM.")]
        public bool IncludeBomInUtf8RequestContent {
            get { return settings.IncludeBomInUtf8RequestContent; }
            set { settings.IncludeBomInUtf8RequestContent = value; }
        }

        [Category("Request")]
        [DisplayName("Proxy Server")]
        [Description("Proxy server used by requests. If blank, no proxy server is used.")]
        public string ProxyServer {
            get { return settings.ProxyServer; }
            set {
                Uri url = null;
                if(value.IsBlank())
                    settings.ProxyServer = "";
                else { 
                    var forgivingUrl = value.Contains("://") ? value : "http://" + value;
                    if (Uri.TryCreate(forgivingUrl, UriKind.Absolute, out url))
                        settings.ProxyServer = url.ToString();
                    else
                        lastValidationError = Tuple.Create("ProxyServer", "The given URL is invalid");
                }
            }
        }

        [Category("Response")]
        [DisplayName("Export Response Body File Dialog Folder")]
        [Description("The default folder set for the Export Response Body file dialog. This location gets overwritten automatically whenever a request body is saved to a different location.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string ExportResponseFileDialogFolder {
            get { return settings.ExportResponseFileDialogFolder; }
            set {
                if (!value.IsBlank() && !Directory.Exists(value))
                    lastValidationError = Tuple.Create("ExportResponseFileDialogFolder", "Specified directory does not exist");
                else
                    settings.ExportResponseFileDialogFolder = (value ?? "").Trim();
            }
        }

        [Category("Response")]
        [DisplayName("Follow Redirects")]
        [Description("Indicates whether or not redirects should be followed.")]
        public bool FollowRedirects {
            get { return settings.FollowRedirects; }
            set { settings.FollowRedirects = value; }
        }

        [Category("Response")]
        [DisplayName("Ignore SSL Validation Errors")]
        [Description("Indicates whether or not SSL validation errors should be ignored.")]
        public bool IgnoreSslValidationErrors {
            get { return settings.IgnoreSslValidationErrors; }
            set { settings.IgnoreSslValidationErrors = value; }
        }

        [Category("Response")]
        [DisplayName("Enable Automatic Content Decompression")]
        [Description("Indicates whether or not gzip and deflate compressed content should be automatically decompressed.")]
        public bool EnableAutomaticContentDecompression {
            get { return settings.EnableAutomaticContentDecompression; }
            set { settings.EnableAutomaticContentDecompression = value; }
        }

        [Category("User Interface")]
        [DisplayName("Max History")]
        [Description("The maximum request history and response snapshots to store.")]
        public ushort MaxSnapshots {
            get { return settings.MaxSnapshots; }
            set { settings.MaxSnapshots = value; }
        }
    }
}
