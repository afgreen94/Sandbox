using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Base;

namespace ExpEng.Core.Base.Factories
{
    public abstract class Factory { }

    public abstract class InitializableFactory : ExternalInitializable, IInitializable
    {
    }
}
