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
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Swensen.Utils;

namespace Swensen.Ior.Core {
    public class RequestModel {
        public Uri Url { get; private set;}
        public HttpMethod Method { get; private set; }
        public Dictionary<string, string> RequestHeaders { get; private set; }
        public Dictionary<string, string> ContentHeaders { get; private set; }

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

            var requestHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var contentHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
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
                    else if (requestHeaders.ContainsKey(key) || contentHeaders.ContainsKey(key))
                        validationErrors.Add("Invalid header line (duplicate key, comma-separate multiple values for one key): " + line);
                    else {
                        var values = value.Split(',').Select(x => x.Trim()).ToList().AsReadOnly();
                        //some ugliness to leverage system.net.http request and content header validation
                        var hrhValidator = (HttpRequestHeaders)Activator.CreateInstance(typeof(HttpRequestHeaders), true);
                        try {
                            hrhValidator.Add(key, value);
                            requestHeaders.Add(key, value);
                        } catch (InvalidOperationException) { //i.e. header belongs in content headers
                            var hchValidator = (HttpContentHeaders)Activator.CreateInstance(typeof(HttpContentHeaders), BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.NonPublic, null, new[] { (object)(Func<long?>)(() => (long?)null) }, CultureInfo.CurrentCulture);
                            try {
                                hchValidator.Add(key, value);
                                contentHeaders.Add(key, value);
                            } catch (Exception e) {
                                validationErrors.Add(string.Format("Invalid header line ({0}): {1}", e.Message, line));
                            }
                        } catch (Exception e) {
                            validationErrors.Add(string.Format("Invalid header line ({0}): {1}", e.Message, line));
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
                    RequestHeaders = requestHeaders,
                    ContentHeaders = contentHeaders,
                    Body = vm.Body
                };
            }

            return validationErrors;                
        }
    }
}
