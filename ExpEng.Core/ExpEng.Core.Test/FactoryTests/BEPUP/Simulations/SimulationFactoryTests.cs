using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Base;
using ExpEng.Core.Base.Factories;
using ExpEng.Core.Contracts.Base;

using ExpEng.Core.Simulation.Engine.BEPUP;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;
using ExpEng.Core.Simulation.Engine.BEPUP.Callbacks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using NarrowPhase = ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;
using PoseIntegrator = ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.PoseIntegrator;

using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using BepuUtilities.Memory;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.DependencyInjection;

using Xunit;
using Xunit.Sdk;
using ExpEng.Core.Base.DependencyInjection;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies;

using ServiceRegistrar = ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.ServiceRegistrar;

namespace ExpEng.Core.Test.FactoryTests.BEPUP.Simulation
{
    public class SimulationFactoryTests : TestFramework.TestClass 
    {
        private bool usingDefaultTimeStepper = false;
        private bool usingDefaultSASArgs = false;

        [Fact]
        public void SimulationFactoryTest()
        {
            this.Initialize();

            using var scope = this.serviceProvider.CreateScope();

            var sf = scope.ServiceProvider.GetRequiredService<ISiD_SimulationFactory_EmbeddedTypes>();

            sf.Initialize();

            var sim = sf.CreateSimulation();
        }


        [Fact]
        public void SimulationFactoryTest_UseDefaultTimeStepperAndSimAllocSizes()
        {

            this.usingDefaultTimeStepper = true;
            this.usingDefaultSASArgs = true;

            this.Initialize();

            this.SimulationFactoryTestCore();
        }

        private void SimulationFactoryTestCore()
        {
            using var scope = this.serviceProvider.CreateScope();

            var sf = scope.ServiceProvider.GetRequiredService<ISiD_SimulationFactory_EmbeddedTypes>();
            sf.Initialize();  //bool useDefaultTimestepper = false, bool useDefaultSASArgs = false ..AG.. maybe 

            var sim = sf.CreateSimulation();
        }

        protected override IConfiguration BuildConfiguration() => new ConfigurationBuilder().Build();

        protected override void RegisterServicesCore(IServiceCollection sc) => new ServiceRegistrar(this.usingDefaultTimeStepper, this.usingDefaultSASArgs).RegisterServices(sc, this.configuration);

        //public class ServiceRegistrar : IServiceRegistrar   //..AG.. bools have to come from somewhere 
        //{
        //    private readonly bool usingDefaultTimeStepper = false;
        //    private readonly bool usingDefaultSASArgs = false;


        //    public ServiceRegistrar() { }
        //    public ServiceRegistrar(bool usingDefaultTimeStepper, bool usingDefaultSASArg) : this()
        //    {
        //        this.usingDefaultTimeStepper = usingDefaultTimeStepper;
        //        this.usingDefaultSASArgs = usingDefaultSASArg;
        //    }

        //     public void RegisterServices(IServiceCollection sc, IConfiguration configuration)
        //    {
        //        sc.AddSingleton(configuration);

        //        sc.AddScoped<SiD_BufferPoolFactory.ISiD_BufferPoolFactory, SiD_BufferPoolFactory>();

        //        sc.AddScoped<ISiD_NarrowPhaseCallbacksFactory_EmbeddedTypes, SiD_NarrowPhaseCallbacksFactory_EmbeddedTypes>();
        //        sc.AddScoped<ISiD_PoseIntegratorCallbacksFactory_EmbeddedTypes, SiD_PoseIntegratorCallbacksFactory_EmbeddedTypes>();

        //        sc.AddScoped<SiD_SolveDescriptionFactory.ISiD_SolveDescriptionFactory, SiD_SolveDescriptionFactory>();

        //        if (this.usingDefaultTimeStepper)
        //            sc.AddScoped<SiD_TimeStepperFactory.ISiD_TimeStepperFactory, SiD_TimeStepperFactory>();

        //        if (this.usingDefaultSASArgs)
        //            sc.AddScoped<SiD_SimulationAllocationSizesFactory.ISiD_SimulationAllocationSizesFactory, SiD_SimulationAllocationSizesFactory>();

        //        sc.AddScoped<ISiD_SimulationFactory_EmbeddedTypes, SiD_SimulationFactory_EmbeddedTypes>();
        //    }
        //}
    }
}
