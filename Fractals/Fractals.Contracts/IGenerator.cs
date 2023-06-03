using AFrame.Base.Contracts.Abstractions;
using AFrame.Base.Contracts.Math;
using AFrame.Base.Contracts.Math.Graphing;
using AFrame.Base.Contracts.Math.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.Contracts
{ 

    public interface IGenerator : IDescribableGraph<IGeneratorDescription>
    {

    }

    public interface IGeneratorDescription : IGraphDescription
    {
        public int N { get; }
        public double B { get; }

        public double R { get; }
        public double D { get; }
    }
}
