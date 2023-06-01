using BepuPhysics.CollisionDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations
{
    public abstract class NarrowPhaseCallbacksFactory : INarrowPhaseCallbacksFactory
    {
        public abstract TNPCallbacksProvided CreateNarrowPhaseCallbacks<TNPCallbacksProvided>() 
            where TNPCallbacksProvided : struct, INarrowPhaseCallbacks;
    }

    public abstract class NarrowPhaseCallbacksFactory<TNPCallbacksKnown> : NarrowPhaseCallbacksFactory, INarrowPhaseCallbacksFactory<TNPCallbacksKnown>
        where TNPCallbacksKnown : struct, INarrowPhaseCallbacks
    {
        public abstract TNPCallbacksKnown CreateNarrowPhaseCallbacks();
    }
}
