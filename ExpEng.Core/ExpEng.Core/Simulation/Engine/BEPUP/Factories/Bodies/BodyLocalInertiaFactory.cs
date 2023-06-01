using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Base.Factories;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies
{
    public abstract class BodyLocalInertiaFactory : Factory, IBodyLocalInertiaFactory
    {
        public abstract BodyInertia CreateBodyLocalInertia();
    }
}
