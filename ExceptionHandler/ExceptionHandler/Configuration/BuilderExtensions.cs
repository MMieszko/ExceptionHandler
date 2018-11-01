using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace ExceptionHandler.Configuration
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<Handler>();

            return @this;
        }

        public static IBuilder<TException> Catch<TException>(this IApplicationBuilder @this)
            where TException : Exception
        {
            return null;
        }

        public static IBuilder CatchAny(this IApplicationBuilder @this)
        {
            return null;
        }

        public static IApplicationBuilder ForUnspecifiedUse<THandler>(this IApplicationBuilder @this)
            where THandler : IHandler
        {
            return null;
        }

        public static IApplicationBuilder ForUnspecifiedUse<THandler>(this IApplicationBuilder @this, THandler handler)
            where THandler : IHandler
        {
            return null;
        }

        public static IApplicationBuilder ForUnspecifiedUse<THandler>(this IApplicationBuilder @this, Func<THandler> handlerCreator)
            where THandler : IHandler
        {
            return null;
        }
    }
}