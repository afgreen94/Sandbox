using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using BepuPhysics;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies
{
    public class SiD_SimulationAllocationSizesFactory : SimulationAllocationSizesFactory, SiD_SimulationAllocationSizesFactory.ISiD_SimulationAllocationSizesFactory
    {

        //..AG.. also pulled from api, default in some demos 

        private static readonly Result _instance = new(new SimulationAllocationSizesWrapper(new SimulationAllocationSizes()
        {
            Bodies = 4096,
            Statics = 4096,
            ShapesPerType = 128,
            ConstraintCountPerBodyEstimate = 8,
            Constraints = 16384,
            ConstraintsPerTypeBatch = 256
        }));

        public override ISimulationAllocationSizesFactoryResult CreateSimulationAllocationSizes() => _instance;


        public interface ISiD_SimulationAllocationSizesFactory : ISimulationAllocationSizesFactory { }
        public interface ISDI_SimulationAllocationSizesFactoryResult : ISimulationAllocationSizesFactoryResult { }
        public class Result : SimulationAllocationSizesFactoryResult, ISDI_SimulationAllocationSizesFactoryResult
        {
            public Result(ISimulationAllocationSizesWrapper simulationAllocationSizes) : base(simulationAllocationSizes)
            {
            }
        }
    }
}
