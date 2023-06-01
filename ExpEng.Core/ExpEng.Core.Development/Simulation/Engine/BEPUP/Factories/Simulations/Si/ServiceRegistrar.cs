using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Base;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations
{
    public class ServiceRegistrar : IServiceRegistrar
    {
        private readonly bool usingDefaultTimeStepper = false;
        private readonly bool usingDefaultSASArgs = false;

        public ServiceRegistrar() { }

        public ServiceRegistrar(bool usingDefaultTimeStepper = false, bool usingDefaultSASArgs = false)
        {
            this.usingDefaultTimeStepper = usingDefaultTimeStepper;
            this.usingDefaultSASArgs = usingDefaultSASArgs;
        }

        public void RegisterServices(IServiceCollection sc, IConfiguration configuration)
        {
            sc.AddScoped<SiD_BufferPoolFactory.ISiD_BufferPoolFactory, SiD_BufferPoolFactory>();

            sc.AddScoped<ISiD_NarrowPhaseCallbacksFactory_EmbeddedTypes, SiD_NarrowPhaseCallbacksFactory_EmbeddedTypes>();
            sc.AddScoped<ISiD_PoseIntegratorCallbacksFactory_EmbeddedTypes, SiD_PoseIntegratorCallbacksFactory_EmbeddedTypes>();

            sc.AddScoped<SiD_SolveDescriptionFactory.ISiD_SolveDescriptionFactory, SiD_SolveDescriptionFactory>();

            if (this.usingDefaultTimeStepper)
                sc.AddScoped<SiD_TimeStepperFactory.ISiD_TimeStepperFactory, SiD_TimeStepperFactory>();

            if (this.usingDefaultSASArgs)
                sc.AddScoped<SiD_SimulationAllocationSizesFactory.ISiD_SimulationAllocationSizesFactory, SiD_SimulationAllocationSizesFactory>();

            sc.AddScoped<ISiD_SimulationFactory_EmbeddedTypes, SiD_SimulationFactory_EmbeddedTypes>();
        }
    }
}
