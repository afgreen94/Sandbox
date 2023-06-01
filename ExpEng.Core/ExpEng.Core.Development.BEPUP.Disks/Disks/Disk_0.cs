using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuPhysics.Constraints;
using BepuPhysics;
using BepuUtilities.Memory;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator;
using ExpEng.Core.Development.Drivers.BEPUP.Scenarios;
using ExpEng.Core.Development.Drivers.BEPUP;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Callbacks.NarrowPhase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.NarrowPhase;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Identity.Client;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public unsafe class Disk_0 : Disk
    {

        public class DiskDescription : Drivers.BEPUP.DiskDescription, IDiskDescription
        {
            public static DiskDescription Instance => new();

            public override string Name => "Disk_0";
            public override Guid Id => new("{87f65028-aaa5-4ff2-8f44-50fdebf209b9}");
        }

        private readonly IDiskDescription description = DiskDescription.Instance;
        public override IDiskDescription Description => this.description;

        protected unsafe override IScenarioDefinition SD => ScenarioDefinition.Instance;
        protected override unsafe IScenario CreateScenario() => scenarioFactory.CreateScenario<SiD_NarrowPhaseCallbacks, PlanetaryGravityCallbacks>(this.BufferPool, this.SD);

        public Disk_0() : base() { }
        public Disk_0(IScenarioFactory scenarioFactory) : base(scenarioFactory) { }

        public unsafe class ScenarioDefinition : ScenarioInitializerScenarioDefinition, ScenarioDefinition.IScenarioDefinition
        {
            public unsafe static ScenarioDefinition Instance => new();

            private unsafe readonly static INarrowPhaseCallbacks _narrowPhaseCallbacks = new SiD_NarrowPhaseCallbacks(new SpringSettings(30, 1));
            private unsafe readonly static IPoseIntegratorCallbacks _poseIntegratorCallbacks = 
                new PlanetaryGravityCallbacks()
                {
                    PlanetCenter = new Vector3(),
                    Gravity = 100000
                };
            private unsafe readonly static SolveDescription _solveDescription = new(4, 1);
            private unsafe readonly static ICameraSettings _initialCameraSettings = 
                new CameraSettings()
                {
                    Position = new Vector3(110, -80, 12),
                    Yaw = 0,
                    Pitch = MathF.PI * -0.5f
                };

            public unsafe override INarrowPhaseCallbacks NarrowPhaseCallbacks => _narrowPhaseCallbacks;
            public unsafe override IPoseIntegratorCallbacks PoseIntegratorCallbacks => _poseIntegratorCallbacks;
            public unsafe override SolveDescription SolveDescription => _solveDescription;
            public override unsafe ICameraSettings InitialCameraSettings => _initialCameraSettings;

            public unsafe override void InitializeScenario(IScenario scenario)
            {
                var planetSize = 50f;
                var planetInitialPosition = new Vector3();

                AddPlanet(planetSize, planetInitialPosition, scenario);

                AddOrbiters(scenario);
            }

            private unsafe static void AddPlanet(float planetSize, Vector3 planetInitialPosition, IScenario scenario)
            {
                var planetShape = new Sphere(planetSize);
                var planetShapeIdx = scenario.Simulation.Shapes.Add(planetShape);
                var planet = new StaticDescription(planetInitialPosition, planetShapeIdx);

                scenario.Simulation.Statics.Add(planet);
            }

            private unsafe static void AddOrbiters(IScenario scenario)
            {
                var orbiter = new Sphere(1f);
                var inertia = orbiter.ComputeInertia(1);
                var orbiterShapeIndex = scenario.Simulation.Shapes.Add(orbiter);

                var spacing = new Vector3(5);



                const int length = 40;


                for (int i = 0; i < length; ++i)
                {
                    for (int j = 0; j < 20; ++j)
                    {
                        const int width = 40;
                        var origin = new Vector3(-50, 95, 0) + spacing * new Vector3(length * -0.5f, 0, width * -0.5f);

                        for (int k = 0; k < width; ++k)
                        {
                            scenario.Simulation.Bodies.Add(BodyDescription.CreateDynamic(
                                origin + new Vector3(i, j, k) * spacing, new Vector3(30, 0, 0), inertia, orbiterShapeIndex, 0.01f));
                        }
                    }
                }
            }

            public unsafe interface IScenarioDefinition : IScenarioInitializerScenarioDefinition { }
        }
    }
}
