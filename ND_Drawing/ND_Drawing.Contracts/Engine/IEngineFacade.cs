using Base.Contracts;
using ND_Drawing.Contracts.Engine.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND_Drawing.Contracts.Engine
{
    public interface IEngineStartupFacade : IAsyncStartupInitializable
    {

    }
    public interface IEngineIOFacade : IEngineSignalManager
    {

    }

    public interface IEngineFacade : IEngineStartupFacade, IEngineIOFacade { }
}
