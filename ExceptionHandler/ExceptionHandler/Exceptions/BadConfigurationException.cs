using System;

namespace ExceptionHandler.Exceptions
{
    public class BadConfigurationException : Exception
    {
        public BadConfigurationException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}