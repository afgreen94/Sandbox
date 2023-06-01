using BepuPhysics;
using BepuUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks
{
    public struct DefaultPoseIntegratorCallbacks : IPoseIntegratorCallbacks
    {

        private unsafe readonly AngularIntegrationMode aim;
        private unsafe readonly bool ascb;
        private unsafe readonly bool ivk;

        public AngularIntegrationMode AngularIntegrationMode => this.aim;
        public bool AllowSubstepsForUnconstrainedBodies => this.ascb;
        public bool IntegrateVelocityForKinematics => this.ivk;
        

        public DefaultPoseIntegratorCallbacks(AngularIntegrationMode angularIntegrationMode,
                                              bool allowSubstepsForUnconstrainedBodies,
                                              bool integrateVelocityForKinematics)
        {
            this.aim = angularIntegrationMode;
            this.ascb = allowSubstepsForUnconstrainedBodies;
            this.ivk = integrateVelocityForKinematics;
        }

        public void Initialize(BepuPhysics.Simulation simulation) { }

        public void IntegrateVelocity(Vector<int> bodyIndices, 
                                      Vector3Wide position,
                                      QuaternionWide orientation, 
                                      BodyInertiaWide localInertia, 
                                      Vector<int> integrationMask, 
                                      int workerIndex, 
                                      Vector<float> dt, 
                                      ref BodyVelocityWide velocity) => throw new NotImplementedException();

        public void PrepareForIntegration(float dt) => throw new NotImplementedException();
    }

    public class PoseIntegratorCallbacksFactory<TPIC>
        where TPIC : struct, IPoseIntegratorCallbacks
    {

    }
}
