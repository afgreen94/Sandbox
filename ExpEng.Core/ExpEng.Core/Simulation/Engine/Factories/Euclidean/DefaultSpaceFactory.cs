//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.Extensions.Configuration;

//using ExpEng.Core.Contracts.Simulation.Euclidean;
//using ExpEng.Core.Contracts.Paradigms;

//namespace ExpEng.Core.Simulation.Engine.Euclidean.Factories
//{

//    //..AG.. Naming !!! 
//    public class DefaultSpaceFactory : SpaceFactory, IDefaultSpaceFactory
//    {

//        private readonly IConfiguration configuration;

//        public DefaultSpaceFactory(IConfiguration configuration)
//        {
//            this.configuration = configuration;
//        }

//        public override ISpace CreateSpaceForm(Args args) => throw new NotImplementedException();
//    }

//    public interface IDefaultSpaceFactory : ISpaceFactory<DefaultSpaceFactory.Args> { }
//}
