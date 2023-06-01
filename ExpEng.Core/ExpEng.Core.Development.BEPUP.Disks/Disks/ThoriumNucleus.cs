using ExpEng.Core.Development.Drivers.BEPUP.Scenarios;
using ExpEng.Core.Development.Drivers.BEPUP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepuPhysics.CollisionDetection;
using BepuPhysics;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.NarrowPhase;
using BepuPhysics.Constraints;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator;
using BepuPhysics.Collidables;
using System.Numerics;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public unsafe class ThoriumNucleus : Disk
    {

        public unsafe class DiskDescription : Drivers.BEPUP.DiskDescription, IDiskDescription
        {
            public unsafe override string Name => "Thorium Nucleus";
            public unsafe override Guid Id => new("{2bf0f45f-cac9-4e29-89a9-6f40dbde35c9}");
            public unsafe override string Description => "Model a Thorium-232 nucleus";
        }

        private unsafe readonly DiskDescription description = new();
        public unsafe override IDiskDescription Description => this.description;

        protected unsafe override IScenarioDefinition SD => ScenarioDefinition.Instance;
        protected unsafe override IScenario CreateScenario() => this.scenarioFactory.CreateScenario<DefaultNarrowPhaseCallbacks,
                                                                                                    DefaultPoseIntegratorCallbacks>(this.BufferPool, this.SD);

        public unsafe class ScenarioDefinition : ScenarioInitializerScenarioDefinition, ScenarioDefinition.IScenarioDefinition
        {
            public unsafe static ScenarioDefinition Instance => new();

            private unsafe readonly CollidableProperty<float> charges = new();

            private unsafe INarrowPhaseCallbacks narrowPhaseCallbacks => new DefaultNarrowPhaseCallbacks(new SpringSettings(30, 1));
            private unsafe IPoseIntegratorCallbacks poseIntegratorCallbacks => new Drivers.BEPUP.Callbacks.PoseIntegrator.ElectroMagentism(this.charges);
            private unsafe SolveDescription solveDescription => new(4, 1);
            private unsafe ICameraSettings cameraSettings =>
            new CameraSettings()
            {

            };

            public unsafe override INarrowPhaseCallbacks NarrowPhaseCallbacks => this.narrowPhaseCallbacks;
            public unsafe override IPoseIntegratorCallbacks PoseIntegratorCallbacks => this.poseIntegratorCallbacks;
            public unsafe override SolveDescription SolveDescription => this.solveDescription;
            public override unsafe ICameraSettings InitialCameraSettings => this.cameraSettings;

            private const int IsotopeNumber = 232;
            private const int AtomicNumber = 90;

            private Vector3 nuclearOrigin;
            private float nuclearRadiusPm = 179.8f;

            private readonly Random rand = new();

            

            public override void InitializeScenario(IScenario scenario)
            {
                //scale 

                //mass 
                // 1 mass unit = 1 amu 
                //distance 
                //1 distance unit = 1 pm
                //charge ?? 
                //1 charge unit = 1 e

                //1 neutron mass = 

                //radi for density 1 amu / pm^3:
                //neutron = 1.335 pm 
                //proton = 1.334 pm 

                var neutronRadiusPm = 1.335f;
                var protonRadiusPm = 1.334f;

                var neutronMassAmu = 1.009f;
                var protonMassAmu = 1.007f;

                var neutron = new Sphere(neutronRadiusPm);
                var proton = new Sphere(protonRadiusPm);

                var neutronInertia = neutron.ComputeInertia(neutronMassAmu);
                var protonInertia = proton.ComputeInertia(protonMassAmu);

                var neutronShapeIdx = scenario.Simulation.Shapes.Add(neutron);
                var protonShapeIdx = scenario.Simulation.Shapes.Add(proton);

                var existing = new List<RigidPose>();

                for (int i = 0; i < AtomicNumber; i++)
                {
                    var n = this.GetRandomPositionInNucleus(existing, neutronRadiusPm);

                    existing.Add(n);

                    var p = this.GetRandomPositionInNucleus(existing, neutronRadiusPm);

                    existing.Add(p);

                    var neutronBody = BodyDescription.CreateDynamic(n, neutronInertia, neutronShapeIdx, 0.01f);
                    var protonBody = BodyDescription.CreateDynamic(p, protonInertia, protonShapeIdx, 0.01f);

                    scenario.Simulation.Bodies.Add(neutronBody);
                    var pHandle = scenario.Simulation.Bodies.Add(protonBody);
                    this.charges.Allocate(pHandle) = 1;
                }

            }

            private RigidPose GetRandomPositionInNucleus(List<RigidPose> existing, float radius)
            {
                RigidPose ret;

                do
                    ret = this.GetRandomPositionInNucleusCore();
                while (Collides(ret, existing, radius));

                return ret;
            }

            private static bool Collides(RigidPose pose, List<RigidPose> existing, float radius) => false;
            //{
            //    foreach(var e in existing)
            //        if ((pose.Position.X + radius) >= (e.Position.X - radius) ||
            //           (pose.Position.X - radius) <= (e.Position.X + radius) ||
            //           (pose.Position.Y + radius) >= (e.Position.Y - radius) ||
            //           (pose.Position.Y - radius) <= (e.Position.Y + radius) ||
            //           (pose.Position.Z + radius) >= (e.Position.Z - radius) ||
            //           (pose.Position.Z - radius) <= (e.Position.Z + radius))
            //            return true;

            //    return false;
            //}

            private RigidPose GetRandomPositionInNucleusCore()
            {
                var x = this.GetRandomPosition(this.nuclearOrigin.X);
                var y = this.GetRandomPosition(this.nuclearOrigin.Y);
                var z = this.GetRandomPosition(this.nuclearOrigin.Z);

                return new RigidPose(new Vector3(x, y, z));
            }

            private float GetRandomPosition(float origin) => (float)(this.rand.Next((int)(origin - this.nuclearRadiusPm), (int)(origin + this.nuclearRadiusPm)) + this.rand.NextDouble());

            public unsafe interface IScenarioDefinition : IScenarioInitializerScenarioDefinition { }
        }
    }
}
