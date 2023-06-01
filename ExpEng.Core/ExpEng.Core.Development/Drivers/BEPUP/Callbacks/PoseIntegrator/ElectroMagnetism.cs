using BepuPhysics;
using BepuUtilities;
using ExpEng.Core.Base.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator
{
    public struct ElectroMagentism : IPoseIntegratorCallbacks
    {
        public AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;
        public bool AllowSubstepsForUnconstrainedBodies => false;
        public bool IntegrateVelocityForKinematics => false;


        private readonly float mass = 1;
        private readonly CollidableProperty<float> charges;

        private Bodies bodies;

        public ElectroMagentism(CollidableProperty<float> charges) { this.charges = charges; }
        public ElectroMagentism(CollidableProperty<float> charges, float uniformMass) : this(charges) { this.mass = uniformMass; }

        public void Initialize(BepuPhysics.Simulation simulation)
        {
            this.charges.Initialize(simulation);
            this.bodies = simulation.Bodies;
        }

        public void PrepareForIntegration(float dt) { }

        public void IntegrateVelocity(Vector<int> bodyIndices,
                                      Vector3Wide position, 
                                      QuaternionWide orientation, 
                                      BodyInertiaWide localInertia, 
                                      Vector<int> integrationMask, 
                                      int workerIndex, 
                                      Vector<float> dt,
                                      ref BodyVelocityWide velocity)
        {

            Span<float> chargeValues = stackalloc float[Vector<float>.Count];
            Vector3Wide delta = new();

            this.LoadChargeValues(bodyIndices, ref chargeValues);

            //..AG..
            //Version error because external is next version beta of nuget project is using.
            //should correct itself when bepu 2.5 is released to nuget.
            //for now, will try to work around. could be difficult because 
            //no apparent other way of manipulating Vector3Wide individual lanes.
            //Idk, there should be but it isn't obvious 

            this.LoadPerBodyDelta(position, chargeValues, dt, ref delta); //..AG.. need masses ?? for dev not composite callback //will need additional integrator layer

            velocity.Linear += delta;
        }


        private void LoadChargeValues(Vector<int> bodyIndices, ref Span<float> chargeValues)
        {
            for (int i = 0; i < Vector<int>.Count; ++i)
            {
                var bodyIndex = bodyIndices[i];
                if (bodyIndex >= 0)
                {
                    var bodyHandle = this.bodies.ActiveSet.IndexToHandle[bodyIndex];
                    chargeValues[i] = this.charges[bodyHandle];
                }
            }
        }

        private void LoadPerBodyDelta(Vector3Wide position, Span<float> chargeValues, Vector<float> dt, ref Vector3Wide delta)
        {
            for (int i = 0; i < chargeValues.Length; i++)
            {
                if (!Charged(chargeValues, i)) continue;

                var fNet = new Vector3();

                Vector3Wide.ReadSlot(ref position, i, out var pos0);

                for (int j = 0; j < chargeValues.Length; j++)
                {
                    if (j == i) continue;
                    if (!Charged(chargeValues, i)) continue;

                    Vector3Wide.ReadSlot(ref position, j, out var pos1);

                    var v = pos1 - pos0;
                    var s = (float)((8.988e9f * chargeValues[i] * chargeValues[j]) / System.Math.Pow(v.Length(), 3));      

                    fNet += (v * s);
                }

                var vect = (fNet / this.mass) * dt[0];
                Vector3Wide.WriteSlot(vect, i, ref delta);
            }
        }

        private static bool Charged(Span<float> chargeValues, int i) => chargeValues[i] != 0f;
    }
}
