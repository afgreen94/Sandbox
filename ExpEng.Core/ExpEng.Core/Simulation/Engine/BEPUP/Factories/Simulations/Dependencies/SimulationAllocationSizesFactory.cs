using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepuPhysics;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations
{
    public abstract class SimulationAllocationSizesFactory : ISimulationAllocationSizesFactory
    {
        public abstract ISimulationAllocationSizesFactoryResult CreateSimulationAllocationSizes();
    }
}
