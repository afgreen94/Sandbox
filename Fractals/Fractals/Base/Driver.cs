using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.Base
{
    public abstract class Driver
    {
        public abstract ValueTask DriveAsync(string[] args);
    }
}
