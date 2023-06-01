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
    public class SiD_BodyLocalInertiaFactory : BodyLocalInertiaFactory, ISiD_BodyLocalInertiaFactory
    {
        public override BodyInertia CreateBodyLocalInertia()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISiD_BodyLocalInertiaFactory : IBodyLocalInertiaFactory { }
}
