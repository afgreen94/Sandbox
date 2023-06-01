using BepuPhysics.CollisionDetection;
using BepuPhysics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepuUtilities.Memory;

namespace ExpEng.Core.Development.Drivers.BEPUP.Scenarios
{
    public class Scenario : IScenario
    {
        public BepuPhysics.Simulation Simulation { get; set; }

        public BufferPool BufferPool { get; set; }
    }

    public interface IScenario
    {
        BepuPhysics.Simulation Simulation { get; }
        BufferPool BufferPool { get; }
        //.AG.. etc 
    }
}
