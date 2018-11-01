using System;
using System.Threading.Tasks;

namespace ExceptionHandler.Abstractions
{
    public interface IExceptionHandler<in TException>
        where TException : Exception
    {
        Task<string> HandleAsync(TException exception);
    }
}