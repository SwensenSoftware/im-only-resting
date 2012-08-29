using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Swensen.RestSharpGui {
    public class RequestViewModel {
        public RequestViewModel() {
            Headers = new string[0];
        }

        public string Url { get; set; }
        public Method? Method { get; set; }
        public string[] Headers { get; set; }
        public string Body { get; set; }
    }
}
