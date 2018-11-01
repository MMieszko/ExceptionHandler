using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Abstractions
{
    public interface IHandler<in TException> : IHandler
        where TException : Exception
    {
        Task<string> HandleAsync(HttpContext context, TException exception);
    }
}