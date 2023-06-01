using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Bodies;
using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Bodies;

using BepuPhysics;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public class SiD_BodyVelocityFactory : BodyVelocityFactory, ISiD_BodyVelocityFactory
    {
        public override BodyVelocity CreateBodyVelocity() => new(Vector3.Zero, Vector3.Zero);
    }

    public interface ISiD_BodyVelocityFactory : IBodyVelocityFactory { }
}
