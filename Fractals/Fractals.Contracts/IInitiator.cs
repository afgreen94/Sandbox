﻿using AFrame.Base.Contracts.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFrame.Base.Contracts.Abstractions;
using AFrame.Base.Contracts.Math;
using AFrame.Base.Contracts.Math.Graphing;
using AFrame.Base.Contracts.Math.Forms;

namespace Fractals.Contracts
{

    public interface IInitiator : IDescribableGraph<IInitiatorDescription>
    {

    }
    public interface IInitiatorDescription : IGraphDescription
    {

    }
}
