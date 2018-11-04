using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Sample.ExceptionHandlers
{
    public class IndexOutOfRangeExceptionHandler : IHandler<IndexOutOfRangeException>
    {
        public Task<Response> HandleAsync(HttpContext context, IndexOutOfRangeException exception)
        {
            return Task.FromResult(new Response(HttpStatusCode.AlreadyReported, $"{context.Request.Path} failed with {exception.GetType().FullName}. Catched by {nameof(IndexOutOfRangeExceptionHandler)}"));
        }
    }
}