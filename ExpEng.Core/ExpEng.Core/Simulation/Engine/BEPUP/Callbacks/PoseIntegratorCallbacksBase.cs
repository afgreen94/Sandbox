using BepuPhysics;
using BepuUtilities;
using ExpEng.Core.Physics;
using ExpEng.Core.Simulation.Engine.BEPUP.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Callbacks
{
    public unsafe struct DefaultPoseIntegratorCallbacks : IPoseIntegratorCallbacks
    {

        private unsafe readonly IInteraction gravity;

        private unsafe readonly IPoseIntegratorCallbacksSettings settings;

        public unsafe AngularIntegrationMode AngularIntegrationMode => this.settings.AngularIntegrationMode;
        public unsafe bool AllowSubstepsForUnconstrainedBodies => this.settings.AllowSubstepsForUnconstrainedBodies;
        public unsafe bool IntegrateVelocityForKinematics => this.settings.IntegrateVelocityForKinematics;


        public DefaultPoseIntegratorCallbacks(IPoseIntegratorCallbacksSettings settings ) { this.settings = settings; }

        public void Initialize(BepuPhysics.Simulation simulation) { }

        public void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position,
                                      QuaternionWide orientation,
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask,
                                      int workerIndex,
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity)
        {
            this.gravity.IntegrateVelocity(bodyIndices, position, orientation, localInertia, integrationMask, workerIndex, dt, ref velocity);
        }

        public interface IVelocityIntegrator
        {
            void IntegrateVelocity(Vector<int> bodyIndices,
                          Vector3Wide position,
                          QuaternionWide orientation,
                          BodyInertiaWide localInertia,
                          Vector<int> integrationMask,
                          int workerIndex,
                          Vector<float> dt,
                          ref BodyVelocityWide velocity);
        }

        public interface IVelocityIntegratorNode 
        {
            void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position,
                                      QuaternionWide orientation,
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask,
                                      int workerIndex,
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity);
            IVelocityIntegratorNode Next { get; }
        }

        public interface IVelocityIntegrationPreparer { }

        public interface IPoseIntegratorCallbacksSettings
        {

            AngularIntegrationMode AngularIntegrationMode { get; }
            bool AllowSubstepsForUnconstrainedBodies { get; }
            bool IntegrateVelocityForKinematics { get; }
        }

        public abstract class PoseIntegratorCallbacksSettings : IPoseIntegratorCallbacksSettings
        {
            public abstract AngularIntegrationMode AngularIntegrationMode { get; }
            public abstract bool AllowSubstepsForUnconstrainedBodies { get; }
            public abstract bool IntegrateVelocityForKinematics { get; }
        }

        public class DefaultPoseIntegratorCallbackSettings : PoseIntegratorCallbacksSettings, IPoseIntegratorCallbacksSettings
        {
            public override AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;
            public override bool AllowSubstepsForUnconstrainedBodies => false;
            public override bool IntegrateVelocityForKinematics => false;
        }



        public void PrepareForIntegration(float dt) => throw new NotImplementedException();
    }

    public class PoseIntegratorCallbacksFactory<TPIC>
        where TPIC : struct, IPoseIntegratorCallbacks
    {

    }
}
