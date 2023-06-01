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

    //"binding field" 
    public struct SphericalHardReflectorField : IPoseIntegratorCallbacks
    {
        public unsafe readonly AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;
        public unsafe readonly bool AllowSubstepsForUnconstrainedBodies => false;
        public unsafe readonly bool IntegrateVelocityForKinematics => false;

        private unsafe readonly Vector3 cloudOrigin;
        private unsafe readonly float cloudRadius;

        public SphericalHardReflectorField(Vector3 origin, float radius)
        {
            this.cloudOrigin = origin;
            this.cloudRadius = radius;
        }

        public unsafe void Initialize(BepuPhysics.Simulation simulation)
        {
        }

        public unsafe void PrepareForIntegration(float dt)
        {
        }

        //could be too expensive, may have to treat as collidable ..AG..
        public unsafe void IntegrateVelocity(Vector<int> bodyIndices,
                                             Vector3Wide position,
                                             QuaternionWide orientation,
                                             BodyInertiaWide localInertia,
                                             Vector<int> integrationMask,
                                             int workerIndex,
                                             Vector<float> dt,
                                             ref BodyVelocityWide velocity)
        {
            Span<float> xU = stackalloc float[Vector<float>.Count];
            Span<float> yU = stackalloc float[Vector<float>.Count];
            Span<float> zU = stackalloc float[Vector<float>.Count];

            for (int i = 0; i < Vector<int>.Count; i++)
                if (this.BodyOutOfCloud(position, i, out var correction))
                    AddUpdate(xU, yU, zU, velocity, i, correction);

            ApplyUpdate(ref velocity, 
                        new Vector3Wide()
                        {
                            X = new(xU),
                            Y = new(yU),
                            Z = new(zU)
                        }, 
                        dt);
        }

        //force field point reflection
        private static void AddUpdate(Span<float> dx, Span<float> dy, Span<float> dz, BodyVelocityWide velocity, int i, Vector3 correction)
        {
            dx[i] = correction.X;
            dy[i] = correction.Y;
            dz[i] = correction.Z;
        }

        private static void ApplyUpdate(ref BodyVelocityWide velocity, Vector3Wide updates, Vector<float> dt) => velocity.Linear += updates * dt;


        //..AG.. new vector3 ?? 
        private bool BodyOutOfCloud(Vector3Wide position, int i, out Vector3 correction) => this.BodyOutOfCloud(new Vector3(position.X[i], position.Y[i], position.Z[i]), out correction);
        private bool BodyOutOfCloud(Vector3 position, out Vector3 correction)
        {
            correction = this.GetCorrection(position);

            return this.OutOfCloud(position);
        }

        private Vector3 GetCorrection(Vector3 position) => this.cloudOrigin - position;
        private bool OutOfCloud(Vector3 position) => System.Math.Sqrt(Vector3.DistanceSquared(position, this.cloudOrigin)) >= this.cloudRadius;

    }
}
