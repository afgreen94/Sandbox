using Abstractions;
using Abstractions.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ND_Drawing.Contracts.Engine;
using ND_Drawing.Contracts.Engine.Factories;

namespace ND_Drawing.Engine
{
    public abstract class EngineManagerBase : IEngineManager
    {

        private readonly IEngineFactory engineFactory;

        private readonly IContext<EngineManagerBase> context;

        private IEngine engine;

        private Task pollCycleTask;
        private Task updateCycleTask;
        private Task checksControlsCycleTask;

        public EngineManagerBase(IEngineFactory engineFactory,   
                                 IContext<EngineManagerBase> context)
        {
            this.engineFactory = engineFactory;
            this.context = context;
        }

        public async ValueTask<IResult> InitializeAsync(IConfiguration configuration)
        {
            //-ag among other things, create engine 

            var engineCreateResult = await this.engineFactory.CreateAsync(configuration).ConfigureAwait(false);

            return new Result();
        }

        protected virtual void StartEngineCycle() => this.engine.Start();
        



    }

    public interface IEngineSubscriber { }
    public interface IEnginePublisher { }
}
