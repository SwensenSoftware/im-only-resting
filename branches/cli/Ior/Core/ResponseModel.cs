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
using System.Net.Http;
using Swensen.Utils;

namespace Swensen.Ior.Core {
    public class ResponseModel {
        public IorContentType ContentType { get; private set;}

        /// <summary>
        /// Create an empty ResponseModel with ResponseStatus set to a loading message.
        /// </summary>
        public ResponseModel(string status="") {
            this.Status = status;
            this.ContentType = new IorContentType();
            initLazyFields(new Uri("http://localhost"));
        }

        public ResponseModel(String errorMessage, DateTime start, DateTime end) : this() {
            ErrorMessage = errorMessage;
            Start = start;
            End = end;
        }

        /// <summary>
        /// Create a ResponseModel populated from an IRestResonse
        /// </summary>
        public ResponseModel(HttpResponseMessage response, DateTime start, DateTime end) {
            if (response == null)
                throw new ArgumentNullException("response");

            this.Status = string.Format("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);

            Start = start;
            End = end;

            var readContentBytesTask = response.Content.ReadAsByteArrayAsync();
            readContentBytesTask.Wait();
            this.ContentBytes = readContentBytesTask.Result;

            var readContentStringTask = response.Content.ReadAsStringAsync();
            readContentStringTask.Wait();
            this.Content = readContentStringTask.Result;

            var headers = response.Headers.Concat(response.Content.Headers);

            this.Headers = headers.Select(p => p.Key + ": " + p.Value.Coalesce().Join(", ")).Join(Environment.NewLine);

            var contentType = headers.FirstOrDefault(x => String.Equals(x.Key, "content-type", StringComparison.OrdinalIgnoreCase)).Value.Coalesce().Join(", ");
            this.ContentType = new IorContentType(contentType);

            this.ErrorMessage = null;

            initLazyFields(response.RequestMessage.RequestUri);            
        }

        private void initLazyFields(Uri requestUri) {
            this.prettyPrintedContent = new Lazy<string>(() => IorContentType.GetPrettyPrintedContent(this.ContentType.MediaTypeCategory, this.Content));
            this.contentFileExtension = new Lazy<string>(() => IorContentType.GetFileExtension(this.ContentType.MediaTypeCategory, this.ContentType.MediaType, requestUri));
            this.temporaryFile = new Lazy<string>(() => FileUtils.CreateTempFile(this.ContentBytes, this.ContentFileExtension));
        }

        public string ErrorMessage { get; private set; }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public long ElapsedMilliseconds { get { return (long) (End - Start).TotalMilliseconds; } }

        public string Status { get; private set; }

        public byte[] ContentBytes { get; private set; }
        public string Content { get; private set; }
        public string Headers { get; set; }

        private Lazy<string> prettyPrintedContent;
        public string PrettyPrintedContent { get { return prettyPrintedContent.Value; } }

        private Lazy<string> contentFileExtension;
        public string ContentFileExtension { get { return contentFileExtension.Value; } }

        private Lazy<string> temporaryFile;
        public string TemporaryFile { get { return temporaryFile.Value; } }

        public static ResponseModel Loading = new ResponseModel(status:"Loading...");
        public static ResponseModel Empty = new ResponseModel();
    }
}
