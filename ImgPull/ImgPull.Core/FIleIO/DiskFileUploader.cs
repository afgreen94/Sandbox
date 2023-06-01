using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public abstract class DiskFileUploaderBase : IFileUploader
    {

        protected const string MissingFilepathProviderErrorText = "Missing Disk Filepath Provider";
        protected const string FilepathProviderExhaustedErrorText = "Disk Filepath Provider Exhausted";

        protected IDestinationDesignationProvider diskFilepathProvider;

        protected HashSet<string> createdDirectoryCache = new();

        protected DiskFileUploaderBase() { }
        protected DiskFileUploaderBase(IDestinationDesignationProvider diskFilepathProvider) { this.diskFilepathProvider = diskFilepathProvider; }

        public virtual async Task<UploadResult> UploadAsync(Stream sourceStream)
        {
            if (this.diskFilepathProvider != null)
            {
                var moveNextResult = await this.diskFilepathProvider.MoveNextAsync().ConfigureAwait(false);

                if (!moveNextResult || this.diskFilepathProvider.Current == null)
                    return new UploadResult() { ErrorText = FilepathProviderExhaustedErrorText };

                return await this.UploadCoreAsync(sourceStream, this.diskFilepathProvider.Current);
            }
            else
                return new UploadResult() { ErrorText = MissingFilepathProviderErrorText };
        }

        public virtual async Task<UploadResult> UploadAsync(Stream sourceStream, string destinationOverride) => await this.UploadCoreAsync(sourceStream, destinationOverride).ConfigureAwait(false);

        protected async Task<UploadResult> UploadCoreAsync(Stream sourceStream, string destination)
        {
            try
            {
                this.CreateDirectory(destination);

                using var fileStream = new FileStream(destination, FileMode.OpenOrCreate);
                await sourceStream.CopyToAsync(fileStream).ConfigureAwait(false);

                return new UploadResult() { UploadDestination = destination };
            }
            catch (Exception ex) { return new UploadResult() { ErrorText = ex.ToString() }; }
        }

        protected void CreateDirectory(string path)
        {
            if(this.createdDirectoryCache.Add(path))
                Utils.Disk.CreateDirectoryChain(path, true);
        }
    }

    public class DiskFileUploader : DiskFileUploaderBase, IFileUploader 
    {
        public DiskFileUploader() : base() { }
        public DiskFileUploader(IDestinationDesignationProvider diskFilepathProvider) : base(diskFilepathProvider) { }
    }
}
