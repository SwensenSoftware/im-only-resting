using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using Swensen.Utils;

namespace Swensen.Ior.Core {
    /// <summary>
    /// A light wrapper around our underlying http client.
    /// </summary>
    public class HttpClient {
        private readonly string defaultRequestContentType;
        private readonly string proxyServer;

        public HttpClient(string defaultRequestContentType, string proxyServer) {
            this.defaultRequestContentType = defaultRequestContentType;
            this.proxyServer = proxyServer;
        }

        public RestRequestAsyncHandle ExecuteAsync(RequestModel requestModel, Action<ResponseModel> callback) {
            var client = new RestClient();
            if (!proxyServer.IsBlank())
                client.Proxy = new WebProxy(proxyServer, false); //make second arg a config option.

            var restRequest = requestModel.ToRestRequest(defaultRequestContentType);

            var start = DateTime.Now;
            return client.ExecuteAsync(restRequest, restResponse => {
                var end = DateTime.Now;
                var responseModel = new ResponseModel(restResponse, start, end);
                callback(responseModel);
            });
        }

    }
}
