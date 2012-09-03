/*
Copyright 2012 Stephen Swensen

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using TidyManaged;

namespace Swensen.RestSharpGui {
    public class ResponseViewModel {
        /// <summary>
        /// Create an empty ResponseViewModel with ResponseStatus set to a loading message.
        /// </summary>
        public ResponseViewModel(string status="") {
            this.Status = status;
            initLazyFields();
        }

        /// <summary>
        /// Create a ResponseViewModel populated from an IRestResonse
        /// </summary>
        public ResponseViewModel(IRestResponse response, DateTime start, DateTime end) {
            if (response == null)
                throw new ArgumentNullException("response");

            this.Status = response.ResponseStatus == RestSharp.ResponseStatus.Completed ?
                          string.Format("{0} {1}", (int)response.StatusCode, response.StatusDescription) :
                          response.ResponseStatus.ToString();

            if (start != null && end != null)
                ElapsedTime = (end - start).Milliseconds + " ms";

            this.Content = response.Content;
            this.ContentBytes = response.RawBytes;
            this.ContentTypeWithoutCharset = extractCharsetlessContentType(response.ContentType);
            this.Headers = String.Join(Environment.NewLine, response.Headers.Select(p => p.Name + ": " + p.Value));

            initLazyFields();            
        }

        private void initLazyFields() {
            this.inferredContentType = new Lazy<InferredContentType>(() => InferredContentTypeUtils.FromContentType(this.ContentTypeWithoutCharset));
            this.prettyPrintedContent = new Lazy<string>(() => prettyPrint(this.InferredContentType, this.Content));
        }

        public string ElapsedTime { get; private set; }

        private string extractCharsetlessContentType(string contentType) {
            //ContentType spec: http://www.w3.org/Protocols/rfc2616/rfc2616-sec3.html#sec3.7
            //media-type     = type "/" subtype *( ";" parameter )
            //type           = token
            //subtype        = token
            
            if (String.IsNullOrWhiteSpace(contentType))
                return contentType;

            return contentType.Split(';')[0].Trim();
        }

        public string Status { get; private set; }

        /// <summary>
        /// The content type with charset excluded if it was present on the header.
        /// </summary>
        public string ContentTypeWithoutCharset { get; private set; }

        private Lazy<InferredContentType> inferredContentType;
        public InferredContentType InferredContentType { get { return inferredContentType.Value; } }

        public byte[] ContentBytes { get; private set; }
        public string Content { get; private set; }
        
        private Lazy<string> prettyPrintedContent;
        public string PrettyPrintedContent { get { return prettyPrintedContent.Value; } }

        public string Headers { get; set; }

        /// <summary>
        /// If contentType is an xml content type, then try to pretty print the rawContent. If that fails or otherwise, just return the rawContent
        /// </summary>
        private static string prettyPrint(InferredContentType contentType, string content) {
            //see http://stackoverflow.com/a/2965701/236255 for list of xml content types (credit to http://stackoverflow.com/users/18936/bobince)
            try {
                switch (contentType) {
                    case InferredContentType.Xml: {
                        var doc = XDocument.Parse(content);
                        var xml = doc.ToString();
                        if (doc.Declaration != null)
                            return doc.Declaration.ToString() + Environment.NewLine + xml;
                        else
                            return xml;
                    }
                    case InferredContentType.Json: {
                        dynamic parsedJson = JsonConvert.DeserializeObject(content);
                        return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                    }
                    case InferredContentType.Html: {
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
    }
}
