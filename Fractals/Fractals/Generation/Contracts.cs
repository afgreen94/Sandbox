using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.Generation
{


    /// <summary>
    /// start with specific dim and generalize
    /// </summary>


    public interface IPoint { }

    public interface IPoint2D : IPoint { }
    public interface IPoint3D : IPoint { }


    public interface ICurve { }

    public interface ICurve2D : ICurve { }
    public interface ICurve3D : ICurve { }


    public interface IFractal : ICurve { }   

    public interface IFractal2D : IFractal, ICurve2D { }
    public interface IFractal3D : IFractal, ICurve { }



}
