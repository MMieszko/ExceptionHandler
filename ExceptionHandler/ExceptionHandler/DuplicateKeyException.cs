using System;

namespace ExceptionHandler
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message)
            : base(message)
        {

        }
    }
}