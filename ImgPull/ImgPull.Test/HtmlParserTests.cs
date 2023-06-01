using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using ImgPull.Core;
using Xunit;

namespace ImgPull.Test
{
    public class HtmlParserTests
    {
        private IFileLinkProvider linkProvider;
        private IFileTransfer fileTransfer;

        private async Task InitializeAsync()
        {
            await HttpHandler.InitializeAsync().ConfigureAwait(false);

            var downloader = new HttpFileDownloader();
            var uploader = new DiskFileUploader();

            this.linkProvider = new FourChanFileLinkProvider(downloader);
            this.fileTransfer = new FileTransfer(downloader, uploader);
        }

        [Fact]
        public void Detect()
        {
            var env = Environment.OSVersion.Version;
        }
    }
}