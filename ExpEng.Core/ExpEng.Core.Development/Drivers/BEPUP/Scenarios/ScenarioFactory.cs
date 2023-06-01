using ExpEng.Core.Contracts.Simulation.Engine.BEPUP.Factories.Simulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities.Memory;


using BepuPhysics.CollisionDetection;
using System.Diagnostics.CodeAnalysis;

namespace ExpEng.Core.Development.Drivers.BEPUP.Scenarios
{
    public abstract class ScenarioFactory : IScenarioFactory
    {
        protected readonly ISimulationFactory simulationFactory;
        protected readonly IScenarioInitializer scenarioInitializer;

        public ScenarioFactory(ISimulationFactory simulationFactory, IScenarioInitializer scenarioInitializer)
        {
            this.simulationFactory = simulationFactory;
            this.scenarioInitializer = scenarioInitializer;
        }

        public virtual IScenario CreateScenario<TNPC, TPIC>(BufferPool bufferPool, IScenarioDefinition scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks
        {

            if (scenarioDefinition.NarrowPhaseCallbacks is not TNPC || scenarioDefinition.PoseIntegratorCallbacks is not TPIC)
                throw new IllegalCallbacksTypesException<TNPC, TPIC>(scenarioDefinition.NarrowPhaseCallbacks.GetType(), 
                                                                     scenarioDefinition.PoseIntegratorCallbacks.GetType());

            var scenario = this.CreateScenarioCore(bufferPool,
                                                   (TNPC)scenarioDefinition.NarrowPhaseCallbacks,
                                                   (TPIC)scenarioDefinition.PoseIntegratorCallbacks,
                                                   scenarioDefinition.SolveDescription);

            this.scenarioInitializer.InitializeScenario<TNPC, TPIC>(scenario, scenarioDefinition);

            return scenario;
        }

        public virtual IScenario CreateScenario<TNPC, TPIC>(BufferPool bufferPool, IScenarioDefinition<TNPC, TPIC> scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks
        {
            var scenario = this.CreateScenarioCore(bufferPool,
                                                   scenarioDefinition.NarrowPhaseCallbacks,
                                                   scenarioDefinition.PoseIntegratorCallbacks,
                                                   scenarioDefinition.SolveDescription);

            this.scenarioInitializer.InitializeScenario(scenario, scenarioDefinition);

            return scenario;
        }

        protected IScenario CreateScenarioCore<TNPC,TPIC>(BufferPool bufferPool, IScenarioDefinition<TNPC,TPIC> scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks
        {
            var scenario = this.CreateScenarioCore(bufferPool,
                                                   scenarioDefinition.NarrowPhaseCallbacks,
                                                   scenarioDefinition.PoseIntegratorCallbacks,
                                                   scenarioDefinition.SolveDescription);

            this.scenarioInitializer.InitializeScenario(scenario, scenarioDefinition);

            return scenario;
        }

        protected IScenario CreateScenarioCore<TNPC, TPIC>(BufferPool bufferPool,
                                                           TNPC narrowPhaseCallbacks,
                                                           TPIC poseIntegratorCallbacks,
                                                           SolveDescription solveDescription)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks => new Scenario()
                                                             {
                                                                 Simulation = this.simulationFactory.Create(bufferPool,
                                                                                                            narrowPhaseCallbacks,
                                                                                                            poseIntegratorCallbacks,
                                                                                                            solveDescription),
                                                                 BufferPool = bufferPool
                                                             };
    }


    public class DefaultScenarioFactory : ScenarioFactory, IScenarioFactory
    {
        public DefaultScenarioFactory() : base(new DefaultSimulationFactory(), new ScenarioDefinitionScenarioInitializer()) { }
        public DefaultScenarioFactory(ISimulationFactory simulationFactory, IScenarioInitializer scenarioInitializer) : base(simulationFactory, scenarioInitializer) { }
    }

    public class DefaultSimulationFactory : ISimulationFactory
    {
        public BepuPhysics.Simulation Create<TNPC,TPIC>(BufferPool bp, TNPC npc, TPIC pic, SolveDescription sd)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks => BepuPhysics.Simulation.Create(bp, npc, pic, sd);
    }

    



    public interface ISimulationFactory
    {
        BepuPhysics.Simulation Create<TNPC, TPIC>(BufferPool bp, TNPC npc, TPIC pic, SolveDescription sd)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks;
    }

    public interface IScenarioCreationWrapper
    {
        public IScenarioDefinition Definition { get; }
        public IScenarioFactory Factory { get; }
    }

    public interface IScenarioFactory
    {
        IScenario CreateScenario<TNPC, TPIC>(BufferPool bufferPool, IScenarioDefinition scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks;

        IScenario CreateScenario<TNPC, TPIC>(BufferPool bufferPool, IScenarioDefinition<TNPC,TPIC> scenarioDefinition)
            where TNPC : struct, INarrowPhaseCallbacks
            where TPIC : struct, IPoseIntegratorCallbacks;
    }

    public interface IScenarioFactory<TNPC,TPIC>
    {
        IScenario CreateScenario(BufferPool bufferPool, IScenarioDefinition scenarioDefinition);
    }


    public class IllegalCallbacksTypesException<TNPC, TPIC> : Exception
    {

        private readonly string tnpcE;
        private readonly string tpicE;

        private readonly string tnpc;
        private readonly string tpic;

        private readonly bool addendum;

        public IllegalCallbacksTypesException(Type tnpc, Type tpic)
        {
            this.tnpc = tnpc.Name;
            this.tpic = tpic.Name;

            var etn = typeof(TNPC);
            var etp = typeof(TPIC);

            this.tnpcE = nameof(etn);
            this.tpicE = nameof(etp);

            this.addendum = !(tnpc.IsAssignableFrom(etn) && tpic.IsAssignableFrom(etp));
        }


        private const string Header = "Illegal CallbackType";
        private const string Addendum = "(s)";

        private const string Body = ":\n\tTNPC_Given: {0} TNPC_Expected: {1} \n\tTPIC_Given: {2} TPIC_Expected: {3}";

        public override string Message
        {
            get
            {
                var sb = new StringBuilder(Header);

                if (this.addendum)
                    sb.Append(Addendum);

                sb.Append(string.Format(Body, this.tnpc, this.tnpcE, this.tpic, this.tpicE));

                return sb.ToString();
            }
        }
    }

}
