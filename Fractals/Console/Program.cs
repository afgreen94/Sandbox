using Fractals.Drivers;
using System;
using System.Security.AccessControl;
using Cons = System.Console;


namespace Console
{
    public class Program { public static async Task Main(string[] args) => await new OpenTk_TestDriver().DriveAsync(args).ConfigureAwait(false); }
}