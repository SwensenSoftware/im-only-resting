using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

////execute the request and get the response
//var restRequest = requestModel.ToRestRequest(Settings.Default.DefaultRequestContentType);
//var client = new RestClient();
//if(!String.IsNullOrWhiteSpace(Settings.Default.ProxyServer))
//    client.Proxy = new WebProxy(Settings.Default.ProxyServer, false); //make second arg a config option.

//var start = DateTime.Now;
//requestAsyncHandle = client.ExecuteAsync(restRequest, restResponse => {
//    //switch to UI thread
//    this.Invoke((MethodInvoker) delegate {
//        this.requestAsyncHandle = null;

//        var end = DateTime.Now;
//        var responseVm = new ResponseViewModel(restResponse, start, end);

//        //bind the response view
//        bind(responseVm);
//        grpResponse.Update();
//    });
//});

namespace Swensen.RestSharpGui {
    public class HttpClient {
        private readonly string defaultRequestContentType;
        private readonly string proxyServer;

        public HttpClient(string defaultRequestContentType, string proxyServer) {
            this.defaultRequestContentType = defaultRequestContentType;
            this.proxyServer = proxyServer;
        }

        public RestRequestAsyncHandle ExecuteAsync(RequestModel requestModel, Action<ResponseViewModel> callback) {
            var client = new RestClient();
            if (!String.IsNullOrWhiteSpace(proxyServer))
                client.Proxy = new WebProxy(proxyServer, false); //make second arg a config option.

            var restRequest = requestModel.ToRestRequest(defaultRequestContentType);

            var start = DateTime.Now;
            return client.ExecuteAsync(restRequest, restResponse => {
                var end = DateTime.Now;
                var responseVm = new ResponseViewModel(restResponse, start, end);
                callback(responseVm);
            });
        }

    }
}
