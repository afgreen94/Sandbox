using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Abstractions.Contracts;
using Base.Contracts;

namespace ND_Drawing.Contracts.Engine.Factories
{
    public interface IEngineFactory
    {
        public ValueTask<IEngineFactoryResult> CreateAsync(IConfiguration configuration);
    }

    public interface ICreateEngineResult : IResult { }
    public interface IEngineFactoryResult : ICreateEngineResult { }

    public interface IManagedEngineFactory : IEngineFactory { }
}
