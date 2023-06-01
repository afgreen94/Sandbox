using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public abstract class FileTransferBase : IFileTransfer
    {
        protected const string FailedDownloadErrorTextTemplate = "Download Failed. Error Text: {0}";
        protected const string FailedUploadErrorTextTemplate = "Upload Failed. Error Text: {0}";

        private readonly IFileDownloader downloader;
        private readonly IFileUploader uploader;

        protected FileTransferBase(IFileDownloader downloader, IFileUploader uploader)
        {
            this.downloader = downloader;
            this.uploader = uploader;
        }

        public virtual async Task<TransferResult> TransferAsync() => await this.TransferCoreAsync().ConfigureAwait(false);
        public virtual async Task<TransferResult> TransferAsync(string sourceOverride = default, string destinationOverride = default) => await this.TransferCoreAsync(sourceOverride, destinationOverride).ConfigureAwait(false);

        protected virtual async Task<TransferResult> TransferCoreAsync(string sourceOverride = default, string destinationOverride = default)
        {
            try
            {
                var downloadResult = string.IsNullOrEmpty(sourceOverride) ?
                                                                            await this.downloader.DownloadAsync().ConfigureAwait(false) :
                                                                            await this.downloader.DownloadAsync(sourceOverride).ConfigureAwait(false);
                if (!downloadResult.Success)
                    return this.BuildFailedDownloadTransferResult(downloadResult.ErrorText);

                using var resultStream = downloadResult.ResultStream;

                var uploadResult = string.IsNullOrEmpty(destinationOverride) ?
                                                                                await this.uploader.UploadAsync(resultStream).ConfigureAwait(false) :
                                                                                await this.uploader.UploadAsync(resultStream, destinationOverride).ConfigureAwait(false);

                if (!uploadResult.Success)
                    return this.BuildFailedUploadResultTransferResult(uploadResult.ErrorText);

                return new TransferResult()
                {
                    DownloadSource = downloadResult.DownloadSource,
                    UploadDestination = uploadResult.UploadDestination
                };
            }
            catch (Exception ex) { return new TransferResult() { ErrorText = ex.ToString() }; }
        }

        protected TransferResult BuildFailedDownloadTransferResult(string downloadResultResultErrorText) => BuildFailedTransferResult(FailedDownloadErrorTextTemplate, downloadResultResultErrorText);
        protected TransferResult BuildFailedUploadResultTransferResult(string uploadResultErrorText) => BuildFailedTransferResult(FailedUploadErrorTextTemplate, uploadResultErrorText);
        protected TransferResult BuildFailedTransferResult(string errorTextTemplate, string errorText) => new(string.Format(errorTextTemplate, errorText));
    }

    public class FileTransfer : FileTransferBase, IFileTransfer
    {
        public FileTransfer(IFileDownloader downloader, IFileUploader uploader) : base(downloader, uploader) { }
    }

}
