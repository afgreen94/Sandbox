using ExpEng.Core.TestConsole.BEPUP;

namespace ExpEng.Core.TestConsole
{
    public class Program { public static async Task Main(string[] args) => await new BD0().DriveAsync(args).ConfigureAwait(false); }
}