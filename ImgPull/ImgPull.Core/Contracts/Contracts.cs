using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public interface IFileLinkProvider
    {
        public IAsyncEnumerable<FileLinkResult> GetLinksAsync(FileLinkProviderArgs optionsOVerride = default);
    }
    public interface IFileDownloader
    {
        public Task<DownloadResult> DownloadAsync();
        public Task<DownloadResult> DownloadAsync(string sourceOverride);
    }

    public interface IFileUploader
    {
        public Task<UploadResult> UploadAsync(Stream sourceStream);
        public Task<UploadResult> UploadAsync(Stream sourceStream, string destinationOverride);
    }

    public interface IFileTransfer
    {
        public Task<TransferResult> TransferAsync();
        public Task<TransferResult> TransferAsync(string sourceOverride = default, string destination = default);
    }

    public interface IDesignationProvider : IAsyncEnumerator<string>
    {

    }

    public interface ISourceDesignationProvider : IDesignationProvider { }
    public interface IDestinationDesignationProvider : IDesignationProvider { }

    public interface ICallResult
    {
        public bool Success { get; }
        public string ErrorText { get; }
    }

    public interface ICallResult<T>
    {
        public T Result { get; }
    }
}
