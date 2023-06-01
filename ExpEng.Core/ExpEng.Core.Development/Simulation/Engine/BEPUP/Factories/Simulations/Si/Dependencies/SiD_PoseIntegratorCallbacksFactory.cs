using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using PoseIntegrator = ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.PoseIntegrator;

using BepuPhysics;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.PoseIntegrator;
using System.Numerics;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies
{
    public class SiD_PoseIntegratorCallbacksFactory : PoseIntegratorCallbacksFactory, ISiD_PoseIntegratorCallbacksFactory
    {
        public override TPICallbacksProvided CreatePoseIntegratorCallbacks<TPICallbacksProvided>() { throw new NotImplementedException(); }
    }

    public abstract class SiD_PoseIntegratorCallbacksFactory<TPICallbacksKnown> : SiD_PoseIntegratorCallbacksFactory, ISiD_PoseIntegratorCallbacksFactory<TPICallbacksKnown>
        where TPICallbacksKnown : struct, IPoseIntegratorCallbacks
    {
        public abstract TPICallbacksKnown CreatePoseIntegratorCallbacks();
    }

    public class SiD_PoseIntegratorCallbacksFactory_EmbeddedTypes : SiD_PoseIntegratorCallbacksFactory<SiD_PoseIntegratorCallbacks>, ISiD_PoseIntegratorCallbacksFactory_EmbeddedTypes
    {

        private readonly Vector3 gravity = new Vector3(0f, -9.81f, 0f);
        private readonly float linearDampening = .03f;
        private readonly float angularDampening = .03f;

        public override SiD_PoseIntegratorCallbacks CreatePoseIntegratorCallbacks() => new(gravity, linearDampening, angularDampening);
    }

    public interface ISiD_PoseIntegratorCallbacksFactory : IPoseIntegratorCallbacksFactory { }
    public interface ISiD_PoseIntegratorCallbacksFactory<TPICallbacksKnown> : IPoseIntegratorCallbacksFactory<TPICallbacksKnown> //IPoseIntegratorCallbacksFactory 
        where TPICallbacksKnown : struct, IPoseIntegratorCallbacks
    { }
    public interface ISiD_PoseIntegratorCallbacksFactory_EmbeddedTypes : ISiD_PoseIntegratorCallbacksFactory<SiD_PoseIntegratorCallbacks>
    { }
}
