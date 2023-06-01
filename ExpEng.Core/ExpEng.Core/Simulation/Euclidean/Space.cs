using ExpEng.Core.Contracts.Paradigms;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Simulation.Engine.Euclidean
{

    public class Space : ISpace
    {
        private readonly IConfiguration configuration;

        public Space() { }
        public Space(IConfiguration configuration) { this.configuration = configuration; }





    }
}
