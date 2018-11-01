using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using ExceptionHandler.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public class Tests
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler().Catch<BadImageFormatException>().AndReturnAsync(exception => Task.FromResult(new Response(HttpStatusCode.Accepted, "Omg")))
                                     .Catch<InvalidCastException>().AndCall(() => new InvalidCastExceptionHandler())
                                     .Catch<IndexOutOfRangeException>().AndWriteResponseAsync(HttpStatusCode.Accepted, Response.Empty(HttpStatusCode.Accepted))
                                     .ForUnspecifiedUse<DefaultExceptionHandler>();


            app.UseExceptionHandler().CatchAny().AndWriteResponse(Response.Empty(HttpStatusCode.Accepted));
            app.UseExceptionHandler().CatchAny().AndWriteResponse(new Response(HttpStatusCode.OK, "Its always ok :)"));
            app.UseExceptionHandler().CatchAny().AndWriteResponse(exception => new Response(HttpStatusCode.Conflict, $"failed with {exception.Message}"));
            app.UseExceptionHandler().CatchAny().AndWriteResponse((ctx, exception) => new Response(HttpStatusCode.Conflict, $"{ctx.Request.Path} failed with {exception.Message}"));
        }
    }

    public class InvalidCastExceptionHandler : IHandler<InvalidCastException>
    {
        public async Task<string> HandleAsync(HttpContext context, InvalidCastException exception)
        {
            var requestBody = await context.ReadRequestBodyAsync();

            return $"{context.Request.Path} failed with {exception.GetType().FullName} while sening {requestBody}";
        }
    }

    public class DefaultExceptionHandler : IHandler<Exception>
    {
        public async Task<string> HandleAsync(HttpContext context, Exception exception)
        {
            var requestBody = await context.ReadRequestBodyAsync();

            return $"{context.Request.Path} failed with {exception.GetType().FullName} while sening {requestBody}";
        }
    }
}