using Abstractions.Contracts;
using Abstractions;
using Base.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ND_Drawing.Contracts;
using ND_Drawing.Contracts.Engine;

namespace ND_Drawing
{
    public interface ICoordinator : IAsyncInitializable
    {
        Task<IResult> RunAsync();
    }

    //just in case
    public interface IScheduler { }

    public interface IEngineManager : IAsyncInitializable
    {

    }

    public interface IEngineManagerCache { }

    public interface IEnginePollingWrapper
    {

    }

    public interface IEngineUpdatingWrapper
    {

    }

    public interface ICoordinatorConfig { }

    public interface IEngineInitializeArgs { }

    public interface IInputPeriphery { }
    public interface IOutputPeriphery { }

    public abstract class CoordinatorBase : ICoordinator
    {

        private readonly IEngineFacade engine;
        private readonly IEngineManager engineManager;
        private readonly IInputManager inputManager;
        private readonly IOutputManager outputManager;
        //periphery refs ??
        private readonly IContext context;

        private IConfiguration configuration;

        public CoordinatorBase(IEngineFacade engine,
                               IEngineManager engineManager,
                               IInputManager inputManager,
                               IOutputManager outputManager,
                               IConfiguration configuration,
                               IContext<CoordinatorBase> context)
        {
            this.engine = engine;
            this.inputManager = inputManager;
            this.outputManager = outputManager;
        }

        public async ValueTask<IResult> InitializeAsync(IConfiguration configuration)
        {
            //set up all initialized thru di 

            return new Result();
        }

        public async Task<IResult> RunAsync() => await MainCycleAsync().ConfigureAwait(false);

        private async Task EngineInputCouple()
        {
            //need midware
        }

        private async Task EngineOutputCouple()
        {

        }
        protected async Task ChecksAsync()
        {

        }

        protected async Task ControlsAsync()
        {

        }

        protected async Task<IResult> MainCycleAsync()
        {
            try
            {
                var cycles = new Task[]
                {
                    EngineInputCouple(),
                    EngineOutputCouple(),
                    ChecksAsync(),
                    ControlsAsync()
                };

                await Task.WhenAll(cycles).ConfigureAwait(false);

                return new Result();
            }
            catch (Exception)
            { return new Result(); }
        }
    }
}
