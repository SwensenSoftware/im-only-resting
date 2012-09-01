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