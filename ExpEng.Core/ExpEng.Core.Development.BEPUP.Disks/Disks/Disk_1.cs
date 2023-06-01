using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuPhysics.Constraints;
using BepuPhysics;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.NarrowPhase;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator;
using ExpEng.Core.Development.Drivers.BEPUP.Scenarios;
using ExpEng.Core.Development.Drivers.BEPUP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public unsafe class Disk_1 : Disk
    {
        public class DiskDescription : Drivers.BEPUP.DiskDescription, IDiskDescription
        {
            public static DiskDescription Instance => new();
            public override string Name => "Disk_1";
            public override Guid Id => new("{5a3f00c8-f84d-455d-88f7-da0d664fa161}");
        }

        private readonly DiskDescription description = DiskDescription.Instance;
        public override IDiskDescription Description => this.description;

        protected unsafe override IScenarioDefinition SD => ScenarioDefinition.Instance;
        protected unsafe override IScenario CreateScenario() => this.scenarioFactory.CreateScenario<DefaultNarrowPhaseCallbacks, 
                                                                                                    SphericalHardReflectorField>(this.BufferPool, this.SD);

        public Disk_1() : base() { }
        public Disk_1(IScenarioFactory scenarioFactory) : base(scenarioFactory) { }

        public unsafe class ScenarioDefinition : ScenarioInitializerScenarioDefinition, ScenarioDefinition.IScenarioDefinition
        {
            public unsafe static ScenarioDefinition Instance => new();

            private unsafe readonly float springFrequency = 30;
            private unsafe readonly float springDampeningRatio = 1;

            //floater to cloud radius ratio => density 
            //floater velocity to density ratio => clusterfuckery 

            private unsafe readonly Vector3 cloudOrigin = new();
            private unsafe readonly float cloudRadius = 100f;

            private unsafe readonly int floaterCount = 90;

            private unsafe readonly float floaterRadius = 5f;
            private unsafe readonly float floaterMass = 1f;

            private unsafe readonly float floaterMinInitVelocity = 80f;
            private unsafe readonly float floaterMaxInitVelocity = 81f;

            private unsafe INarrowPhaseCallbacks narrowPhaseCallbacks => new DefaultNarrowPhaseCallbacks(new SpringSettings(this.springFrequency, this.springDampeningRatio));
            private unsafe IPoseIntegratorCallbacks poseIntegratorCallbacks => new SphericalHardReflectorField(this.cloudOrigin, this.cloudRadius);
            private unsafe SolveDescription solveDescription => new(4, 1);
            private unsafe ICameraSettings initialCameraSettings => new CameraSettings()
            {
                Position = new Vector3(0, 25, 370),
                Yaw = 0,
                Pitch = 0
            };

            public unsafe override INarrowPhaseCallbacks NarrowPhaseCallbacks => this.narrowPhaseCallbacks;
            public unsafe override IPoseIntegratorCallbacks PoseIntegratorCallbacks => this.poseIntegratorCallbacks;
            public unsafe override SolveDescription SolveDescription => this.solveDescription;
            public unsafe override ICameraSettings InitialCameraSettings => this.initialCameraSettings;

            public unsafe override void InitializeScenario(IScenario scenario)
            {
                var floaterShape = new Sphere(this.floaterRadius);

                var shapeIdx = scenario.Simulation.Shapes.Add(floaterShape);
                var inertia = floaterShape.ComputeInertia(this.floaterMass);

                new FloaterHelper(this.cloudOrigin,
                                  this.cloudRadius,
                                  this.floaterMinInitVelocity,
                                  this.floaterMaxInitVelocity).GetFloaterPositionAndVelocities(this.floaterCount,
                                                                                      out var poses,
                                                                                      out var velocities);


                for (int i = 0; i < floaterCount; i++)
                {
                    var f = BodyDescription.CreateDynamic(poses[i], velocities[i], inertia, shapeIdx, 0.01f);
                    scenario.Simulation.Bodies.Add(f);
                }


            }

            //private void AddOriginMarker()
            //{

            //}


            private class FloaterHelper
            {
                private readonly Vector3 cloudOrigin;

                private readonly float deltaX;
                private readonly float deltaY;
                private readonly float deltaZ;

                private readonly float minVelocity;
                private readonly float maxVelocity;

                private readonly Random rand = new();

                public FloaterHelper(Vector3 cloudOrigin, float cloudRadius, float minVelocity, float maxVelocity)
                {
                    this.cloudOrigin = cloudOrigin;

                    this.minVelocity = minVelocity;
                    this.maxVelocity = maxVelocity;

                    this.deltaX = System.Math.Abs(this.cloudOrigin.X - cloudRadius);
                    this.deltaY = System.Math.Abs(this.cloudOrigin.Y - cloudRadius);
                    this.deltaZ = System.Math.Abs(this.cloudOrigin.Z - cloudRadius);
                }

                public void GetFloaterPositionAndVelocities(int floaterCount, out List<Vector3> poses, out List<Vector3> velocities)
                {
                    var runningPos = new HashSet<Vector3>();
                    var runningVel = new List<Vector3>();

                    for (int i = floaterCount; i > 0; i--)
                    {
                        while (runningPos.Add(this.GetRandPosVector())) break;

                        runningVel.Add(this.GetRandVelVector());
                    }

                    poses = runningPos.ToList();
                    velocities = runningVel.ToList();
                }
                private Vector3 GetRandPosVector() => new()
                {
                    X = this.GetRand(this.cloudOrigin.X, this.deltaX),
                    Y = this.GetRand(this.cloudOrigin.Y, this.deltaY),
                    Z = this.GetRand(this.cloudOrigin.Z, this.deltaZ)
                };

                private Vector3 GetRandVelVector()
                {
                    Vector3 v;
                    float l;

                    do
                    {
                        v = new Vector3()
                        {
                            X = this.GetVRand(),
                            Y = this.GetVRand(),
                            Z = this.GetVRand()
                        };

                        l = v.Length();

                    } while (l > this.maxVelocity || l < this.minVelocity);

                    return v;
                }

                private float GetRand(float o, float d) => (float)(this.rand.Next((int)(o - d), (int)(o + d)) + this.rand.NextDouble());
                private float GetVRand() => (float)(this.rand.Next((int)this.minVelocity, (int)this.maxVelocity) + this.rand.NextDouble());
            }

            public unsafe interface IScenarioDefinition : IScenarioInitializerScenarioDefinition { }
        }
    }
}
