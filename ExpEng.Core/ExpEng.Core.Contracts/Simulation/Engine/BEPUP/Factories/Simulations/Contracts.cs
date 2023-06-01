using BepuPhysics.CollisionDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepuUtilities.Memory;
using BepuPhysics;

namespace ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations
{
    public interface ISimulationFactory
    {
        BepuPhysics.Simulation CreateSimulation<TNPCallbacks, TPICallbacks>()
            where TNPCallbacks : struct, INarrowPhaseCallbacks
            where TPICallbacks : struct, IPoseIntegratorCallbacks;

        BepuPhysics.Simulation CreateSimulation(BufferPool bp, INarrowPhaseCallbacks npCallbacks, IPoseIntegratorCallbacks poseIntegratorCallbacks);
    }

    public interface ISimulationFactory<TNPCallbacks, TPICallbacks> : ISimulationFactory 
        where TNPCallbacks : struct, INarrowPhaseCallbacks
        where TPICallbacks : struct, IPoseIntegratorCallbacks
    {
        BepuPhysics.Simulation CreateSimulation();
    }


    public interface IBufferPoolFactory { BufferPool CreateBufferPool(); }
    




    public interface ICallbacksFactory
    {
        //..AG.. api doesnt unify callback factories, maybe because unsafe structs? idk
        //it should matter, but have to think of them as seperate things, and looks lumpy in the repo, oh well.
        //this means error in pi factory has to be corrected 
        //shouldn't be hard. should look more into unsafe struct 
    }

    public interface INarrowPhaseCallbacksFactory : ICallbacksFactory
    {
        TNPCallbacksProvided CreateNarrowPhaseCallbacks<TNPCallbacksProvided>()
            where TNPCallbacksProvided : struct, INarrowPhaseCallbacks;
    }
    public interface INarrowPhaseCallbacksFactory<TNPCallbacksKnown> : INarrowPhaseCallbacksFactory 
        where TNPCallbacksKnown : struct, INarrowPhaseCallbacks
    {
        TNPCallbacksKnown CreateNarrowPhaseCallbacks();
    }

    public interface IPoseIntegratorCallbacksFactory : ICallbacksFactory
    { 
        TPICallbacksProvided CreatePoseIntegratorCallbacks<TPICallbacksProvided>()
            where TPICallbacksProvided : struct, IPoseIntegratorCallbacks; 
    }
    public interface IPoseIntegratorCallbacksFactory<TPICallbacksKnown> : IPoseIntegratorCallbacksFactory 
        where TPICallbacksKnown : struct, IPoseIntegratorCallbacks
    {
        TPICallbacksKnown CreatePoseIntegratorCallbacks();
    }

    //..AG.. generic multityped interface bases. ie. IGenericArgs<T1..TN> etc. 

    public interface ISolveDescriptionFactory { SolveDescription CreateSolveDescription(); }

    public interface ITimeStepperFactory { ITimestepper CreateTimeStepper(); }




    //..Pulled from api, struct impl w/ cmmnts ..AG..
    public interface ISimulationAllocationSizes
    {
        /// <summary>
        /// The number of bodies to allocate space for.
        /// </summary>
        int Bodies { get; }
        /// <summary>
        /// The number of statics to allocate space for.
        /// </summary>
        int Statics { get; }
        /// <summary>
        /// The number of inactive islands to allocate space for.
        /// </summary>
        int Islands { get; }
        /// <summary>
        /// Minimum number of shapes to allocate space for in each shape type batch.
        /// </summary>
        int ShapesPerType { get; }
        /// <summary>
        /// The number of constraints to allocate bookkeeping space for. This does not affect actual type batch allocation sizes, only the solver-level constraint handle storage.
        /// </summary>
        int Constraints { get; }
        /// <summary>
        /// The minimum number of constraints to allocate space for in each individual type batch.
        /// New type batches will be given enough memory for this number of constraints, and any compaction will not reduce the allocations below it.
        /// The number of constraints can vary greatly across types- there are usually far more contacts than ragdoll constraints.
        /// Per type estimates can be assigned within the Solver.TypeBatchAllocation if necessary. This value acts as a lower bound for all types.
        /// </summary>
        int ConstraintsPerTypeBatch { get; }
        /// <summary>
        /// The minimum number of constraints to allocate space for in each body's constraint list.
        /// New bodies will be given enough memory for this number of constraints, and any compaction will not reduce the allocations below it.
        /// </summary>
        int ConstraintCountPerBodyEstimate { get; }

