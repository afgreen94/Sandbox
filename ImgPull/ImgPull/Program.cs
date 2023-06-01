﻿using System;
using System.Threading.Tasks;
using ImgPull.Core.Console;

namespace ImgPull
{
    public class Program
    {
        static async Task Main(string[] args) => await new Driver().DriveAsync(args).ConfigureAwait(false);
    }
}


