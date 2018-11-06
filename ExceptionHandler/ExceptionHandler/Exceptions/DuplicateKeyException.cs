using System;

namespace ExceptionHandler.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message)
            : base(message)
        {

        }
    }
}