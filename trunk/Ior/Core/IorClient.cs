using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using NLog;
using Swensen.Utils;
using System.Threading;

namespace Swensen.Ior.Core {
    /// <summary>
    /// A light wrapper around our underlying http client.
    /// </summary>
    public class IorClient {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

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
                //default content-type: http://mattryall.net/blog/2008/03/default-content-type
                string textCt;
                requestModel.ContentHeaders.TryGetValue("Content-Type", out textCt);
                textCt = textCt.IsBlank() ? defaultRequestContentType : textCt; //then try settings supplied
                textCt = textCt.IsBlank() ? "application/octet-stream" : textCt; // then try w3 spec default

                //get encoding
                var ct = new ContentType(textCt);
                var encoding = Encoding.GetEncoding("ISO-8859-1"); //w3 default
                try {
                    //todo: move to validation and have no fallback if INVALID charset is given
                    encoding = Encoding.GetEncoding(ct.CharSet);
                } catch {
                    log.Warn("charset={0} not supported, falling back on {0}", ct.CharSet, encoding.WebName);
                }

                //write content w/ BOM
                var memStream = new MemoryStream();
                var bom = encoding.GetPreamble();
                memStream.Write(bom, 0, bom.Length);
                var contentBytes = encoding.GetBytes(requestModel.Body);
                memStream.Write(contentBytes, 0, contentBytes.Length);
                var content = new ByteArrayContent(memStream.ToArray());
                
                foreach (var header in requestModel.ContentHeaders) {
                    content.Headers.Remove(header.Key); //remove defaults
                    if (String.Equals(header.Key, "content-type", StringComparison.OrdinalIgnoreCase))
                        content.Headers.Add(header.Key, textCt);
                    else
                        content.Headers.Add(header.Key, header.Value);
                }

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
