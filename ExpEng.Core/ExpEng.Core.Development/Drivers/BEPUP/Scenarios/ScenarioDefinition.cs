using BepuPhysics.CollisionDetection;
using BepuPhysics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.Drivers.BEPUP.Scenarios
{
    public abstract class ScenarioDefinition<TNPCallbacks, TPICallbacks> : IScenarioDefinition<TNPCallbacks, TPICallbacks>
        where TNPCallbacks : struct, INarrowPhaseCallbacks
        where TPICallbacks : struct, IPoseIntegratorCallbacks
    {
        public abstract TNPCallbacks NarrowPhaseCallbacks { get; }
        public abstract TPICallbacks PoseIntegratorCallbacks { get; }
        public abstract SolveDescription SolveDescription { get; }
        public abstract ICameraSettings InitialCameraSettings { get; }
    }

    public abstract class ScenarioDefinition : IScenarioDefinition
    {
        public abstract INarrowPhaseCallbacks NarrowPhaseCallbacks { get; }
        public abstract IPoseIntegratorCallbacks PoseIntegratorCallbacks { get; }
        public abstract SolveDescription SolveDescription { get; }
        public abstract ICameraSettings InitialCameraSettings { get; }
    }

    public interface IScenarioDefinition<TNPCallbacks, TPICallbacks>
        where TNPCallbacks : struct, INarrowPhaseCallbacks
        where TPICallbacks : struct, IPoseIntegratorCallbacks
    {
        public TNPCallbacks NarrowPhaseCallbacks { get; }
        public TPICallbacks PoseIntegratorCallbacks { get; }
        public SolveDescription SolveDescription { get; }
        public ICameraSettings InitialCameraSettings { get; }
    }

    public unsafe interface IScenarioDefinition
    {
        public INarrowPhaseCallbacks NarrowPhaseCallbacks { get; }
        public IPoseIntegratorCallbacks PoseIntegratorCallbacks { get; }
        public SolveDescription SolveDescription { get; }
        public ICameraSettings InitialCameraSettings { get; }
    }

    public interface IScenarioDefinitionDescription { }


}
