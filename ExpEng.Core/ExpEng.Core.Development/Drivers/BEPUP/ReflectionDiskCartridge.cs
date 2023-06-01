using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExpEng.Core.Development.Drivers.BEPUP
{
    public class ReflectionDiskCartridge : DiskCartridge
    {

        private Disk[] _disks;

        protected override Disk[] disks
        {
            get
            {
                if (_disks == null)
                {
                    Load();
                    return disks;
                }
                else
                    return _disks;
            }
        }

        private const string DiskRegex = "*ExpEng.*.Disks.dll";

        private readonly string diskRegex = DiskRegex;

        public void Load()
        {
            var path = GetPath();
            var assembly = Assembly.LoadFrom(path);

            var disks = new List<Disk>();

            foreach (var t in assembly.GetTypes())
                if (!t.IsAbstract && t.IsAssignableFrom(typeof(Disk)))
                    CreateAddInstance(t, disks);

            _disks = disks.ToArray();
        }

        private void CreateAddInstance(Type t, IList<Disk> disks)
        {
            var instance = (Disk)Activator.CreateInstance(t);
            disks.Add(instance);
        }

        private string GetPath()
        {
            var wdPathParts = Assembly.GetExecutingAssembly().Location.Replace('\\', '/').Split('/');

            return string.Concat(string.Join('/', new ArraySegment<string>(wdPathParts, 0, wdPathParts.Length - 1)), diskRegex);
        }
    }
}
