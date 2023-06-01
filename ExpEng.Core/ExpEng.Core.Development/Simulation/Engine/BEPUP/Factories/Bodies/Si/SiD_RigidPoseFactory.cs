using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics;
using ExpEng.Core.Simulation.Engine.BEPUP.Euclidean.Reference;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public class SiD_RigidPoseFactory : RigidPoseFactory, ISiD_RigidPoseFactory
    {
        public override RigidPose CreateRigidPose() => RigidPose.Identity;
    }

    public interface ISiD_RigidPoseFactory : IRigidPoseFactory { }
}
