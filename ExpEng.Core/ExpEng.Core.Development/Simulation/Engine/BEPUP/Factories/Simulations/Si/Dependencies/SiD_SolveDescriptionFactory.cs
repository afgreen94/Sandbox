using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using BepuPhysics;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies
{
    public class SiD_SolveDescriptionFactory : SolveDescriptionFactory, SiD_SolveDescriptionFactory.ISiD_SolveDescriptionFactory
    {
        private readonly int velocityIterationCount = 8;
        private readonly int substepCount = 1;
        public override SolveDescription CreateSolveDescription() => new(velocityIterationCount, substepCount);

        public interface ISiD_SolveDescriptionFactory : ISolveDescriptionFactory { }
    }
}
