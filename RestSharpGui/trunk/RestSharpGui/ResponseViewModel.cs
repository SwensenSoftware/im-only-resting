using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.Xml.Linq;

namespace Swensen.RestSharpGui {
    public class ResponseViewModel {
        /// <summary>
        /// Create an empty ResponseViewModel with ResponseStatus set to a loading message.
        /// </summary>
        public ResponseViewModel(string status="") {
            this.Status = status;
        }

        /// <summary>
        /// Create a ResponseViewModel populated from an IRestResonse
        /// </summary>
        public ResponseViewModel(IRestResponse response) {
            if (response == null)
                throw new ArgumentNullException("response");

            this.Status = response.ResponseStatus == RestSharp.ResponseStatus.Completed ?
                          string.Format("{0} {1}", (int)response.StatusCode, response.StatusDescription) :
                          response.ResponseStatus.ToString();

            this.Content = response.Content;
            this.ContentBytes = response.RawBytes;
            this.ContentType = response.ContentType;
            this.Headers = String.Join(Environment.NewLine, response.Headers.Select(p => p.Name + ": " + p.Value));
        }

        public string Status { get; private set; }

        public string ContentType { get; private set; }
        public InferredContentType InferredContentType { get { return InferredContentTypeUtils.FromContentType(ContentType); } }

        public byte[] ContentBytes { get; private set; }
        public string Content { get; private set; }
        public string PrettyPrintedContent { get { return prettyPrint(InferredContentType, Content); } }

        public string Headers { get; set; }

        /// <summary>
        /// If contentType is an xml content type, then try to pretty print the rawContent. If that fails or otherwise, just return the rawContent
        /// </summary>
        private static string prettyPrint(InferredContentType contentType, string content) {
            //see http://stackoverflow.com/a/2965701/236255 for list of xml content types (credit to http://stackoverflow.com/users/18936/bobince)
            switch (contentType) {
                case InferredContentType.Xml :
                    try {
                        return XDocument.Parse(content).ToString();
                    } catch {
                        return content;
                    }
                default:
                    return content;
            }
        }
    }
}
