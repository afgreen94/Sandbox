using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Contracts.Base
{
    public interface IServiceRegistrar
    {
        void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration);

    }

    public interface IServiceCollectionContainer
    {

    }
}
