using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;
using System.Xml.Linq;
using Newtonsoft.Json;
using TidyManaged;
using System.IO;

namespace Swensen.Ior.Core {
    /// <summary>
    /// Broad categories of http media types that we know how to do special processing for (i.e. pretty printing, or choosing correct file extension).
    /// </summary>
    public enum IorMediaTypeCategory {
        Xml, Html, Json, Text, Application, Other
    }
                 
    //http://www.w3.org/TR/html4/types.html#h-6.7
    /// <summary>
    /// An immutable wrapper around our underlying content type representation which provides specialized processing for various processing
    /// like pretty printing and choosing correct file extensions.
    /// </summary>
    public class IorContentType {
        private readonly ContentType ct;
        
        public readonly string MediaType;

        public readonly IorMediaTypeCategory MediaTypeCategory;

        public IorContentType() : this("application/octet-stream") { }

        public IorContentType(string contentType) {
            try {
                contentType = contentType.Split(',')[0]; //though illegal, we've since comma separated content-types
                this.ct = new ContentType(contentType);
            } catch {
                this.ct = new ContentType("application/octet-stream");
            }
            
            this.MediaType = ct.MediaType.ToLower();
            this.MediaTypeCategory = GetMediaTypeCategory(this.MediaType);
        }

        public static IorMediaTypeCategory GetMediaTypeCategory(string mt) { 
            if (mt == "text/html" || mt == "application/xhtml+xml")
                return IorMediaTypeCategory.Html;

            if (mt == "text/xml" || mt == "application/xml" || mt.EndsWith("+xml")) //+xml catch all must come after Html
                return IorMediaTypeCategory.Xml;

            if (mt == "text/json" || mt == "application/json")
                return IorMediaTypeCategory.Json;

            if (mt == "text/plain")
                return IorMediaTypeCategory.Text;

            if (mt.StartsWith("image/") || mt.StartsWith("video/") || mt.StartsWith("audio/"))
                return IorMediaTypeCategory.Application;

            return IorMediaTypeCategory.Other;
        }

        /// <summary>
        /// If contentType is an xml content type, then try to pretty print the rawContent. If that fails or otherwise, just return the rawContent
        /// </summary>
        public static string GetPrettyPrintedContent(IorMediaTypeCategory mtc, string content) {
            //see http://stackoverflow.com/a/2965701/236255 for list of xml content types (credit to http://stackoverflow.com/users/18936/bobince)
            try {
                switch (mtc) {
                    case IorMediaTypeCategory.Xml: {
                            var doc = XDocument.Parse(content);
                            var xml = doc.ToString();
                            if (doc.Declaration != null)
                                return doc.Declaration.ToString() + Environment.NewLine + xml;
                            else
                                return xml;
                        }
                    case IorMediaTypeCategory.Json: {
                            dynamic parsedJson = JsonConvert.DeserializeObject(content);
                            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                        }
                    case IorMediaTypeCategory.Html: {
                            //need to convert to utf16-little endian stream and set Document input/output encoding since Document.FromString screws up encoding.
                            var stream = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(content));
                            using (var doc = Document.FromStream(stream)) {
                                doc.InputCharacterEncoding = EncodingType.Utf16LittleEndian;
                                doc.OutputCharacterEncoding = EncodingType.Utf16LittleEndian;
                                doc.ShowWarnings = false;
                                doc.Quiet = true;
                                doc.OutputXhtml = false;
                                doc.OutputXml = false;
                                doc.OutputHtml = false;
                                doc.IndentBlockElements = AutoBool.Yes;
                                doc.IndentSpaces = 4;
                                doc.IndentAttributes = false;
                                //doc.IndentCdata = true;
                                doc.AddVerticalSpace = true;
                                doc.AddTidyMetaElement = false;
                                doc.WrapAt = 120;

                                doc.MergeDivs = AutoBool.No;
                                doc.MergeSpans = AutoBool.No;
                                doc.JoinStyles = false;
                                doc.ForceOutput = true;
                                doc.CleanAndRepair();

                                string output = doc.Save();
                                return output;
                            }
                        }
                    default:
                        return content;
                }
            } catch {
                return content;
            }
        }

        public static string GetFileExtension(IorMediaTypeCategory mtc, string mt) {
            switch (mtc) {
                case IorMediaTypeCategory.Html: return "html";
                case IorMediaTypeCategory.Json: return "json";
                case IorMediaTypeCategory.Text: return "txt";
                case IorMediaTypeCategory.Xml: return "xml";
                case IorMediaTypeCategory.Application:
                    var parts = mt.Split('/');
                    return parts[1];
                default:
                    switch(mt) {
                        case "text/csv": return "csv";
                        case "text/css": return "css";
                        case "text/ecmascript":
                        case "text/javascript":
                        case "application/javascript":
                        case "application/x-javascript":
                            return "js";
                        default: return "";
                    }
            }
        }
    }                                                                        
}
