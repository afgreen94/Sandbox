using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Development.Drivers.BEPUP
{
    public interface IDiskDescription
    {
        string Name { get; }
        string Description { get; }
        Guid Id { get; }
    }

    public abstract class DiskDescription : IDiskDescription
    {
        public abstract string Name { get; }
        public abstract Guid Id { get; }
        public virtual string Description => string.Empty;
    }

    public class DiskEqualityComparer : IEqualityComparer<Disk>
    {
        public bool Equals(Disk? x, Disk? y) => x != null && y != null && x.Description.Id == y.Description.Id;

        public int GetHashCode([DisallowNull] Disk obj) => obj.GetHashCode();
    }
}
