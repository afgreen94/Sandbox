using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using ExpEng.Core.Development;
using ExpEng.Core.Development.Drivers;
using ExpEng.Core.Development.Drivers.BEPUP;

namespace ExpEng.Core.Development.BEPUP.Disks
{
    public class ReflectionDiskCartridge : DiskCartridge
    {

        private Disk[] _disks;

        protected override Disk[] disks 
        {
            get
            {
                if (this._disks == null)
                {
                    this.Load();
                    return this.disks;
                }
                else
                    return this._disks;
            }
        }

        private const string DiskRegex = "*ExpEng.*.Disks.dll";

        private readonly string diskRegex = DiskRegex;

        public void Load()
        {
            var path = this.GetPath();
            var assembly = Assembly.LoadFrom(path);

            var disks = new List<Disk>();

            foreach(var t in assembly.GetTypes())
                if (!t.IsAbstract && t.IsAssignableFrom(typeof(Disk)))
                    this.CreateAddInstance(t, disks);

            this._disks = disks.ToArray();
        }

        private void CreateAddInstance(Type t, IList<Disk> disks)
        {
            var instance = (Disk)Activator.CreateInstance(t);
            disks.Add(instance);
        }

        private string GetPath()
        {
            var wdPathParts = Assembly.GetExecutingAssembly().Location.Replace('\\', '/').Split('/');

            return string.Concat(string.Join('/', new ArraySegment<string>(wdPathParts, 0, wdPathParts.Length - 1)), this.diskRegex);
        } 
    }
}