        /// <summary>
        /// Constructs a description of simulation allocations.
        /// </summary>
        /// <param name="bodies">The number of bodies to allocate space for.</param>
        /// <param name="statics">The number of statics to allocate space for.</param>
        /// <param name="islands">The number of inactive islands to allocate space for.</param>
        /// <param name="shapesPerType">Minimum number of shapes to allocate space for in each shape type batch.</param>
        /// <param name="constraints">The number of constraints to allocate bookkeeping space for. This does not affect actual type batch allocation sizes, only the solver-level constraint handle storage.</param>
        /// <param name="constraintsPerTypeBatch">The minimum number of constraints to allocate space for in each individual type batch.
        /// New type batches will be given enough memory for this number of constraints, and any compaction will not reduce the allocations below it.
        /// The number of constraints can vary greatly across types- there are usually far more contacts than ragdoll constraints.
        /// Per type estimates can be assigned within the Solver.TypeBatchAllocation if necessary. This value acts as a lower bound for all types.</param>
        /// <param name="constraintCountPerBodyEstimate">The minimum number of constraints to allocate space for in each body's constraint list.
        /// New bodies will be given enough memory for this number of constraints, and any compaction will not reduce the allocations below it.</param>

    }


    public interface ISimulationAllocationSizesWrapper : ISimulationAllocationSizes { SimulationAllocationSizes Value { get; } }
    public interface ISimulationAllocationSizesFactoryResult { ISimulationAllocationSizesWrapper SimulationAllocationSizes { get; } }


    public abstract class SimulationAllocationSizesFactoryResult : ISimulationAllocationSizesFactoryResult
    {

        private readonly ISimulationAllocationSizesWrapper simulationAllocationSizes;

        public ISimulationAllocationSizesWrapper SimulationAllocationSizes => this.simulationAllocationSizes;

        protected SimulationAllocationSizesFactoryResult(ISimulationAllocationSizesWrapper simulationAllocationSizes) { this.simulationAllocationSizes = simulationAllocationSizes; }
    }

    public interface ISimulationAllocationSizesFactory { ISimulationAllocationSizesFactoryResult CreateSimulationAllocationSizes(); }


    public class SimulationAllocationSizesWrapper : ISimulationAllocationSizesWrapper
    {
        private readonly SimulationAllocationSizes allocationSizes;

        public SimulationAllocationSizes Value => this.allocationSizes;

        public SimulationAllocationSizesWrapper(ISimulationAllocationSizes simAllocSizes) : this(new SimulationAllocationSizes()
        {
            Bodies = simAllocSizes.Bodies,
            Statics = simAllocSizes.Statics,
            Islands = simAllocSizes.Islands,
            ShapesPerType = simAllocSizes.ShapesPerType,
            Constraints = simAllocSizes.Constraints,
            ConstraintCountPerBodyEstimate = simAllocSizes.ConstraintCountPerBodyEstimate,
        }) { }

        //public SimulationAllocationSizesWrapper(SimulationAllocationSizesWrapper other)
        //{
        //    //..AG.. tbd
        //}

        public SimulationAllocationSizesWrapper(SimulationAllocationSizes allocationSizes) { this.allocationSizes = allocationSizes; }

        public int Bodies => this.allocationSizes.Bodies;

        public int Statics => this.allocationSizes.Statics;

        public int Islands => this.allocationSizes.Islands;

        public int ShapesPerType => this.allocationSizes.ShapesPerType;

        public int Constraints => this.allocationSizes.Constraints;

        public int ConstraintsPerTypeBatch => this.allocationSizes.ConstraintsPerTypeBatch;

        public int ConstraintCountPerBodyEstimate => this.allocationSizes.ConstraintCountPerBodyEstimate;
    }
}
