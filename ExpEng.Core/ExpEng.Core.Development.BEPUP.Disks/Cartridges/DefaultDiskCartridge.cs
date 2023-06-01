using ExpEng.Core.Development.Drivers.BEPUP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public unsafe class DefaultDiskCartridge : DiskCartridge
    {
        private readonly Disk[] _disks = new Disk[] 
        {
            new SimpleElectroMagnetism() 
        };
        protected override Disk[] disks => _disks;
    }
}
