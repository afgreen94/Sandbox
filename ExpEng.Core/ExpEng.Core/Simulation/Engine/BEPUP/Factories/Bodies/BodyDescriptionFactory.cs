using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Base.Factories;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Base;

using BepuPhysics;
using BepuPhysics.Collidables;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies
{
    public abstract class BodyDescriptionFactory : InitializableFactory, IBodyDescriptionFactory, IInitializable
    {

        protected readonly IRigidPoseFactory rigidPoseFactory;
        protected readonly IBodyVelocityFactory velocityFactory;
        protected readonly IBodyLocalInertiaFactory localInertiaFactory;
        protected readonly ICollidableDescriptionFactory collidableFactory;
        protected readonly IBodyActivityDescriptionFactory activityFactory;

        protected bool isInitialized = false;


        protected BodyDescriptionFactory(IRigidPoseFactory rigidPoseFactory,
                                         IBodyVelocityFactory velocityFactory,
                                         IBodyLocalInertiaFactory localInertiaFactory,
                                         ICollidableDescriptionFactory collidableFactory,
                                         IBodyActivityDescriptionFactory activityFactory)
        {
            this.rigidPoseFactory = rigidPoseFactory;
            this.velocityFactory = velocityFactory;
            this.localInertiaFactory = localInertiaFactory;
            this.collidableFactory = collidableFactory;
            this.activityFactory = activityFactory;
        }

        public virtual BodyDescription CreateBodyDescription()
        {
            this.EnsureInitialized();

            return this.CreateBodyDescriptionCore();
        }

        protected virtual BodyDescription CreateBodyDescriptionCore() => new()
        {
            Pose = this.rigidPoseFactory.CreateRigidPose(),
            Velocity = this.velocityFactory.CreateBodyVelocity(),
            LocalInertia = this.localInertiaFactory.CreateBodyLocalInertia(),
            Collidable = this.collidableFactory.CreateCollidableDescription(),
            Activity = this.activityFactory.CreateBodyActivityDescription()
        };
    }
}
