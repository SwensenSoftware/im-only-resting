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
        public ResponseViewModel() {
            ResponseStatus = "Loading...";
        }

        /// <summary>
        /// Create a ResponseViewModel populated from an IRestResonse
        /// </summary>
        public ResponseViewModel(IRestResponse response) {
            if (response == null)
                throw new ArgumentNullException("response");

            ResponseStatus = response.ResponseStatus == RestSharp.ResponseStatus.Completed ?
                             string.Format("{0} {1}", (int)response.StatusCode, response.StatusDescription) :
                             response.ResponseStatus.ToString();

            var contentType = response.ContentType;
            IsXmlContentType = contentType != null && (contentType == "text/xml" || contentType == "application/xml" || contentType.EndsWith("+xml"));

            PrettyPrintedContent = prettyPrint(response.ContentType, response.Content); //depends on IsXmlContentType

            Headers = String.Join(Environment.NewLine, response.Headers.Select(p => p.Name + ": " + p.Value));

        }

        public string ResponseStatus { get; set; }

        private bool IsXmlContentType { get; set; }

        public string PrettyPrintedContent { get; set; }

        public string Headers { get; set; }

        /// <summary>
        /// If contentType is an xml content type, then try to pretty print the rawContent. If that fails or otherwise, just return the rawContent
        /// </summary>
        private string prettyPrint(string contentType, string rawContent) {
            //see http://stackoverflow.com/a/2965701/236255 for list of xml content types (credit to http://stackoverflow.com/users/18936/bobince)
            if (this.IsXmlContentType) {
                try {
                    return XDocument.Parse(rawContent).ToString();
                } catch {
                    return rawContent;
                }
            } else
                return rawContent;
        }
    }
}
