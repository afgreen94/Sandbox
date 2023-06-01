using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepuPhysics;
using BepuPhysics.CollisionDetection;
using BepuUtilities;
using BepuUtilities.Memory;

using ExpEng.Core.Simulation.Engine.BEPUP.Callbacks;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks;

using NarrowPhase = ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;
using PoseIntegrator = ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.PoseIntegrator;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;
using ExpEng.Core.Contracts.Base;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.PoseIntegrator;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations
{
    public abstract class SiD_SimulationFactory : SimulationFactory, ISiD_SimulationFactory
    {
        public SiD_SimulationFactory(SiD_BufferPoolFactory.ISiD_BufferPoolFactory bufferPoolFactory,
                                     ISiD_NarrowPhaseCallbacksFactory narrowPhaseCallbacksFactory,
                                     ISiD_PoseIntegratorCallbacksFactory poseIntegratorCallbacksFactory,
                                     SiD_SolveDescriptionFactory.ISiD_SolveDescriptionFactory solveDescriptionFactory,
                                     SiD_TimeStepperFactory.ISiD_TimeStepperFactory timeStepperFactory = default,
                                     SiD_SimulationAllocationSizesFactory.ISiD_SimulationAllocationSizesFactory simulationAllocationSizesFactory = default)
                                     : base(bufferPoolFactory,
                                            narrowPhaseCallbacksFactory,
                                            poseIntegratorCallbacksFactory,
                                            solveDescriptionFactory,
                                            timeStepperFactory,
                                            simulationAllocationSizesFactory)
        {
        }
    }

    public abstract class SiD_SimulationFactory<TNPCallbacks, TPICallbacks> : SimulationFactory<TNPCallbacks, TPICallbacks>, ISiD_SimulationFactory<TNPCallbacks, TPICallbacks>
        where TNPCallbacks : struct, INarrowPhaseCallbacks
        where TPICallbacks : struct, IPoseIntegratorCallbacks
    {
        public SiD_SimulationFactory(SiD_BufferPoolFactory.ISiD_BufferPoolFactory bufferPoolFactory,
                                     ISiD_NarrowPhaseCallbacksFactory<TNPCallbacks> narrowPhaseCallbacksFactory,
                                     ISiD_PoseIntegratorCallbacksFactory<TPICallbacks> poseIntegratorCallbacksFactory,
                                     SiD_SolveDescriptionFactory.ISiD_SolveDescriptionFactory solveDescriptionFactory,
                                     SiD_TimeStepperFactory.ISiD_TimeStepperFactory timeStepperFactory = default,
                                     SiD_SimulationAllocationSizesFactory.ISiD_SimulationAllocationSizesFactory simulationAllocationSizesFactory = default)
                                    : base(bufferPoolFactory, narrowPhaseCallbacksFactory, poseIntegratorCallbacksFactory, solveDescriptionFactory, timeStepperFactory, simulationAllocationSizesFactory)
        {
        }
    }

    public class SiD_SimulationFactory_EmbeddedTypes : SiD_SimulationFactory<SiD_NarrowPhaseCallbacks, SiD_PoseIntegratorCallbacks>, ISiD_SimulationFactory_EmbeddedTypes
    {

        private readonly static Type _narrowPhaseCallbacksType = typeof(SiD_NarrowPhaseCallbacks);
        private readonly static Type _poseIntegatorCallbacksType = typeof(SiD_PoseIntegratorCallbacks);

        public static Type NarrowPhaseCallbacksType => _narrowPhaseCallbacksType;
        public static Type PoseIntegartorCallbacksType => _poseIntegatorCallbacksType;

        public SiD_SimulationFactory_EmbeddedTypes(SiD_BufferPoolFactory.ISiD_BufferPoolFactory bufferPoolFactory,
                                                   ISiD_NarrowPhaseCallbacksFactory_EmbeddedTypes narrowPhaseCallbacksFactory,
                                                   ISiD_PoseIntegratorCallbacksFactory_EmbeddedTypes poseIntegratorCallbacksFactory,
                                                   SiD_SolveDescriptionFactory.ISiD_SolveDescriptionFactory solveDescriptionFactory,
                                                   SiD_TimeStepperFactory.ISiD_TimeStepperFactory timeStepperFactory = default,
                                                   SiD_SimulationAllocationSizesFactory.ISiD_SimulationAllocationSizesFactory simulationAllocationSizesFactory = default)
                                                   : base(bufferPoolFactory,
                                                          narrowPhaseCallbacksFactory,
                                                          poseIntegratorCallbacksFactory,
                                                          solveDescriptionFactory,
                                                          timeStepperFactory,
                                                          simulationAllocationSizesFactory)
        {
        }

        protected override SiD_NarrowPhaseCallbacks CreateNarrowPhaseCallbacks() => ((ISiD_NarrowPhaseCallbacksFactory_EmbeddedTypes)this.narrowPhaseCallbacksFactory).CreateNarrowPhaseCallbacks();
        protected override SiD_PoseIntegratorCallbacks CreatePoseIntegratorCallbacks() => ((ISiD_PoseIntegratorCallbacksFactory_EmbeddedTypes)this.poseIntegratorCallbacksFactory).CreatePoseIntegratorCallbacks();
    }

    public interface ISiD_SimulationFactory : ISimulationFactory { }
    public interface ISiD_SimulationFactory<TNPCallbacks, TPICallbacks> : ISimulationFactory<TNPCallbacks, TPICallbacks>
        where TNPCallbacks : struct, INarrowPhaseCallbacks
        where TPICallbacks : struct, IPoseIntegratorCallbacks
    { 
    }
    public interface ISiD_SimulationFactory_EmbeddedTypes : ISiD_SimulationFactory<SiD_NarrowPhaseCallbacks,
                                                                                   SiD_PoseIntegratorCallbacks>, IInitializable
    {
    } 
}
