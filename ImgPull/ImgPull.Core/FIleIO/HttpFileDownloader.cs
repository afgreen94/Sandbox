using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public abstract class HttpFileDownloaderBase : IFileDownloader
    {
        protected const string MissingSourceUriProvider = "Missing Source Uri Provider";
        protected const string SourceUriProviderExhaustedError = "Source Uri Provider exhausted";

        protected readonly ISourceDesignationProvider SourceUriProvider;

        protected HttpFileDownloaderBase() { }
        protected HttpFileDownloaderBase(ISourceDesignationProvider sourceUriProvider) { this.SourceUriProvider = sourceUriProvider; }

        public async Task<DownloadResult> DownloadAsync() => await this.DownloadCoreAsync().ConfigureAwait(false);
        public async Task<DownloadResult> DownloadAsync(string sourceOverride) => await this.DownloadCoreAsync(sourceOverride).ConfigureAwait(false);

        private async Task<DownloadResult> DownloadCoreAsync(string sourceOverride = default)
        {
            try
            {
                Uri source;

                if (!string.IsNullOrEmpty(sourceOverride))
                    source = new Uri(sourceOverride);
                else if (this.SourceUriProvider != null)
                {
                    var moveNextResult = await this.SourceUriProvider.MoveNextAsync().ConfigureAwait(false);

                    if (!moveNextResult || this.SourceUriProvider.Current == null)
                        return new DownloadResult(SourceUriProviderExhaustedError);

                    source = new Uri(SourceUriProvider.Current);
                }
                else
                    return new DownloadResult(MissingSourceUriProvider);

                var response = await HttpHandler.SendAsync(new HttpRequestMessage()
                {
                    RequestUri = source,
                    Method = HttpMethod.Get
                }).ConfigureAwait(false);

                var httpResponseValidationResult = await HttpResultValidator.ValidateHttpResponseMessageAsync(response).ConfigureAwait(false);

                if (!httpResponseValidationResult.Success)
                    return new DownloadResult(httpResponseValidationResult.ToString());

                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                return new DownloadResult() { ResultStream = responseStream };
            }
            catch (Exception ex) { return new DownloadResult(ex.ToString()); }
        }
    }

    public class HttpFileDownloader : HttpFileDownloaderBase, IFileDownloader
    {
        public HttpFileDownloader() : base() { }
        public HttpFileDownloader(ISourceDesignationProvider sourceUriProvider) : base(sourceUriProvider) { }
    }
}
