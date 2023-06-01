using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Contracts
{
    public interface IInternalInitializable
    {
        protected bool IsInitialized { get; }
        protected void InitiailizeCore();
        protected void EnsureInitialized();

    }
}
