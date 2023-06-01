using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Immutable;
using System.Diagnostics;

namespace ExpEng.Core.Low_Level
{

    //Memory Flex ? 
    public struct MemoryFlex<T>
    {
        private readonly Memory<T> flex;

        private readonly long capacity;
    }
}
