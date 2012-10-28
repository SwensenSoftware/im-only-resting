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
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Swensen.Utils;

namespace Swensen.Ior.Core {
    public class RequestModel {
        public Uri Url { get; private set;}
        public HttpMethod Method { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public IEnumerable<KeyValuePair<string, string>> NonContentTypeHeaders {
            get {
                return Headers.Where(x => x.Key.ToUpper() != "CONTENT-TYPE");
            }
        }
        public string Body { get; private set; }

        public static List<string> TryCreate(RequestViewModel vm, out RequestModel requestModel) {
            var validationErrors = new List<string>();

            Uri url = null;
            if(vm.Url.IsBlank())
                validationErrors.Add("Request URL may not be empty");
            else {
                var knownProtocals = new[] { "http://", "https://", "ftp://", "ftps://", "file:///" };
                var forgivingUrl = knownProtocals.Any(x => vm.Url.StartsWith(x)) ? vm.Url : "http://" + vm.Url;
                if(!Uri.TryCreate(forgivingUrl, UriKind.Absolute, out url))
                    validationErrors.Add("Request URL is invalid");    
            }

            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var line in vm.Headers) {
                if (line.IsBlank())
                    continue; //allow empty lines

                var match = Regex.Match(line, @"^([^\:]+)\:(.+)$", RegexOptions.Compiled);
                if (!match.Success)
                    validationErrors.Add("Invalid header line (format incorrect): " + line);
                else {

                    var key = match.Groups[1].Value.Trim();
                    var value = match.Groups[2].Value.Trim();
                    if (key.IsBlank() || value.IsBlank())
                        validationErrors.Add("Invalid header line (key or value is blank): " + line);
                    else if (headers.ContainsKey(key))
                        validationErrors.Add("Invalid header line (duplicate key): " + line);
                    else {
                        if (key.ToUpper() == "CONTENT-TYPE") { //or other content type headers? split out?
                            //var hh = (HttpContentHeaders)Activator.CreateInstance(typeof(HttpContentHeaders), true);
                            //try {
                            //    hh.Add(key, value.Split(',').Select(x => x.Trim()));
                                headers.Add(key, value);
                            //} catch (Exception e) {
                            //    validationErrors.Add(string.Format("Invalid header line ({0}): {1}", e.Message, line));
                            //}
                        } else {
                            var hh = (HttpRequestHeaders)Activator.CreateInstance(typeof(HttpRequestHeaders), true);
                            try {
                                hh.Add(key, value.Split(',').Select(x => x.Trim()));
                                headers.Add(key, value);
                            } catch (Exception e) {
                                validationErrors.Add(string.Format("Invalid header line ({0}): {1}", e.Message, line));
                            }
                        }
                    }
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

        public String GetContentType(String defaultRequestContentType) {
            //default content-type: http://mattryall.net/blog/2008/03/default-content-type
            var ct = Headers.FirstOrDefault(header => header.Key.ToUpper() == "CONTENT-TYPE").Value; //first try user supplied
            ct = ct.IsBlank() ? defaultRequestContentType : ct; //then try settings supplied
            ct = ct.IsBlank() ? "application/octet-stream" : ct; // then try w3 spec default
            return ct;
        }
    }
}
