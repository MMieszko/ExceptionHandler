using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Sample.ExceptionHandlers
{
    public class DefaultExceptionHandler : IHandler<Exception>
    {
        public Task<Response> HandleAsync(HttpContext context, Exception exception, IServiceProvider serviceProvider)
        {
            return Task.FromResult(new Response(HttpStatusCode.Conflict, exception.Message));
        }
    }
}