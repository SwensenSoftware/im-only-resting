using System;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Swensen.Utils;
using System.Threading;

namespace Swensen.Ior.Core {
    /// <summary>
    /// A light wrapper around our underlying http client.
    /// </summary>
    public class IorClient {
        private readonly string defaultRequestContentType;
        private readonly string proxyServer;

        public IorClient(string defaultRequestContentType, string proxyServer) {
            this.defaultRequestContentType = defaultRequestContentType;
            this.proxyServer = proxyServer;
        }

        public CancellationTokenSource ExecuteAsync(RequestModel requestModel, Action<ResponseModel> callback) {
            //todo: add using statements for disposible objects

            var handler = new HttpClientHandler();
            if (!proxyServer.IsBlank()) {
                handler.Proxy = new WebProxy(proxyServer, false); //make second arg a config option.
                handler.UseProxy = true;
            }

            var client = new HttpClient(handler);
            var request = new HttpRequestMessage {
                RequestUri = requestModel.Url, 
                Method = requestModel.Method
            };

            foreach (var header in requestModel.RequestHeaders)
                request.Headers.Add(header.Key, header.Value);

            if (requestModel.Method != HttpMethod.Get) {
                var content = new StringContent(requestModel.Body);
                foreach (var header in requestModel.ContentHeaders) {
                    content.Headers.Remove(header.Key);
                    if (!String.Equals(header.Key, "content-type", StringComparison.OrdinalIgnoreCase))
                        request.Headers.Add(header.Key, header.Value);
                }

                //default content-type: http://mattryall.net/blog/2008/03/default-content-type
                content.Headers.Remove("Content-Type");
                string ct;
                requestModel.ContentHeaders.TryGetValue("Content-Type", out ct);
                ct = ct.IsBlank() ? defaultRequestContentType : ct; //then try settings supplied
                ct = ct.IsBlank() ? "application/octet-stream" : ct; // then try w3 spec default
                content.Headers.Add("Content-Type", ct);
                
                request.Content = content;
            }

            var start = DateTime.Now;

            var ctokenSource = new CancellationTokenSource();
            var ctoken = ctokenSource.Token;
            
            client.SendAsync(request,ctoken).ContinueWith(responseTask => {
                var response = responseTask.Result;
                var end = DateTime.Now;
                var responseModel = new ResponseModel(response, start, end);
                callback(responseModel);
            });

            return ctokenSource;
        }

    }
}
