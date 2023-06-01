using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepuPhysics;
using BepuPhysics.CollisionDetection;

namespace ExpEng.Core.Development.Drivers.BEPUP.Scenarios
{
    public abstract class ScenarioInitializerScenarioDefinition<TNPC,TPIC> : ScenarioDefinition<TNPC,TPIC>, IScenarioInitializerScenarioDefinition<TNPC, TPIC>
        where TNPC : struct, INarrowPhaseCallbacks
        where TPIC : struct, IPoseIntegratorCallbacks
    {
        public abstract void InitializeScenario(IScenario scenario);
    }
    public abstract class ScenarioInitializerScenarioDefinition : ScenarioDefinition, IScenarioInitializerScenarioDefinition
    {
        public abstract void InitializeScenario(IScenario scenario);
    }

    public interface IScenarioInitializerScenarioDefinition : IScenarioDefinition
    {
        void InitializeScenario(IScenario scenario);
    }

    public interface IScenarioInitializerScenarioDefinition<TNPC,TPIC> : IScenarioDefinition<TNPC,TPIC>
        where TNPC : struct, INarrowPhaseCallbacks
        where TPIC : struct, IPoseIntegratorCallbacks
    {
        void InitializeScenario(IScenario scenario);
    }
}
