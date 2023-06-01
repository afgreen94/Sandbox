using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ExpEng.Core.Development.Drivers.BEPUP; 
using ExpEng.Core.Development.Drivers.BEPUP.Scenarios;

using ExpEng.Core.TestFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Sdk;

namespace ExpEng.Core.Test
{


    public class DiskTests : TestFramework.TestClass
    {



        [Fact]
        public async Task Disk0Test()
        {
            using var sw = new StreamWriter("C:\\Users\\Alex\\source\\repos\\ExpEng.Core\\FreeGuid.txt");

            for (int i = 0; i < 1000; i++)
                await sw.WriteLineAsync(Guid.NewGuid().ToString()).ConfigureAwait(false);
        }













        protected override IConfiguration BuildConfiguration() => BuildDefaultConfiguration();

        protected override void RegisterServicesCore(IServiceCollection sc)
        {
            throw new NotImplementedException();
        }
    }
}
