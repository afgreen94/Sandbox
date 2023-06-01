﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpEng.Core.Drivers
{
    public interface IDriver
    {
        ValueTask DriveAsync(string[] args);
    }
}
