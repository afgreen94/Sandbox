using BepuPhysics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepuPhysics.Collidables;
using ExpEng.Core.Contracts.Base;

namespace ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies
{
    public interface IBodyDescriptionFactory 
    {
        BodyDescription CreateBodyDescription();
    }

    public interface IRigidPoseFactory { RigidPose CreateRigidPose(); }
    public interface IBodyVelocityFactory { BodyVelocity CreateBodyVelocity(); }
    public interface IBodyLocalInertiaFactory { BodyInertia CreateBodyLocalInertia(); }
    public interface ICollidableDescriptionFactory { CollidableDescription CreateCollidableDescription(); }
    public interface IBodyActivityDescriptionFactory { BodyActivityDescription CreateBodyActivityDescription(); }

}
