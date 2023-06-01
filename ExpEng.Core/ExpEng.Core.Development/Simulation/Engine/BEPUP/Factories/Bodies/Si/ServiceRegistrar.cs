using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpEng.Core.Contracts.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpEng.Core.Development.Simulation.Engine.BEPUP.Factories.Bodies
{
    public class ServiceRegistrar : IServiceRegistrar
    {
        public void RegisterServices(IServiceCollection sc, IConfiguration configuration)
        {
            sc.AddScoped<ISiD_RigidPoseFactory, SiD_RigidPoseFactory>();
            sc.AddScoped<ISiD_CollidableDescriptionFactory, SiD_CollidableDescriptionFactory>();
            sc.AddScoped<ISiD_BodyActivityDescriptionFactory, SiD_BodyActivityFactory>();

            sc.AddScoped<ISiD_BodyDescriptionFactory_Navtive, SiD_BodyDescriptionFactory_Navtive>();
        }
    }
}
