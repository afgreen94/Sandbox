using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Drivers
{
    public class ConsoleDriver : Driver, IDriver
    {
        public override ValueTask DriveAsync(string[] args) => new();
    }
}
