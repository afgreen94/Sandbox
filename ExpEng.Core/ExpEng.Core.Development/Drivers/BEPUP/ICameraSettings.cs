using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ExpEng.Core.Development.Drivers.BEPUP
{
    public unsafe interface ICameraSettings
    {
        Vector3 Position { get; }
        float Yaw { get; }
        float Pitch { get; }
    }

    public unsafe class CameraSettings : ICameraSettings
    {
        public Vector3 Position { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
    }
}
