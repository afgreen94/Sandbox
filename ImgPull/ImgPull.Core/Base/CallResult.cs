using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public class CallResult : ICallResult
    {
        private string errorText;

        public CallResult() : this(string.Empty) { }
        public CallResult(string errorText) { this.errorText = errorText; }
        public string ErrorText
        {
            get { return this.errorText; }
            set { this.errorText = value; }
        }
        public bool Success => string.IsNullOrEmpty(this.errorText);
    }
    public class CallResult<T> : CallResult, ICallResult<T>
    {
        private readonly T result;
        public CallResult(T result) { this.result = result; }
        public T Result => this.result;
    }
}