using System;
using System.Threading.Tasks;

using ND_Drawing;

namespace ND_Drawing.Console
{
    public class Program
    {
        public static async Task Main(string[] args) => await new Driver().DriveAsync(args).ConfigureAwait(false);
    }
}