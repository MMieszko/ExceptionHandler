using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Http;
using Sample.Services;

namespace Sample.ExceptionHandlers
{
    public class IndexOutOfRangeExceptionHandler : IHandler<IndexOutOfRangeException>
    {
        public async Task<Response> HandleAsync(HttpContext context, IndexOutOfRangeException exception, IServiceProvider serviceProvider)
        {
            var message = $"{context.Request.Path} failed with {exception.GetType().FullName}. Catch by {nameof(IndexOutOfRangeExceptionHandler)}";

            await ((LoggerService) serviceProvider.GetService(typeof(LoggerService))).LogAsync(message);

            return new Response(HttpStatusCode.AlreadyReported, message);
        }
    }
}