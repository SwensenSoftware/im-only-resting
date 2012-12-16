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
        private readonly bool includeUtf8Bom;

        public IorClient(string defaultRequestContentType, string proxyServer, bool includeUtf8Bom) {
            this.defaultRequestContentType = defaultRequestContentType;
            this.proxyServer = proxyServer;
            this.includeUtf8Bom = includeUtf8Bom;
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
                //write content w/ BOM  if needed
                var contentBytes = GetEncodedBytes(requestModel.Body, ct.CharSet, includeUtf8Bom);
                var content = new ByteArrayContent(contentBytes);
                
                foreach (var header in requestModel.ContentHeaders) {
                    content.Headers.Remove(header.Key); //remove defaults
                    if (String.Equals(header.Key, "content-type", StringComparison.OrdinalIgnoreCase)) //treat special w/ defaults, etc.
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
                if (responseTask.Status == System.Threading.Tasks.TaskStatus.RanToCompletion) {
                    var response = responseTask.Result;
                    var end = DateTime.Now;
                    var responseModel = new ResponseModel(response, start, end);
                    callback(responseModel);
                } else {
                    log.Info("responseTask did not run to completion, status={0}", responseTask.Status);
                }
            });

            return ctokenSource;
        }


        //todo can we make this async and cancellable? (since could take a long time)
        //see for when to use a BOM ("preamble") http://www.w3.org/International/questions/qa-byte-order-mark
        //and http://www.w3.org/TR/2010/WD-html-polyglot-20100624/#character-encoding
        //and http://www.w3.org/TR/html4/charset
        public static byte[] GetEncodedBytes(string content, string charset, bool includeUtf8Bom) {
            //n.b. .NET default utf-16 and utf-32 to utf-16le and utf-32le, but we want to default to be, per the spec
            var charsetUpper = (charset ?? "").ToUpperInvariant();
            //n.b. 1. do not use bom if le or be is indicated, 2. do use bom if not indicated for utf-16 and utf-32, as be. 3. utf-8 bom is optional, use if user preference.
            var bom = new byte[] {};
            var encoding = Encoding.GetEncoding("ISO-8859-1"); //w3 default
            try {
                switch (charsetUpper) {
                    case "UTF-8":
                        encoding = Encoding.GetEncoding("UTF-8");
                        if (includeUtf8Bom)
                            bom = encoding.GetPreamble();
                        break;
                    case "UTF-16":
                        encoding = Encoding.GetEncoding("UTF-16BE");
                        bom = encoding.GetPreamble();
                        break;
                    case "UTF-32":
                        encoding = Encoding.GetEncoding("UTF-32BE");
                        bom = encoding.GetPreamble();
                        break;
                    default:
                        //todo: move to validation and have no fallback if INVALID charset is given
                        encoding = Encoding.GetEncoding(charset);
                        //as of this writing, we don't believe boms should be emitted for any other encoding types.
                        break;
                }
            } catch {
                log.Warn("charset={0} not supported, falling back on {1}", charset, encoding.WebName);
            }

            var memStream = new MemoryStream();
            memStream.Write(bom, 0, bom.Length);
            var contentBytes = encoding.GetBytes(content);
            memStream.Write(contentBytes, 0, contentBytes.Length);
            return memStream.ToArray();
        }
    }
}
