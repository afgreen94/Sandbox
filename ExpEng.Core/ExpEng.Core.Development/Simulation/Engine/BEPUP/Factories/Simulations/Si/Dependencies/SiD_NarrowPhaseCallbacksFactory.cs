using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using NarrowPhase = ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;

using BepuPhysics.CollisionDetection;
using System.Runtime.InteropServices;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;
using BepuPhysics.Constraints;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies
{
    public class SiD_NarrowPhaseCallbacksFactory : NarrowPhaseCallbacksFactory, ISiD_NarrowPhaseCallbacksFactory
    {
        public override TNPCallbacksProvided CreateNarrowPhaseCallbacks<TNPCallbacksProvided>()
        {




            throw new NotImplementedException();
        }
    }

    public abstract class SiD_NarrowPhaseCallbacksFactory<TNPCallbacksKnown> : SiD_NarrowPhaseCallbacksFactory, ISiD_NarrowPhaseCallbacksFactory<TNPCallbacksKnown>
        where TNPCallbacksKnown : struct, INarrowPhaseCallbacks
    {
        public abstract TNPCallbacksKnown CreateNarrowPhaseCallbacks();
    }

    public class SiD_NarrowPhaseCallbacksFactory_EmbeddedTypes : SiD_NarrowPhaseCallbacksFactory<SiD_NarrowPhaseCallbacks>, ISiD_NarrowPhaseCallbacksFactory_EmbeddedTypes
    {
        private readonly SpringSettings contactSpringiness = new(30, 1);
        private readonly float maximumRecoveryVelocity = 2f;
        private readonly float frictionCoefficient = 1f;

        public override SiD_NarrowPhaseCallbacks CreateNarrowPhaseCallbacks() => new(contactSpringiness, maximumRecoveryVelocity, frictionCoefficient);
    }


    public interface ISiD_NarrowPhaseCallbacksFactory : INarrowPhaseCallbacksFactory { }
    public interface ISiD_NarrowPhaseCallbacksFactory<TNPCallbacksKnown> : INarrowPhaseCallbacksFactory<TNPCallbacksKnown> //INarrowPhaseCallbacksFactory<TNPCallbacksKnown>
        where TNPCallbacksKnown : struct, INarrowPhaseCallbacks
    { }
    public interface ISiD_NarrowPhaseCallbacksFactory_EmbeddedTypes : ISiD_NarrowPhaseCallbacksFactory<SiD_NarrowPhaseCallbacks> { }

}
