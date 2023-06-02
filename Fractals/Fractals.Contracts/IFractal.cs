using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using AFrame.Base.Contracts.Math;

namespace Fractals.Contracts
{
    public interface IFractal : ICurve { }

    public interface IFractal2D : IFractal, ICurve2D { }
    public interface IFractal3D : IFractal, ICurve { }
}
