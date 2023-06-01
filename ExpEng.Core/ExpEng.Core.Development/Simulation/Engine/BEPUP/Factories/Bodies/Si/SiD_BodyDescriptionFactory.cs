using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics;
using BepuPhysics.Collidables;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public abstract class SiD_BodyDescriptionFactory : BodyDescriptionFactory, ISiD_BodyDescriptionFactory
    {
        public SiD_BodyDescriptionFactory(IRigidPoseFactory rigidPoseFactory,
                                          IBodyVelocityFactory velocityFactory,
                                          IBodyLocalInertiaFactory localInertiaFactory,
                                          ICollidableDescriptionFactory collidableFactory,
                                          IBodyActivityDescriptionFactory activityFactory) :
                                          base(rigidPoseFactory,
                                                 velocityFactory,
                                                 localInertiaFactory,
                                                 collidableFactory,
                                                 activityFactory)
        {
        }
    }

    public interface ISiD_BodyDescriptionFactory : IBodyDescriptionFactory { }


}
