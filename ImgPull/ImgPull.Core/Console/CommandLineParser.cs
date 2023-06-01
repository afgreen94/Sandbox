using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core.Console
{
    public abstract class CommandLineParserBase
    {
        protected CommandLineArgsValidatorBase _commandLineArgsValidator;

        private const string OutputDirectoryFlagToken0 = "-OD";
        private const string OutputDirectoryFlagToken1 = "-od";
        private const string OutputDirectoryFlagToken2 = "-OutputDirectory";

        private const string VerbosityFlagToken0 = "-V";
        private const string VerbosityFlagToken1 = "-v";
        private const string VerbosityFlagToken2 = "-Verbose";

        protected const string InvalidTokenException = "Invalid Command Line Flag: {0}";
        protected const string MissingArgumentException = "Missing Argument for Flag: {0}";
        protected const string InvalidArgumentException = "Invalid Argument for Flag: {0}, Value: {1}";

        protected virtual CommandLineArgsBase commandLineArgs => new CommandLineArgs();
        protected virtual CommandLineArgsValidatorBase commandLineArgsValidator
        {
            get
            {
                if (this._commandLineArgsValidator == default)
                    this._commandLineArgsValidator = this.GetCommandLineArgsValidatorImplementation();

                return this._commandLineArgsValidator;
            }
        }

        public virtual CommandLineArgsBase ParseValidate(string[] args) 
        {
            var ret = this.GetCommandLineArgsImplementation();
            var length = args.Length;

            for(int i = 0; i < args.Length; i++)
            {
                switch(args[i])
                {
                    case OutputDirectoryFlagToken0:
                    case OutputDirectoryFlagToken1:
                    case OutputDirectoryFlagToken2:
                        this.ValidateArgumentPresent(args[i], i, length);
                        //Validate Argument Value 
                        ret.Output.OutputDirectoryRoot = args[++i];
                        break;
                    case VerbosityFlagToken0:
                    case VerbosityFlagToken1:
                    case VerbosityFlagToken2:
                        this.ValidateArgumentPresent(args[i], i, length);
                        if (!bool.TryParse(args[++i], out var verbose))
                            throw new Exception(string.Format(InvalidArgumentException, args[i - i], args[i]));
                        ret.Verbose = verbose;
                        break;
                    default:
                        if (!this.TryParseCore(args, ref i, length, ref ret))
                            throw new Exception(string.Format(InvalidTokenException, args[i]));
                        else
                            break;
                }
            }

            this.ValidateCommandLineArgs(ret);

            return ret;
        }

        protected void ValidateArgumentPresent(string currArg, int currIdx, int length)
        {
            if (currIdx + 1 >= length)
                throw new Exception(string.Format(MissingArgumentException, currArg));
        }

        protected void ValidateCommandLineArgs(CommandLineArgsBase commandLineArgs)
        {
            if (!this.commandLineArgsValidator.Validate(commandLineArgs, out var errorText))
                throw new Exception(errorText);
        }

        protected virtual CommandLineArgsBase GetCommandLineArgsImplementation() => new CommandLineArgs();
        protected virtual CommandLineArgsValidatorBase GetCommandLineArgsValidatorImplementation() => new CommandLineArgsValidator();

        protected virtual bool TryParseCore(string[] args, ref int currIdx, int length, ref CommandLineArgsBase commandLineArgs) => false;
    }

    public abstract class CommandLineArgsValidatorBase
    {
        protected const string MissingOutputDirectoryArgument = "Missing OutputDirectory Argument";

        public virtual bool Validate(CommandLineArgsBase args, out string errorText)    //-AG- chain messages 
        {
            //-AG- TODO

            if(string.IsNullOrEmpty(args.Output.OutputDirectoryRoot))
            {
                errorText = MissingOutputDirectoryArgument;
                return false;
            }

            return this.ValidateCore(args, out errorText);
        }

        protected virtual bool ValidateCore(CommandLineArgsBase args, out string errorText)
        {
            errorText = string.Empty;
            return true;
        }
    }

    public class CommandLineArgsBase
    {
        public string RootUrlPrefix { get; set; }
        public bool Verbose { get; set; } = false;
        public RecursionArgs Recursion { get; set; } = new();
        public SupportedFileTypesArgs SupportedFileTypes { get; set; } = new();
        public OutputArgs Output { get; set; } = new();
        public ErrorHandlingArgs ErrorHandling { get; set; } = new();

        public class RecursionArgs
        {

        }
        public class SupportedFileTypesArgs
        {

        }

        public class OutputArgs
        {
            public string OutputDirectoryRoot { get; set; }
        }

        public class ErrorHandlingArgs
        {
            public bool ThrowOnTransferFailure { get; set; } = false;
        }
    }

    public class CommandLineParser : CommandLineParserBase { }
    public class CommandLineArgsValidator : CommandLineArgsValidatorBase { }
    public class CommandLineArgs : CommandLineArgsBase { }
    

    public class FourChanCommandLineParser : CommandLineParserBase
    {
        private const string BoardFlag0 = "-B";
        private const string BoardFlag1 = "-b";
        private const string BoardFlag2 = "-Board";

        private const string FilterFlag0 = "-f";
        private const string FilterFlag1 = "-F";
        private const string FilterFlag2 = "Filter";

        protected override bool TryParseCore(string[] args, ref int currIdx, int length, ref CommandLineArgsBase commandLineArgs)
        {  

            switch(args[currIdx])
            {
                case BoardFlag0:
                case BoardFlag1:
                case BoardFlag2:
                    this.ValidateArgumentPresent(args[currIdx], currIdx, length);
                    ((FourChanCommandLineArgs)commandLineArgs).Board = args[++currIdx];
                    return true;
                case FilterFlag0:
                case FilterFlag1:
                case FilterFlag2:
                    this.ValidateArgumentPresent(args[currIdx], currIdx, length);
                    ((FourChanCommandLineArgs)commandLineArgs).Expressions.Add(args[++currIdx]);
                    return true;
                default:
                    return false;
            }
        }

        protected override CommandLineArgsBase GetCommandLineArgsImplementation() => new FourChanCommandLineArgs();
        protected override CommandLineArgsValidatorBase GetCommandLineArgsValidatorImplementation() => new FourChanCommandLineArgsValidator();
    }
    public class FourChanCommandLineArgsValidator : CommandLineArgsValidatorBase
    {
        private const string MissingBoardArgumentException = "Missing Board Argument";

        protected override bool ValidateCore(CommandLineArgsBase args, out string errorText)
        {
            errorText = string.Empty;

            if(((FourChanCommandLineArgs)args).Board == default)
                    errorText = MissingBoardArgumentException;

            return string.IsNullOrEmpty(errorText);
        }
    }

    public class FourChanCommandLineArgs : CommandLineArgsBase
    {
        public HashSet<string> Expressions { get; set; } = new HashSet<string>();
        public string Board { get; set; }
    }
}
