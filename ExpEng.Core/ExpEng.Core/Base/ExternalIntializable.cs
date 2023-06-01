using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpEng.Core.Contracts.Base;

namespace ExpEng.Core.Base
{
    public abstract class ExternalInitializable : Initializable, IInitializable
    {
        public bool IsInitialized => this.IsInitialized;

        public new void Initialize() => base.Initialize();
    }
}
