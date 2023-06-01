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
    public interface IInteraction
    {
        void PrepareForIntegration(float dt);
        void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position,
                                      QuaternionWide orientation,
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask,
                                      int workerIndex,
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity);
    }
}
