using BepuPhysics;
using BepuUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Interactions
{
    public struct StrongNuclear : IInteraction
    {
        public void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position,
                                      QuaternionWide orientation,
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask,
                                      int workerIndex,
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity)
        { }

        public void PrepareForIntegration(float dt)
        { }
    }

    public struct WeakNuclear : IInteraction
    {
        public void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position,
                                      QuaternionWide orientation,
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask,
                                      int workerIndex,
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity)
        { }

        public void PrepareForIntegration(float dt)
        { }
    }
}
