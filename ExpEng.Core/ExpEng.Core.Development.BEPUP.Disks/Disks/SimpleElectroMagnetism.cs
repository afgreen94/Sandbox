using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using BepuPhysics;
using BepuPhysics.CollisionDetection;
using ExpEng.Core.Development.Drivers.BEPUP;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.NarrowPhase;
using ExpEng.Core.Development.Drivers.BEPUP.Scenarios;
using BepuPhysics.Constraints;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator;
using BepuPhysics.Collidables;
using System.Diagnostics;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public unsafe class SimpleElectroMagnetism : Disk
    {
        public unsafe class DiskDescription : Drivers.BEPUP.DiskDescription, IDiskDescription
        {
            public unsafe static DiskDescription Instance => new();
            public unsafe override string Name => "Electromagentism - Simple";
            public unsafe override Guid Id => new("{16ff36c0-4ae1-4d9f-a855-946da6f623d2}");
            public unsafe override string Description => "Simple model of electromagentism";
        }

        private unsafe readonly IDiskDescription description = DiskDescription.Instance;
        public unsafe override IDiskDescription Description => this.description;

        protected override IScenarioDefinition SD => ScenarioDefinition.Instance;

        protected override IScenario CreateScenario() => this.scenarioFactory.CreateScenario<DefaultNarrowPhaseCallbacks,
                                                                                             ElectroMagentism>(this.BufferPool, this.SD);

        public unsafe class ScenarioDefinition : ScenarioInitializerScenarioDefinition, ScenarioDefinition.IScenarioDefinition
        {
            public static ScenarioDefinition Instance => new();


            private readonly float springFrequency = 30;
            private readonly float springDampingRatio = 1;

            private readonly float uniformParticleMass = 1f;

            private unsafe readonly CollidableProperty<float> charges = new();

            private unsafe INarrowPhaseCallbacks narrowPhaseCallbacks => new DefaultNarrowPhaseCallbacks(new SpringSettings(this.springFrequency, this.springDampingRatio));
            private unsafe IPoseIntegratorCallbacks poseIntegratorCallbacks => new ElectroMagentism(this.charges, this.uniformParticleMass);
            private unsafe SolveDescription solveDescription => new(4, 1);
            private unsafe ICameraSettings initialCameraSettings => 
            new CameraSettings()
            {
                
            };

            public override INarrowPhaseCallbacks NarrowPhaseCallbacks => this.narrowPhaseCallbacks;
            public override IPoseIntegratorCallbacks PoseIntegratorCallbacks => this.poseIntegratorCallbacks;
            public override SolveDescription SolveDescription => this.solveDescription;
            public override ICameraSettings InitialCameraSettings => this.initialCameraSettings;

            public override void InitializeScenario(IScenario scenario)
            {
                var particleRadius = 5f;

                var particleShape = new Sphere(particleRadius);
                var particleInertia = particleShape.ComputeInertia(this.uniformParticleMass);
                var particleShapeIdx = scenario.Simulation.Shapes.Add(particleShape);

                var charge = 2f;

                var posPose = new RigidPose(new Vector3(50, 0, 0));
                var negPose = new RigidPose(new Vector3(-50, 0, 0));

                var pos = BodyDescription.CreateDynamic(posPose, particleInertia, particleShapeIdx, 0.01f);
                var neg = BodyDescription.CreateDynamic(negPose, particleInertia, particleShapeIdx, 0.01f);

                var posHandle = scenario.Simulation.Bodies.Add(pos);
                var negHandle = scenario.Simulation.Bodies.Add(neg);

                this.charges.Allocate(posHandle) = charge;
                this.charges.Allocate(negHandle) = -1 * charge;
            }

            public interface IScenarioDefinition : IScenarioInitializerScenarioDefinition { }
        }
    }
}
