using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base;
using Base.Contracts;

using Gaming;
using Microsoft.Extensions.DependencyInjection;

namespace ND_Drawing
{
    public class Driver : Base.Driver, IDriver
    {

        public Driver(ICommandLineParser commandLineParser,
              IConfigurationProvider configurationProvider,
              IServiceRegistrar serviceRegistrar) : base(commandLineParser, configurationProvider, serviceRegistrar) { }
        public Driver() : this(new CommandLineParser(), 
                               new ConfigurationProvider(),
                               new ServiceRegistrar()) { }

        protected override async ValueTask<IDriveResult> DriveCoreAsync()
        {
            using var scope = this.serviceProvider.CreateScope();




            return new DriveResult();
        }




        public class CommandLineParser : ICommandLineParser
        {
            public ApplicationArgs Parse(string[] args)
            {
                throw new NotImplementedException();
            }
        }

        public class ConfigurationProvider : IConfigurationProvider
        {
            public Microsoft.Extensions.Configuration.IConfiguration GetConfiguration()
            {
                throw new NotImplementedException();
            }
        }

        public class ServiceRegistrar : IServiceRegistrar
        {
            public void RegisterSerivces(IServiceCollection serviceCollection, Microsoft.Extensions.Configuration.IConfiguration configuration)
            {
                throw new NotImplementedException();
            }
        }
    }

}
