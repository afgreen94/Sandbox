using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

using BepuPhysics;
using ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Simulations.Si.Dependencies
{
    public class SiD_TimeStepperFactory : TimeStepperFactory, SiD_TimeStepperFactory.ISiD_TimeStepperFactory
    {
        public override ITimestepper CreateTimeStepper() => new DefaultTimestepper();

        public interface ISiD_TimeStepperFactory : ITimeStepperFactory { }
    }
}
