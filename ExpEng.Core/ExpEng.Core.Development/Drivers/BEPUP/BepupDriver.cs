using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using ExpEng.Core.Drivers;
using ExpEng.Core.Contracts;
using ExpEng.Core.Simulation;
using ExpEng.Core.Simulation.Engine.BEPUP;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;


using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuPhysics.CollisionDetection.CollisionTasks;
using BepuPhysics.CollisionDetection.SweepTasks;
using BepuPhysics.Constraints;
using BepuPhysics.Constraints.Contact;
using BepuPhysics.Trees;
using BepuUtilities;
using BepuUtilities.Collections;
using BepuUtilities.Memory;
using ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Demos;
using Demos.Demos;
using DemoRenderer;
using DemoContentLoader;
using DemoContentBuilder;
using DemoUtilities;
using OpenTK;

using Factories = ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories;
using ExpEng.Console;

namespace ExpEng.Core.TestConsole.BEPUP
{
    public abstract class BEPUPDriver : Driver, IDriver
    {

        private IServiceProvider serviceProvider;
        private IConfiguration configuration;


        public ValueTask DriveCoreAsync(string[] args)
        {
            using var window = GetWindow();
            using var loop = GetGameLoop(window);

            var content = GetContentArchive(); 

            var demo = new DemoHarness(loop, content);

            loop.Run(demo);

            return new ValueTask();
        }

        private static Window GetWindow() => new("pretty cool multicolored window", 
                                                 new Int2((int)(DisplayDevice.Default.Width * 0.75f), (int)(DisplayDevice.Default.Height * 0.75f)),
                                                 WindowMode.Windowed);

        private static GameLoop GetGameLoop(Window window) => new(window);

        private static ContentArchive GetContentArchive()
        {
            using var stream = typeof(Program).Assembly.GetManifestResourceStream("Demos.Demos.contentarchive");
            return ContentArchive.Load(stream);
        }


        public override ValueTask DriveAsync(string[] args)
        {
            //..AG.. initially
            var useDefaultTimeStepper = false;
            var useDefaultSASArgs = false;

            this.Initialize(useDefaultTimeStepper, useDefaultSASArgs);

            using var scope = this.serviceProvider.CreateScope();

            var bodyFactory = scope.ServiceProvider.GetRequiredService<ISiD_BodyDescriptionFactory_Navtive>();
            var simulationFactory = scope.ServiceProvider.GetRequiredService<ISiD_SimulationFactory_EmbeddedTypes>();

            bodyFactory.Initialize();
            simulationFactory.Initialize();

            var sim = simulationFactory.CreateSimulation();

            sim.Bodies.Add(bodyFactory.CreateBodyDescription());
            sim.Bodies.Add(bodyFactory.CreateBodyDescription());

            sim.Timestep(0.01f);

            return new ValueTask();
        }



        public class SimulationWrapper : BepuPhysics.Simulation
        {

        }

        private void Initialize(bool useDefaultTimeStepper, bool useDefaultSASArgs)
        {
            this.configuration = new ConfigurationBuilder().Build();

            var sc = new ServiceCollection();

            this.RegisterServices(sc, useDefaultTimeStepper, useDefaultSASArgs);

            this.serviceProvider = sc.BuildServiceProvider();
        }

        private void RegisterServices(IServiceCollection sc, bool useDefaultTimeStepper = false, bool useDefaultSASArgs = false)
        {
            new Factories.Simulations.ServiceRegistrar(useDefaultTimeStepper, useDefaultSASArgs).RegisterServices(sc, this.configuration);
            new Factories.Bodies.ServiceRegistrar().RegisterServices(sc, this.configuration);
        }
    }


    public class BD0 : BEPUPDriver { }
}
