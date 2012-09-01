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

namespace Swensen.RestSharpGui {
    public enum InferredContentType {
        Xml, Html, Json, Text, Css, Csv, Javascript, Other
    }

    public static class InferredContentTypeUtils {
        public static InferredContentType FromContentType(string contentType) {
            if (contentType == null)
                return InferredContentType.Other;

            if (contentType == "text/html" || contentType == "application/xhtml+xml")
                return InferredContentType.Html;

            if (contentType == "text/xml" || contentType == "application/xml" || contentType.EndsWith("+xml")) //+xml catch all must come after Html
                return InferredContentType.Xml;

            if (contentType == "text/json" || contentType == "application/json")
                return InferredContentType.Json;

            if (contentType == "text/csv")
                return InferredContentType.Csv;

            if (contentType == "text/css")
                return InferredContentType.Css;

            if (contentType == "text/plain")
                return InferredContentType.Text;

            if (contentType == "text/javascript" || contentType == "application/javascript" || contentType == "application/x-javascript")
                return InferredContentType.Javascript;


            return InferredContentType.Other;
        }

        public static string FileExtension(InferredContentType ifc) {
            switch (ifc) {
                case InferredContentType.Css: return "css";
                case InferredContentType.Csv: return "csv";
                case InferredContentType.Html: return "html";
                case InferredContentType.Javascript: return "js";
                case InferredContentType.Json: return "json";
                case InferredContentType.Text: return "txt";
                case InferredContentType.Xml: return "xml";
                default: return "";
            }
        }
    }
}