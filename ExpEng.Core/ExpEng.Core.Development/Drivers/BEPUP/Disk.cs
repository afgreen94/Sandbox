using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BepuPhysics.Collidables;
using BepuPhysics.Constraints;
using BepuPhysics;
using BepuUtilities.Memory;
using BepuUtilities;
using DemoContentLoader;
using DemoRenderer;
using Demos;

using ExpEng.Core.Development.Drivers.BEPUP.Callbacks;
//using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.NarrowPhase;
using ExpEng.Core.Development.Drivers.BEPUP.Callbacks.PoseIntegrator;
using ExpEng.Core.Development.Drivers.BEPUP.Scenarios;
using System.Security.Cryptography;
using BepuPhysics.CollisionDetection;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ExpEng.Core.Development.Drivers.BEPUP
{
    public abstract unsafe class Disk : Demo
    {

        protected unsafe readonly IScenarioFactory scenarioFactory = new DefaultScenarioFactory();
        protected unsafe abstract IScenarioDefinition SD { get; }

        public unsafe abstract IDiskDescription Description { get; }

        public Disk() { }
        public Disk(IScenarioFactory scenarioFactory)
        {
            this.scenarioFactory = scenarioFactory;
        }

        public unsafe override void Initialize(ContentArchive content, Camera camera)
        {
            SetCamera(camera);
            SetSimulation();
        }

        protected unsafe abstract IScenario CreateScenario();

        protected unsafe virtual void SetSimulation() => Simulation = CreateScenario().Simulation;
        protected unsafe virtual void SetCamera(Camera camera)
        {
            camera.Position = this.SD.InitialCameraSettings.Position;
            camera.Yaw = this.SD.InitialCameraSettings.Yaw;
            camera.Pitch = this.SD.InitialCameraSettings.Pitch;

        }
    }
}
