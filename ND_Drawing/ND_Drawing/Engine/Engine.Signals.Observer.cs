using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ND_Drawing.Contracts.Engine;
using Signals;
using Signals.Contracts;

namespace ND_Drawing.Engine
{
    public abstract partial class Engine 
    {

        public abstract partial class Signals
        {

            public class EngineSSM_0  //-ag Engine Signal System Manager, Observer Pattern 
            {

            }
        }

    }


    public interface IEngineSignalSystemManager : ISignalSystemManager { }

    public class EngineComms : ISignal
    {

    }
}
