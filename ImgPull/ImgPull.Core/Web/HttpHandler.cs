using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ImgPull.Core
{
    public class HttpHandler
    {

        private static HttpClient _client;
        private static bool _initialized = false;

        public static Task InitializeAsync(HttpMessageHandler handler = default) 
        {
            if (_initialized)
                return Task.CompletedTask;
            
            _client = handler == default ? new HttpClient() : new HttpClient(handler);
            _initialized = true;

            return Task.CompletedTask;
        }

        public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => await SendAsyncCore(request).ConfigureAwait(false);

        private static async Task<HttpResponseMessage> SendAsyncCore(HttpRequestMessage request)
        {
            TryValidateCore(request, out var errorText);

            if (!string.IsNullOrEmpty(errorText))
                throw new Exception(errorText);

            return await SendReturnHttpResponseCoreAsync(request).ConfigureAwait(false);
        }

        private static async Task<HttpResponseMessage> SendReturnHttpResponseCoreAsync(HttpRequestMessage request)
        {
            return await _client.SendAsync(request).ConfigureAwait(false);
        }

        //private static async Task<string> SendReturnStringCoreAsync(HttpRequestMessage request, bool skipValidate = false, bool suppressException = false)
        //{
        //    throw new NotImplementedException();
        //}

        private static bool TryValidateCore(out string errorText) => TryValidateCore(default, out errorText);
        private static bool TryValidateCore(HttpRequestMessage request, out string errorText)
        {
            errorText = string.Empty;

            if (!_initialized)
                errorText = "Http Handler Not Initialized";


            //-AG


            return string.IsNullOrEmpty(errorText);

        }
    }
}
