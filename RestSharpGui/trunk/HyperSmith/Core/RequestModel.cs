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
using System.Net;
using RestSharp;
using System.Text.RegularExpressions;

namespace Swensen.Ior.Core {
    public class RequestModel {
        public Uri Url { get; private set;}
        public Method Method { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public string Body { get; private set; }

        public static List<string> TryCreate(RequestViewModel vm, out RequestModel requestModel) {
            List<string> validationErrors = new List<string>();

            Uri url = null;
            if(String.IsNullOrWhiteSpace(vm.Url))
                validationErrors.Add("Request URL may not be empty");
            else {
                var knownProtocals = new[] { "http://", "https://", "ftp://", "ftps://", "file:///" };
                var forgivingUrl = knownProtocals.Any(x => vm.Url.StartsWith(x)) ? vm.Url : "http://" + vm.Url;
                if(!Uri.TryCreate(forgivingUrl, UriKind.Absolute, out url))
                    validationErrors.Add("Request URL is invalid");    
            }

            var headers = new Dictionary<string,string>();
            foreach (var line in vm.Headers) {
                if (String.IsNullOrWhiteSpace(line))
                    continue; //allow empty lines

                var match = Regex.Match(line, @"^([^\:]+)\:(.+)$", RegexOptions.Compiled);

                if (!match.Success)
                    validationErrors.Add("Invalid header line: " + line);
                else {
                    var key = match.Groups[1].Value.Trim();
                    var value = match.Groups[2].Value.Trim();
                    if (String.IsNullOrWhiteSpace(key) || String.IsNullOrWhiteSpace(value))
                        validationErrors.Add("Invalid header line: " + line);
                    else
                        headers.Add(key, value);
                }
            }

            if (validationErrors.Count > 0)
                requestModel = null;
            else {
                requestModel = new RequestModel() {
                    Url = url,
                    Method = vm.Method,
                    Headers = headers,
                    Body = vm.Body
                };
            }

            return validationErrors;                
        }

        public RestRequest ToRestRequest(string defaultRequestContentType) {
            var rr = new RestRequest(Url.ToString(), Method);            
            foreach (var header in Headers)
                rr.AddHeader(header.Key, header.Value);

            //default content-type: http://mattryall.net/blog/2008/03/default-content-type
            var ct = Headers.FirstOrDefault(header => header.Key.ToUpper() == "CONTENT-TYPE").Value; //first try user supplied
            ct = String.IsNullOrWhiteSpace(ct) ? defaultRequestContentType : ct; //then try settings supplied
            ct = String.IsNullOrWhiteSpace(ct) ? "application/octet-stream" : ct; // then try w3 spec default
            rr.AddParameter(ct, Body, ParameterType.RequestBody); //http://stackoverflow.com/questions/5095692/how-to-add-text-to-request-body-in-restsharp

            return rr;
        }
    }
}
