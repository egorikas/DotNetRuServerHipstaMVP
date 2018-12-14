using System;

namespace DotNetRuServerHipstaMVP.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public string[] Exceptions { get; }

        public ValidationException(string message) : base(message)
        {
            Exceptions = new[] {message};
        }

        public ValidationException(string[] exceptions)
        {
            Exceptions = exceptions;
        }
    }

}