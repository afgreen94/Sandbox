using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Paradigms;
using ExpEng.Core.Contracts.Physics.Paradigms;

namespace ExpEng.Core.Contracts.Simulation.Euclidean
{
    public interface ISpaceFactory<TCreateArgs> : ISpaceFormFactory<ISpace, TCreateArgs>
    {
    }
}
