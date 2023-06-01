using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public class SiD_BodyActivityFactory : BodyActivityDescriptionFactory, ISiD_BodyActivityDescriptionFactory
    {
        public override BodyActivityDescription CreateBodyActivityDescription()
        {
            return new BodyActivityDescription();
        }
    }

    public interface ISiD_BodyActivityDescriptionFactory : IBodyActivityDescriptionFactory { }
}
