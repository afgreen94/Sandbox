using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Base;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Xunit;
using Xunit.Sdk;

namespace ExpEng.Core.TestFramework
{
    
    public abstract class TestClass : Initializable
    {

        protected IConfiguration configuration { get; private set; }
        protected IServiceProvider serviceProvider { get; private set; }

        public TestClass() { }

        protected override void InitializeCore()
        {
            this.configuration = this.BuildConfiguration();

            var sc = new ServiceCollection();

            this.RegisterServices(sc);
        }

        protected abstract IConfiguration BuildConfiguration();

        protected void RegisterServices(IServiceCollection sc)
        {
            sc.AddSingleton(this.configuration);

            this.RegisterServicesCore(sc);

            this.serviceProvider = sc.BuildServiceProvider();
        }
        protected abstract void RegisterServicesCore(IServiceCollection sc);


        protected static IConfiguration BuildDefaultConfiguration() => new ConfigurationBuilder().Build();

    }



}
