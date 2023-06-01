using ExpEng.Core.Contracts.Paradigms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Physics
{
    public abstract class FundamentalInteraction : Interaction { }

    public class Gravity : FundamentalInteraction {  }

    public class ElectroMagnetism : FundamentalInteraction { }

    public class WeakNuclear : FundamentalInteraction { }

    public class StrongNuclear : FundamentalInteraction { }
}
