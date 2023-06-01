using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepuPhysics;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations
{
    public abstract class PoseIntegratorCallbacksFactory : IPoseIntegratorCallbacksFactory
    {
        public abstract TPICallbacksProvided CreatePoseIntegratorCallbacks<TPICallbacksProvided>() 
            where TPICallbacksProvided : struct, IPoseIntegratorCallbacks;
    }

    public abstract class PoseIntegratorCallbacksFactory<TPICallbacksKnown> : PoseIntegratorCallbacksFactory, IPoseIntegratorCallbacksFactory<TPICallbacksKnown>
        where TPICallbacksKnown : struct, IPoseIntegratorCallbacks
    {
        public abstract TPICallbacksKnown CreatePoseIntegratorCallbacks();
    }
}
