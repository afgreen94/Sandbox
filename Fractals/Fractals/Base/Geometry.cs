using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.Base
{
    public class Point1D
    {
        public float X { get; }
    }


    public class Point2D : Point1D { public double Y { get; } }
    public class Point3D : Point2D { public double Z { get; } }



    public class Surface
    {

    }

    public class OneSurface : Surface
    {

    }

    public class TwoSurface : Surface
    {

    }

    public class ThreeSurface : Surface
    {

    }

    public class FourSurface : Surface
    {

    }

    public class NSurface : Surface
    {

    }

    public class TwoNSurface : NSurface
    {

    }

    public class ThreeNSurface : NSurface
    {

    }

    public class FourNSurface : NSurface
    {

    }
}
