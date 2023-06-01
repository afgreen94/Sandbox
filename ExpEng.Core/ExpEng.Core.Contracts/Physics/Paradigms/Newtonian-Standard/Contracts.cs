using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpEng.Core.Contracts.Physics.Paradigms;

namespace ExpEng.Core.Contracts.Paradigms
{

    //Unifier abstr ?? ..AG..   
    public interface IReal { }

    public interface IRealOrdered : IOrdered { }
    public interface IRealOrdered<T>: IRealOrdered { }
    //public interface IRealChaotic : IChaotic { } //..AG.. hmmm ??? maybe ignore for Newtonian, at least Standard 

    
    //Abstract Fundamentals, notate in convention, etc. ? ..AG.. 
    public interface IMassive : IRealOrdered
    {
    }

    public interface IForceful : IRealOrdered 
    {
    }

    public interface IMassive<I>
    {
    }

    public interface IMassOrder { }


    //Concrete Fundamentals, ..like above ..AG.. 
    public interface IParticle : IMassive { }
    public interface IWave : IForceful { }

    public interface ISpace : ISpaceForm { }
    public interface ITime : ITimeForm { }

    public interface ISpaceFormFactory { }
    public interface ISpaceFormFactory<TSpaceForm, TCreateArgs> : ISpaceFormFactory
        where TSpaceForm : ISpaceForm
    {
        public TSpaceForm CreateSpaceForm(TCreateArgs args);
    }

    //??
    public interface IInteraction { } //force ? 


    public interface IAtomicParticle : IParticle { }

    public interface INeutron : IAtomicParticle { }
    public interface IProton : IAtomicParticle { }
    public interface IElectron : IAtomicParticle { }



}
