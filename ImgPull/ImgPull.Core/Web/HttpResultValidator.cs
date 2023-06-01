using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace ImgPull.Core
{
    public class HttpResultValidator
    {
        public static async Task<HttpResponseValidationResult> ValidateHttpResponseMessageAsync(HttpResponseMessage response, bool preserveResponse = false)
        {
            if (response.IsSuccessStatusCode)
                return new HttpResponseValidationResult();

            var errorText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!preserveResponse)
                response.Dispose();

            return new HttpResponseValidationResult(response.StatusCode, response.ReasonPhrase, errorText);
        } 
    }

    public class HttpResponseValidationResult : CallResult
    {

        private const string ErrorTextTemplate = "Http Request Failed.\n" +
                                                 "Status Code: {0}\n" +
                                                 "Reason Phrase: {1}\n" +
                                                 "Response Body: {2}";

        public HttpResponseValidationResult() { }
        public HttpResponseValidationResult(HttpStatusCode statusCode, string reasonPhrase, string errorText) : base(errorText) 
        { this.StatusCode = statusCode; this.ReasonPhrase = reasonPhrase; }

        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }

        public override string ToString() => this.Success ? base.ToString() : string.Format(ErrorTextTemplate, this.StatusCode.ToString(), this.ReasonPhrase, this.ErrorText);

    }
}
