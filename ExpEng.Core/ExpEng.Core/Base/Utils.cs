using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Base
{
    //public class RandomEx : Random
    //{
    //    public float NextFloat(float lIncl = default, float uExcl = default)
    //    {

    //    }
    //}

    public class Utils
    {
        private static float GetRPrime(int atomicNumber) => 1.5f; //use dict 
        public static float GetNuclearRadius(int atomicNumber) => (float)(GetRPrime(atomicNumber) * System.Math.Cbrt(atomicNumber));
    }
}
