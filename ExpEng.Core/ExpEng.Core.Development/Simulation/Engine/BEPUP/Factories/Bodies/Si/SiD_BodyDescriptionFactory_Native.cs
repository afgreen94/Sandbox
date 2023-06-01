using BepuPhysics;
using BepuPhysics.Collidables;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Base;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public class SiD_BodyDescriptionFactory_Navtive : BodyDescriptionFactory, ISiD_BodyDescriptionFactory_Navtive
    {
        public SiD_BodyDescriptionFactory_Navtive(IRigidPoseFactory rigidPoseFactory,
                                                  ICollidableDescriptionFactory collidableDescriptionFactory,
                                                  IBodyActivityDescriptionFactory bodyActivityDescriptionFactory)
                                                  : base(rigidPoseFactory, 
                                                         default, 
                                                         default, 
                                                         collidableDescriptionFactory, 
                                                         bodyActivityDescriptionFactory)
        {
        }

        public SiD_BodyDescriptionFactory_Navtive(ISiD_RigidPoseFactory rigidPoseFactory,
                                                  ISiD_CollidableDescriptionFactory collidableDescriptionFactory,
                                                  ISiD_BodyActivityDescriptionFactory bodyActivityDescriptionFactory)
                                                  : this((IRigidPoseFactory)rigidPoseFactory,
                                                        (ICollidableDescriptionFactory)collidableDescriptionFactory, 
                                                        (IBodyActivityDescriptionFactory)bodyActivityDescriptionFactory)
        {
        }

        public SiD_BodyDescriptionFactory_Navtive(IRigidPoseFactory rigidPoseFactory,
                                                  IBodyVelocityFactory velocityFactory,
                                                  IBodyLocalInertiaFactory localInertiaFactory,
                                                  ICollidableDescriptionFactory collidableFactory,
                                                  IBodyActivityDescriptionFactory activityFactory)
                                                  : base(rigidPoseFactory, 
                                                         velocityFactory, 
                                                         localInertiaFactory, 
                                                         collidableFactory, 
                                                         activityFactory)
        {
        }

        public SiD_BodyDescriptionFactory_Navtive(ISiD_RigidPoseFactory rigidPoseFactory,
                                                  ISiD_BodyVelocityFactory bodyVelocityFactory,
                                                  ISiD_BodyLocalInertiaFactory bodyLocalInertiaFactory,
                                                  ISiD_CollidableDescriptionFactory collidableDescriptionFactory,
                                                  ISiD_BodyActivityDescriptionFactory bodyActivityDescriptionFactory)
                                                  : this((IRigidPoseFactory)rigidPoseFactory, 
                                                         (IBodyVelocityFactory)bodyVelocityFactory, 
                                                         (IBodyLocalInertiaFactory)bodyLocalInertiaFactory,
                                                         (ICollidableDescriptionFactory)collidableDescriptionFactory, 
                                                         (IBodyActivityDescriptionFactory)bodyActivityDescriptionFactory)
        { }

        protected override BodyDescription CreateBodyDescriptionCore() => this.CreateKinematic();


        //private BodyDescription CreateDynamic()
        //{

        //    var rp = this.rigidPoseFactory.CreateRigidPose();
        //    var bv = this.velocityFactory.CreateBodyVelocity();

        //    //BodyDescription.CreateDynamic(rp, 
        //}
        private BodyDescription CreateKinematic()
        {
            var rp = this.rigidPoseFactory.CreateRigidPose();
            var cd = this.collidableFactory.CreateCollidableDescription();
            var bad = this.activityFactory.CreateBodyActivityDescription();

            return BodyDescription.CreateKinematic(rp, cd, bad);
        }

        protected override void InitializeCore()
        {
        }
    }

    public interface ISiD_BodyDescriptionFactory_Navtive : ISiD_BodyDescriptionFactory, IInitializable { }

}
