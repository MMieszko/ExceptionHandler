using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Builder;

namespace ExceptionHandler.Configuration
{
    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<ExceptionHandler>();


            return @this;
        }

        public static IApplicationBuilder UseExceptionHandler<TExceptionHandler>(this IApplicationBuilder @this)
            where TExceptionHandler : ExceptionHandlerBase
        {
            @this.UseMiddleware<TExceptionHandler>();


            return @this;
        }

        public static ISpecificExceptionCallback<TException> Catch<TException>(this IApplicationBuilder @this)
            where TException : Exception
        {
            return null;
        }

        public static IGeneralExceptionCallback CatchAny(this IApplicationBuilder @this)
        {
            return null;
        }

    }


    public interface ISpecificExceptionCallback<out TException>
        where TException : Exception
    {
        IApplicationBuilder AndDo(Func<TException, Task<string>> action);
        IApplicationBuilder AndCall(Func<IExceptionHandler<TException>> exceptionCreator);
        IApplicationBuilder AndCall<THandler>(IExceptionHandler<TException> exceptionHandler);
        IApplicationBuilder AndCall<THandler>();
        IApplicationBuilder AndCall(IExceptionHandler<TException> exceptionHandler);
        IApplicationBuilder AndWriteResponse(HttpStatusCode statusCode);
        IApplicationBuilder AndWriteResponse(HttpStatusCode statusCode, string message);
    }

    public interface IGeneralExceptionCallback
    {
        IApplicationBuilder AndWriteResponse(HttpStatusCode statusCode);
        IApplicationBuilder AndWriteResponse(HttpStatusCode statusCode, string message);
    }

    public interface IResponseWriter
    {
        Task<string> WriteAsync();
    }

}