using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Base
{

    public abstract class Initializable 
    {
        protected bool IsInitialized { get; private set; } = false;

        protected virtual void Initialize()
        {
            this.InitializeCore();
            this.AfterInitializeCore();

            this.IsInitialized = true;
        }
        protected abstract void InitializeCore();

        protected virtual void AfterInitializeCore() { }

        protected void EnsureInitialized()
        {
            if (!this.IsInitialized)
                this.ErrorOnInitialized();
        }

        protected void EnsureNotInitialized()
        {
            if (this.IsInitialized)
                this.ErrorOnInitialized();
        }

        protected virtual void ErrorOnInitialized() => throw new Exception("..AG..");
        protected virtual void ErrorOnNotInitialized() => throw new Exception("..AG..");
    }

}
