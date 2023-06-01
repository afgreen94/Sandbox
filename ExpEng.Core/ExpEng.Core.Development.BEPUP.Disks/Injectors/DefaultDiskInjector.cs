using ExpEng.Core.Development.Drivers.BEPUP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public unsafe class DefaultDiskInjector : DiskInjector
    {
        protected override DiskCartridge[] Cartridges => new DiskCartridge[]
        {
            new DefaultDiskCartridge(),
            //new ReflectionDiskCartridge()
        };
    }
}
