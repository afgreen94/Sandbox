using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ExpEng.Core.Drivers
{
    public abstract class Driver : IDriver
    {
        public abstract ValueTask DriveAsync(string[] args);
    }
}