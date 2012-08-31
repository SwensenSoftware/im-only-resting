using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Swensen.RestSharpGui {
    public class RequestViewModel {
        public string Url { get; set; }
        public Method? Method { get; set; }
        public string[] Headers { get; set; }
        public string Body { get; set; }

        public void Save(string fileName) {
            using (var sw = File.Create(fileName)) {
                var serializer = new XmlSerializer(typeof(RequestViewModel));
                serializer.Serialize(sw, this);
            }
        }

        public static RequestViewModel Open(string fileName) {
            using (var file = File.Open(fileName, FileMode.Open)) {
                var serializer = new XmlSerializer(typeof(RequestViewModel));
                return serializer.Deserialize(file) as RequestViewModel;
            }
        }
    }
}
