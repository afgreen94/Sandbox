using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public class UploadResult : CallResult
    {
        public UploadResult() { }
        public string UploadDestination { get; set; }
    }
    public class DownloadResult : CallResult
    {
        public DownloadResult() { }
        public DownloadResult(string errorText) : base(errorText) { }
        public DownloadResult(Stream resultStream) { this.ResultStream = resultStream; }
        public DownloadResult(string downloadSource, Stream resultStream) { this.DownloadSource = downloadSource; this.ResultStream = resultStream; }
        public string DownloadSource { get; set; }
        public Stream ResultStream { get; set; }

        public async Task<string> AsStringAsync()
        {
            if (!this.ValidateStreamSet())
                return default;

            using var sr = new StreamReader(this.ResultStream);
            return await sr.ReadToEndAsync().ConfigureAwait(false);
        }

        public async Task<byte[]> AsBytesAsync()
        {
            if (this.ValidateStreamSet())
                return default;

            using var ms = new MemoryStream();
            await this.ResultStream.CopyToAsync(ms);

            return ms.ToArray();
        }

        private bool ValidateStreamSet() => this.ResultStream != default;
    }

    public class TransferResult : CallResult
    {
        public TransferResult() { }
        public TransferResult(string errorText) : base(errorText) { }
        public TransferResult(string downloadSource, string uploadDestination) { this.DownloadSource = downloadSource; this.UploadDestination = uploadDestination; }

        public string DownloadSource { get; set; }
        public string UploadDestination { get; set; }
    }

    public class FileLinkResult
    {
        public string FileLink { get; set; }
        public string FileName { get; set; }
    }
    public class TargetCollection
    {
        public HashSet<string> PageLinks { get; set; } = new HashSet<string>();
        public HashSet<string> FileLinks { get; set; } = new HashSet<string>();
    }
}
