using AFrame.Base.Contracts.Abstractions;
using AFrame.Base.Contracts.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.Contracts
{

    //how is it generated?
    //what is self-similarity ratio base?
    //hausdorf ???
    //topo
    //self-sim 
    // hausdorf >= topo ?

    //sure to be plenty of emergent stuff, really is meat so need to attend to -ag 

    public interface IFractalDefinition : IDescribable
    {

        public IInitiator Generator { get; }
        public IGenerator Initiator { get; }

        public double D { get; }
        public double Dt { get; }
        public double R { get; }
        public double B { get; }
        public bool Proper { get; }

    }
}
