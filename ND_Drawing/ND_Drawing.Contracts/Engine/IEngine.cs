using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abstractions;
using Abstractions.Contracts;
using Base;
using Base.Contracts;
using ND_Drawing.Contracts.Engine.Signals;
using Signals.Contracts;

namespace ND_Drawing.Contracts.Engine
{
    public interface IEngine : IAsyncStartupInitializable, IEngineManager
    {

    }

    public interface IEngineHarness : IEngine { }
}
