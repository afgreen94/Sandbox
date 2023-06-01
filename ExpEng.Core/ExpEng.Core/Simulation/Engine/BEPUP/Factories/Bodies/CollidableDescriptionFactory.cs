using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics.Collidables;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies
{
    public abstract class CollidableDescriptionFactory : ICollidableDescriptionFactory
    {
        public abstract CollidableDescription CreateCollidableDescription();
    }
}
