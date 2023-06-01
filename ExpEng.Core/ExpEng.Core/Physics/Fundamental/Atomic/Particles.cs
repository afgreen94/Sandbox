using ExpEng.Core.Contracts.Paradigms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Physics.Fundamental.Atomic
{
    public abstract class Particle : Physics.Particle, IAtomicParticle
    {
    }

    public class Neutron : Physics.Particle, INeutron
    {
    }

    public class Proton : Physics.Particle, IProton
    {
    }

    public class Electron : Physics.Particle, IElectron
    {
    }
}
