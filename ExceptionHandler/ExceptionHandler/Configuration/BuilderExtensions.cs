using System;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Builder;

namespace ExceptionHandler.Configuration
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<Middleware>();

            return @this;
        }

        public static IApplicationBuilder UseExceptionMiddleware<TMiddleware>(this IApplicationBuilder @this)
            where TMiddleware : Middleware
        {
            @this.UseMiddleware<TMiddleware>();

            return @this;
        }

        public static IBuilder<TException> Catch<TException>(this IApplicationBuilder @this)
            where TException : Exception
        {
            return new Builder<TException>(@this);
        }

        public static IBuilder<Exception> CatchDefault(this IApplicationBuilder @this)
        {
            return new Builder<Exception>(@this);
        }
    }
}