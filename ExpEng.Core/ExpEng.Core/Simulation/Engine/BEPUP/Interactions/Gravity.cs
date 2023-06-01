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
    public struct PerBodyGravity : IInteraction
    {
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

        public unsafe void PrepareForIntegration(float dt) { }

    }

    public unsafe struct PointGravity : IInteraction
    {

        private readonly Vector3 point;

        public unsafe void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position,
                                      QuaternionWide orientation,
                                      BodyInertiaWide localInertia,
                                      Vector<int> integrationMask,
                                      int workerIndex,
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity)
        {



        }

        public unsafe void PrepareForIntegration(float dt) { }
    }

    public unsafe struct MultiPointGravity : IInteraction
    {
        public unsafe void IntegrateVelocity(Vector<int> bodyIndices,
                              Vector3Wide position,
                              QuaternionWide orientation,
                              BodyInertiaWide localInertia,
                              Vector<int> integrationMask,
                              int workerIndex,
                              Vector<float> dt,
                              ref BodyVelocityWide velocity)
        {



        }

        public unsafe void PrepareForIntegration(float dt) { }
    }

    public struct DominantBodyGravity : IInteraction
    {
        public unsafe void PrepareForIntegration(float dt) { }

        public unsafe void IntegrateVelocity(Vector<int> bodyIndices,
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

    public struct ComparableBodyGravity : IInteraction
    {
        public unsafe void PrepareForIntegration(float dt) { }

        public unsafe void IntegrateVelocity(Vector<int> bodyIndices,
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
