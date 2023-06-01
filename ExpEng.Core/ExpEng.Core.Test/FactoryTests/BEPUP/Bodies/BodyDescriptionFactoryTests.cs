using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Development.Simulation.Engine.BEPUP;
using ExpEng.Core.TestFramework;
using ExpEng.Core.Contracts.Base;

using ServiceRegistrar = ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies.ServiceRegistrar;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

using ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies;

namespace ExpEng.Core.Test.FactoryTests.BEPUP.Bodies
{
    public class BodyDescriptionFactoryTests : TestClass
    {
        protected override IConfiguration BuildConfiguration() => new ConfigurationBuilder().Build();

        protected override void RegisterServicesCore(IServiceCollection sc) =>  new ServiceRegistrar().RegisterServices(sc, this.configuration);


        [Fact]
        public void SiD_BodyDescriptionFactory_Native()
        {
            this.Initialize();


            using var scope = this.serviceProvider.CreateScope();

            var bdf = scope.ServiceProvider.GetRequiredService<ISiD_BodyDescriptionFactory_Navtive>();

            bdf.Initialize();

            var body = bdf.CreateBodyDescription();
        }

        //public class ServiceRegistrar : IServiceRegistrar
        //{
        //    public void RegisterServices(IServiceCollection sc, IConfiguration configuration)
        //    {
        //        sc.AddScoped<ISiD_RigidPoseFactory, SiD_RigidPoseFactory>();
        //        sc.AddScoped<ISiD_CollidableDescriptionFactory, SiD_CollidableDescriptionFactory>();
        //        sc.AddScoped<ISiD_BodyActivityDescriptionFactory, SiD_BodyActivityFactory>();

        //        sc.AddScoped<ISiD_BodyDescriptionFactory_Navtive, SiD_BodyDescriptionFactory_Navtive>();
        //    }
        //}
    }
}
