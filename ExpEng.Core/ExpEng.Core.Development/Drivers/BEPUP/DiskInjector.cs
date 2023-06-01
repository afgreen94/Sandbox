using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.Drivers.BEPUP
{
    public abstract class DiskInjector
    {
        protected abstract DiskCartridge[] Cartridges { get; }
        public virtual Disk[] Disks => Cartridges.SelectMany(c => c.Disks).Distinct(new DiskEqualityComparer()).ToArray();
    }

    public abstract class DiskCartridge
    {
        protected abstract Disk[] disks { get; }
        public Disk[] Disks => disks;
    }
}
