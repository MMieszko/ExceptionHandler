using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Abstractions
{
    public interface IHandler<in TException>
    {
        /// <summary>
        /// Handles given exception and returns instance of <see cref="Response"/>
        /// </summary>
        /// <param name="context">Current HttpContext</param>
        /// <param name="exception">Exception which occurred</param>
        /// <param name="serviceProvider">ASP.NET Core service provider to resolve any service registered in DI</param>
        Task<Response> HandleAsync(HttpContext context, TException exception, IServiceProvider serviceProvider);
    }
}