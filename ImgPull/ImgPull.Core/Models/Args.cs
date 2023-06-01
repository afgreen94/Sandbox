using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public class DownloadArgs
    {
    }

    public class UploadArgs
    {
        public byte[] SourceBytes { get; set; }
        public Stream SourceStream { get; set; }
    }

    public class FileLinkProviderArgs
    {
        public string Source { get; set; }
    }
}
