using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.Xml.Linq;

namespace Swensen.RestSharpGui {
    public class ResponseViewModel {
        private IRestResponse response;

        public ResponseViewModel(IRestResponse response) {
            this.response = response;
        }

        public string ResponseStatus {
            get {
                return response.ResponseStatus == RestSharp.ResponseStatus.Completed ? 
                       string.Format("{0} {1}", (int)response.StatusCode, response.StatusDescription) : 
                       response.ResponseStatus.ToString();
            }
        }

        private bool IsXmlContentType {
            get {
                var contentType = response.ContentType;
                return contentType != null && (contentType == "text/xml" || contentType == "application/xml" || contentType.EndsWith("+xml"));
            }
        }

        public string PrettyPrintedContent {
            get {
                return prettyPrint(response.ContentType, response.Content);
            }
        }

        public string Headers {
            get {
                return String.Join(Environment.NewLine, response.Headers.Select(p => p.Name + ": " + p.Value));
            }
        }

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
