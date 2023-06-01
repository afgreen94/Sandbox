using Abstractions.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ND_Drawing.Contracts.Engine;
using Signals.Contracts;

namespace ND_Drawing.Engine
{
    public abstract partial class Engine : IEngine //-ag game state machine, exposes in one way or another game state (need translation eventually), and manages updates to the state. Also has api for external signaling 
    {




        private readonly IContext<Engine> context;


        public Engine(IContext<Engine> context) { this.context = context; }




        public ValueTask<IResult> InitializeAsync(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public IResult Start()
        {
            throw new NotImplementedException();
        }




    }
}
