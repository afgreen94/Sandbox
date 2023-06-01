using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ImgPull.Core.Console
{
    public abstract class DriverBase
    {
        protected const string WindowsOSVersionKey = "WINDOWS";

        protected IServiceProvider ServiceProvider;

        protected virtual CommandLineParserBase commandLineParser => new CommandLineParser();

        protected CommandLineArgsBase commandLineArgs;
        public virtual async Task DriveAsync(string[] args)
        {
            ParseValidateSetCommandLineArgs(args);

            await this.InitializeAsync().ConfigureAwait(false);

            using var scope = this.ServiceProvider.CreateScope();

            var linkProvider = scope.ServiceProvider.GetRequiredService<IFileLinkProvider>();
            var fileTransfer = scope.ServiceProvider.GetRequiredService<IFileTransfer>();
            var filenameCorrection = scope.ServiceProvider.GetRequiredService<IFilenameCorrection>();

            TransferResult transferResult;

            await foreach (var linkResult in linkProvider.GetLinksAsync().ConfigureAwait(false))
            {
                var filename = linkResult.FileName;
                _ = filenameCorrection.TryCorrectFilename(ref filename);

                var localPath = string.Concat(this.commandLineArgs.Output.OutputDirectoryRoot, filename);

                if (this.commandLineArgs.Verbose)    //build in logging with relevant log levels 
                    System.Console.WriteLine($"Transfering {linkResult.FileLink} to {localPath}");

                transferResult = await fileTransfer.TransferAsync(linkResult.FileLink, localPath).ConfigureAwait(false);

                if (!transferResult.Success)
                {
                    //-AG- Handle 
                }
            }
        }

        protected virtual async Task InitializeAsync()
        {
            await HttpHandler.InitializeAsync().ConfigureAwait(false);

            this.InitializeServices();

            this.InitializeCore();
        }
        protected virtual void InitializeCore()
        {

        }

        protected virtual void InitializeServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IFileDownloader, HttpFileDownloader>();
            serviceCollection.AddScoped<IFileUploader, DiskFileUploader>();
            serviceCollection.AddScoped<IFileTransfer, FileTransfer>();

            this.RegisterOSServicesCore(serviceCollection);
            this.RegisterServicesCore(serviceCollection);

            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected virtual void RegisterOSServicesCore(IServiceCollection serviceCollection)
        {
            var osVersion = Environment.OSVersion.VersionString;

            if(osVersion.ToUpper().Contains(WindowsOSVersionKey))
            {
                serviceCollection.AddScoped<IFilenameCorrection, WindowsFilenameCorrrection>();
            }
        }

        protected virtual void RegisterServicesCore(IServiceCollection serviceCollection)
        {
        }

        protected virtual void ParseValidateSetCommandLineArgs(string[] args)
        {
            var commandLineArgs = this.commandLineParser.ParseValidate(args);
            this.commandLineArgs = commandLineArgs;
        }
    }

    public class Driver : DriverBase
    {
    }

    public class FourChanDriver : DriverBase
    {
        protected override CommandLineParserBase commandLineParser => new FourChanCommandLineParser();

        protected override void RegisterServicesCore(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new FourChanFileLinkProvider.Options() { IncludedExpressions = new HashSet<string>(((FourChanCommandLineArgs)this.commandLineArgs).Expressions), Board = ((FourChanCommandLineArgs)this.commandLineArgs).Board });
            serviceCollection.AddScoped<IFileLinkProvider, FourChanFileLinkProvider>();        
        }
    }
}