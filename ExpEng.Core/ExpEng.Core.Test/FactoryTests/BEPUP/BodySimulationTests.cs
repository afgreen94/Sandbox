using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Development.Simulation.Engine.BEPUP;
using ExpEng.Core.TestFramework;
using ExpEng.Core.Contracts.Base;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Xunit;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Test.FactoryTests.BEPUP.Simulation;
using ExpEng.Core.Test.FactoryTests.BEPUP.Bodies;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

using Factories = ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories;

namespace ExpEng.Core.Test.FactoryTests.BEPUP
{
    public class BodySimulationTests : TestClass
    {

        private bool usingDefaultTimeStepper = false;
        private bool usingDefaultSASArgs = false;

        protected override IConfiguration BuildConfiguration() => new ConfigurationBuilder().Build();

        protected override void RegisterServicesCore(IServiceCollection sc)
        {
            new Factories.Bodies.ServiceRegistrar().RegisterServices(sc, this.configuration);
            new Factories.Simulations.ServiceRegistrar(this.usingDefaultTimeStepper, this.usingDefaultSASArgs).RegisterServices(sc, this.configuration);
        }

        [Fact]
        public void SimulateBodyTest0() => this.SimulateBodyTestCore();

        private void SimulateBodyTestCore()
        {
            this.Initialize();

            using var scope = this.serviceProvider.CreateScope();

            var sf = scope.ServiceProvider.GetRequiredService<ISiD_SimulationFactory_EmbeddedTypes>();
            var bf = scope.ServiceProvider.GetRequiredService<ISiD_BodyDescriptionFactory_Navtive>();

            sf.Initialize();
            bf.Initialize();

            var simulation = sf.CreateSimulation();

            simulation.Bodies.Add(bf.CreateBodyDescription());
            simulation.Bodies.Add(bf.CreateBodyDescription());

        }


        //..AG.. close to ready for init commit!! :) 
    }
}
