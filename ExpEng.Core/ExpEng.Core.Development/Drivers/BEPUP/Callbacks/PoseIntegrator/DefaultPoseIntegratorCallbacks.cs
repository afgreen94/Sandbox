using BepuPhysics;
using BepuUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator
{
    public struct DefaultPoseIntegratorCallbacks : IPoseIntegratorCallbacks    //does nothing 
    {
        public AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;

        public bool AllowSubstepsForUnconstrainedBodies => false;

        public bool IntegrateVelocityForKinematics => false;

        public void Initialize(BepuPhysics.Simulation simulation) { }

        public void PrepareForIntegration(float dt)
        {
        }

        public void IntegrateVelocity(Vector<int> bodyIndices, 
                                      Vector3Wide position,
                                      QuaternionWide orientation, 
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask, 
                                      int workerIndex, 
                                      Vector<float> dt, 
                                      ref BodyVelocityWide velocity)
        {
        }

    }
}
