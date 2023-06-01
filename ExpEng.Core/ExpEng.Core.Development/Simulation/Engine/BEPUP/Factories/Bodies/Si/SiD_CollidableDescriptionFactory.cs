using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics.Collidables;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public class SiD_CollidableDescriptionFactory : CollidableDescriptionFactory, ISiD_CollidableDescriptionFactory
    {
        public override CollidableDescription CreateCollidableDescription()
        {
            return new CollidableDescription();
        }
    }

    public interface ISiD_CollidableDescriptionFactory : ICollidableDescriptionFactory { }
}
