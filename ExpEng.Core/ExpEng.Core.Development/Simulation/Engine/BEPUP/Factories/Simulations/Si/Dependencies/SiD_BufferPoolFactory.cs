using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using BepuUtilities.Memory;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies
{
    public class SiD_BufferPoolFactory : BufferPoolFactory, SiD_BufferPoolFactory.ISiD_BufferPoolFactory
    {
        public override BufferPool CreateBufferPool() => new();

        public interface ISiD_BufferPoolFactory : IBufferPoolFactory { }
    }
}
