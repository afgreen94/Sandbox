using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepuPhysics;
using BepuPhysics.CollisionDetection;

namespace ExpEng.Core.Development.Drivers.BEPUP.Scenarios
{
    public interface IScenarioInitializer
    {
        void InitializeScenario<TNPC, TPIC>(IScenario scenario, IScenarioDefinition scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks;
        void InitializeScenario<TNPC,TPIC>(IScenario scenario, IScenarioDefinition<TNPC,TPIC> scenarionDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks;
    }

    public class ScenarioDefinitionScenarioInitializer : IScenarioInitializer
    {
        public void InitializeScenario<TNPC,TPIC>(IScenario scenario, IScenarioDefinition scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks
        {
            if (scenarioDefinition is IScenarioInitializerScenarioDefinition)
                ((IScenarioInitializerScenarioDefinition)scenarioDefinition).InitializeScenario(scenario); //..AG.. there's a way to refactor with pattern matching ?? 
            else
                throw new Exception("..AG..");
        }
        public void InitializeScenario<TNPC,TPIC>(IScenario scenario, IScenarioDefinition<TNPC,TPIC> scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks
        {
            if (scenarioDefinition is IScenarioInitializerScenarioDefinition<TNPC, TPIC>)
                ((IScenarioInitializerScenarioDefinition<TNPC, TPIC>)scenarioDefinition).InitializeScenario(scenario); //..AG.. there's a way to refactor with pattern matching ?? 
            else
                throw new Exception("..AG..");
        }
    }
}
