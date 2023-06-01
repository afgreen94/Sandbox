using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{

    public class WindowsFilenameValidator : IWindowsFilenameValidator
    {

        private static readonly HashSet<char> IllegalChars = //-AG- May be incomplete
        new()
        {
            '#',
            '<',
            '%',
            '>',
            '&',
            '*',
            '{',
            '}',
            '?',
            '/',
            '\\',
            '\t',
            '\n',
            '\r',
            ' ',
            '\b',
            '$',
            '+',
            '!',
            '\'',
            '\"',
            '=',
            ':',
            '@',
            '|',
            '`'
        };

        public bool ValidateFilenameGetIllegalCharIndices(string filename, out int[] illegalCharIndices)
        {
            var chars = filename.ToCharArray();
            var illegalCharIndicesList = new List<int>();

            for (int i = 0; i < chars.Length; i++)
                if (IllegalChars.Contains(chars[i]))
                    illegalCharIndicesList.Add(i);

            illegalCharIndices = illegalCharIndicesList.ToArray();

            return illegalCharIndices.Length != 0;
        }
    }

    public class WindowsFilenameCorrrection : IFilenameCorrection
    {
        private const char DefaultReplacementChar = '_';

        private readonly IWindowsFilenameValidator validator = new WindowsFilenameValidator();
        private readonly char replacementChar = DefaultReplacementChar;
        private readonly IDictionary<char, char> replacementCharMap;

        public WindowsFilenameCorrrection() { }
        public WindowsFilenameCorrrection(Options options) 
        {
            if (options.Validator != default)
                this.validator = options.Validator;

            if (options.ReplacementChar != default)
                this.replacementChar = options.ReplacementChar;

            if (options.ReplacementCharMap != default)
                this.replacementCharMap = options.ReplacementCharMap;
        }

        public bool TryCorrectFilename(ref string filename)
        {
            if (!this.validator.ValidateFilenameGetIllegalCharIndices(filename, out var illegalCharIndices))
                return false;

            Func<char, char> replacementFunc = (this.replacementCharMap != default) ? this.GetReplacementUsingReplacementCharMap : this.GetReplacementUsingReplacementChar;

            var chars = filename.ToCharArray();

            foreach (var icIndex in illegalCharIndices)
                chars[icIndex] = replacementFunc(chars[icIndex]);

            filename = new string(chars);

            return true;
        }

        private char GetReplacementUsingReplacementChar(char c) => this.replacementChar;
        private char GetReplacementUsingReplacementCharMap(char c) => this.replacementCharMap[c];

        public class Options
        {
            public IWindowsFilenameValidator Validator { get; set; }
            public char ReplacementChar { get; set; }
            public IDictionary<char, char> ReplacementCharMap { get; set; }
        }
    }

    public interface IFilenameValidator //-AG- weak naming 
    {
        public bool ValidateFilenameGetIllegalCharIndices(string filename, out int[] illegalCharIndices);
    }

    public interface IWindowsFilenameValidator : IFilenameValidator { }

    public interface IFilenameCorrection  //-AG- weak naming 
    {
        public bool TryCorrectFilename(ref string filename);
    }


}
