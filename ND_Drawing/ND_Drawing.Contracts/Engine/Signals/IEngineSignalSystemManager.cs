using Signals.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND_Drawing.Contracts.Engine.Signals
{
    public interface IEngineSignalSystemManager : ISignalSystemManager
    {
    }

    public interface IEngineSignalSystemManager_Observer : IEngineSignalSystemManager { }
}
