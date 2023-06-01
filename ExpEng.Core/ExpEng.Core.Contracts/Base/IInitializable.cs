using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Contracts.Base
{
    public interface IInitializable
    {
        public bool IsInitialized { get; }
        public void Initialize();
    }

    public interface IAsyncInitializable : IInitializable
    {
        public ValueTask InitializeAsync();
    }
}
