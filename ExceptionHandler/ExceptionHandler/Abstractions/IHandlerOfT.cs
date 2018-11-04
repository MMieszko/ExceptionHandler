using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Abstractions
{
    public interface IHandler<in TException>
    {
        Task<Response> HandleAsync(HttpContext context, TException exception, IServiceProvider serviceProvider);
    }
}