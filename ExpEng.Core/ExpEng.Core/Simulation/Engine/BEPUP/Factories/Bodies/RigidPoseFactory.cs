using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics;
using ExpEng.Core.Base.Factories;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies
{
    public abstract class RigidPoseFactory : Factory, IRigidPoseFactory
    {
        public abstract RigidPose CreateRigidPose();
    }
}
