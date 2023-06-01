using BepuPhysics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;

namespace ExpEng.Core.Simulation.Engine.BEPUP.Factories.Simulations
{
    public abstract class TimeStepperFactory : ITimeStepperFactory
    {
        public abstract ITimestepper CreateTimeStepper();
    }
}
