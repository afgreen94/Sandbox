using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public class Logger
    {

    }

    public interface ILogger { }
    public interface ILoggingTarget { }

    public enum LogLevel
    {
        Application,
        Information,
        Warning,
        Error,
        Critical
    }

    public enum Verbosity
    {
        Application,
        Development,
        Debug,
        Full
    }
}
