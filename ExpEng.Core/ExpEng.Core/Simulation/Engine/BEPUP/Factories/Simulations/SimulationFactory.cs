using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Base.Factories;
using ExpEng.Core.Contracts.Base;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using BepuPhysics;
using BepuPhysics.CollisionDetection;
using BepuUtilities;
using BepuUtilities.Memory;
using ExpEng.Core.Base;
using System.Runtime.CompilerServices;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations
{
    public abstract class SimulationFactory : InitializableFactory, ISimulationFactory, IInitializable
    {
        protected readonly IBufferPoolFactory bufferPoolFactory;
        protected readonly INarrowPhaseCallbacksFactory narrowPhaseCallbacksFactory;
        protected readonly IPoseIntegratorCallbacksFactory poseIntegratorCallbacksFactory;
        protected readonly ISolveDescriptionFactory solveDescriptionFactory;
        protected readonly ITimeStepperFactory timeStepperFactory;
        protected readonly ISimulationAllocationSizesFactory simulationAllocationSizesFactory;

        protected readonly bool npCallbacksTypesProvided;
        protected readonly bool piCallbackTypesProvided;

        protected SimulationFactory(IBufferPoolFactory bufferPoolFactory,
                                    INarrowPhaseCallbacksFactory narrowPhaseCallbacksFactory,
                                    IPoseIntegratorCallbacksFactory poseIntegratorCallbacksFactory,
                                    ISolveDescriptionFactory solveDescriptionFactory,
                                    ITimeStepperFactory timeStepperFactory = default,
                                    ISimulationAllocationSizesFactory simulationAllocationSizesFactory = default)
        {
            this.bufferPoolFactory = bufferPoolFactory;
            this.narrowPhaseCallbacksFactory = narrowPhaseCallbacksFactory;
            this.poseIntegratorCallbacksFactory = poseIntegratorCallbacksFactory;
            this.solveDescriptionFactory = solveDescriptionFactory;
            this.timeStepperFactory = timeStepperFactory;
            this.simulationAllocationSizesFactory = simulationAllocationSizesFactory;
        }

        protected override void InitializeCore() { }

        public virtual BepuPhysics.Simulation CreateSimulation<TNPCallbacks, TPICallbacks>()
            where TNPCallbacks : struct, INarrowPhaseCallbacks
            where TPICallbacks : struct, IPoseIntegratorCallbacks
        {
            this.EnsureInitialized();

            return this.CreateSimulationCore<TNPCallbacks, TPICallbacks>();
        }


        protected virtual BepuPhysics.Simulation CreateSimulationCore<TNPCallbacks, TPICallbacks>()
            where TNPCallbacks : struct, INarrowPhaseCallbacks
            where TPICallbacks : struct, IPoseIntegratorCallbacks
        {

            this.CreateNonCallbacksArgs(out var bufferPool, out var solveDescription, out var timeStepper, out var simAllocSizes);

            var npCallbacks = this.CreateNarrowPhaseCallbacks<TNPCallbacks>();
            var piCallbacks = this.CreatePoseIntegratorCallbacks<TPICallbacks>();

            if (this.TryCreateSimulationAllocationSizes(out var sas))
                simAllocSizes = new SimulationAllocationSizesWrapper(sas).Value;

            return this.CreateSimulationCore(bufferPool, npCallbacks, piCallbacks, solveDescription, timeStepper, simAllocSizes);
        }

        protected virtual BepuPhysics.Simulation CreateSimulationCore<TNPCallbacks, TPICallbacks>(BufferPool bp,
                                                                                                  SolveDescription solveDescription,
                                                                                                  ITimestepper? timesTepper,
                                                                                                  SimulationAllocationSizes? simAllocSizes)
            where TNPCallbacks : struct, INarrowPhaseCallbacks
            where TPICallbacks : struct, IPoseIntegratorCallbacks
        {
            var npCallbacks = this.CreateNarrowPhaseCallbacks<TNPCallbacks>();
            var piCallbacks = this.CreatePoseIntegratorCallbacks<TPICallbacks>();

            return this.CreateSimulationCore(bp, npCallbacks, piCallbacks, solveDescription, timesTepper, simAllocSizes);
        }

        protected virtual BepuPhysics.Simulation CreateSimulationCore<TNPCallbacks, TPICallbacks>(TNPCallbacks npCallbacks, TPICallbacks piCallbacks)
            where TNPCallbacks : struct, INarrowPhaseCallbacks
            where TPICallbacks : struct, IPoseIntegratorCallbacks
        {

            this.CreateNonCallbacksArgs(out var bufferPool, out var solveDescription, out var timeStepper, out var simAllocSizes);
            return this.CreateSimulationCore(bufferPool, npCallbacks, piCallbacks, solveDescription, timeStepper, simAllocSizes);
        }

        protected virtual BepuPhysics.Simulation CreateSimulationCore<TNPCallbacks, TPICallbacks>(BufferPool bp, 
                                                                                                  TNPCallbacks npCallbacks,
                                                                                                  TPICallbacks piCallbacks,
                                                                                                  SolveDescription solveDescription,
                                                                                                  ITimestepper? timeStepper,
                                                                                                  SimulationAllocationSizes? simAllocSizes)
            where TNPCallbacks : struct, INarrowPhaseCallbacks
            where TPICallbacks : struct, IPoseIntegratorCallbacks
        {
            if (timeStepper == null && simAllocSizes == null)   //..AG.. Conditionals TBD 
                return
                    BepuPhysics.Simulation.Create(bp, npCallbacks, piCallbacks, solveDescription);
            //else if(timeStepper != null && simAllocSizes == null) 
            //else if(timeStepper == null && simAllocSizes != null)
            else
                return BepuPhysics.Simulation.Create(bp,
                                             npCallbacks,
                                             piCallbacks,
                                             solveDescription,
                                             timeStepper,
                                             simAllocSizes);
        }


        protected virtual BufferPool CreateBufferPool() => this.bufferPoolFactory.CreateBufferPool();
        protected virtual SolveDescription CreateSolveDescription() => this.solveDescriptionFactory.CreateSolveDescription();

        protected virtual TNPCallbacks CreateNarrowPhaseCallbacks<TNPCallbacks>()
            where TNPCallbacks : struct, INarrowPhaseCallbacks => this.narrowPhaseCallbacksFactory.CreateNarrowPhaseCallbacks<TNPCallbacks>();
        protected virtual TPICallbacks CreatePoseIntegratorCallbacks<TPICallbacks>()
            where TPICallbacks : struct, IPoseIntegratorCallbacks => this.poseIntegratorCallbacksFactory.CreatePoseIntegratorCallbacks<TPICallbacks>();

        protected virtual bool TryCreateTimeStepper(out ITimestepper? timeStepper)
        {
            if (this.timeStepperFactory != default)
            {
                timeStepper = this.timeStepperFactory.CreateTimeStepper();
                return true;
            }

            timeStepper = null;

            return false;
        }
        protected virtual bool TryCreateSimulationAllocationSizes(out ISimulationAllocationSizes? simulationAllocationSizes) //.AG..
        {

            if (this.simulationAllocationSizesFactory != default)
            {
                var res = this.simulationAllocationSizesFactory.CreateSimulationAllocationSizes();

                if (res != null)
                {
                    simulationAllocationSizes = res.SimulationAllocationSizes;
                    return true;
                }
            }

            simulationAllocationSizes = null;
            return false;
        }

        private void CreateNonCallbacksArgs(out BufferPool bp, out SolveDescription sd, out ITimestepper? timeStepper, out SimulationAllocationSizes? simAllocSizes)
        {
            bp = this.CreateBufferPool();
            sd = this.CreateSolveDescription();

            _ = this.TryCreateTimeStepper(out timeStepper);

            simAllocSizes = null;

            if (this.TryCreateSimulationAllocationSizes(out var sas))
                simAllocSizes = new SimulationAllocationSizesWrapper(sas).Value;
        }

        public BepuPhysics.Simulation CreateSimulation(BufferPool bp, INarrowPhaseCallbacks npCallbacks, IPoseIntegratorCallbacks poseIntegratorCallbacks)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class SimulationFactory<TNPCallbacks, TPICallbacks> : SimulationFactory, ISimulationFactory<TNPCallbacks, TPICallbacks>
        where TNPCallbacks : struct, INarrowPhaseCallbacks
        where TPICallbacks : struct, IPoseIntegratorCallbacks
    {

        protected SimulationFactory(IBufferPoolFactory bufferPoolFactory,
                                    INarrowPhaseCallbacksFactory<TNPCallbacks> narrowPhaseCallbacksFactory,
                                    IPoseIntegratorCallbacksFactory<TPICallbacks> poseIntegratorCallbacksFactory,
                                    ISolveDescriptionFactory solveDescriptionFactory,
                                    ITimeStepperFactory timeStepperFactory,
                                    ISimulationAllocationSizesFactory simulationAllocationSizesFactory)
                                    : base(bufferPoolFactory,
                                           narrowPhaseCallbacksFactory,
                                           poseIntegratorCallbacksFactory,
                                           solveDescriptionFactory,
                                           timeStepperFactory,
                                           simulationAllocationSizesFactory)
        {
        }

        public BepuPhysics.Simulation CreateSimulation() => this.CreateSimulationCore<TNPCallbacks, TPICallbacks>();

        protected override BepuPhysics.Simulation CreateSimulationCore<TNPCallbacksProvided, TPICallbacksProvided>()
        {
            var npCallbacks = this.CreateNarrowPhaseCallbacks();
            var piCallbacks = this.CreatePoseIntegratorCallbacks();

            return base.CreateSimulationCore(npCallbacks, piCallbacks);
        }

        protected abstract TNPCallbacks CreateNarrowPhaseCallbacks();
        protected abstract TPICallbacks CreatePoseIntegratorCallbacks();
    }


}
