using System;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Builder;

namespace ExceptionHandler.Configuration
{
    public static class BuilderExtensions
    {
        /// <summary>
        /// Adds <see cref="Middleware"/> into pipeline.
        /// </summary>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<Middleware>();

            return @this;
        }
        
        /// <summary>
        /// Adds custom middleware which inherit from <see cref="Middleware"/> into pipeline
        /// </summary>
        /// <typeparam name="TMiddleware">Type of class which inherit from <see cref="Middleware"/></typeparam>
        public static IApplicationBuilder UseExceptionMiddleware<TMiddleware>(this IApplicationBuilder @this)
            where TMiddleware : Middleware
        {
            @this.UseMiddleware<TMiddleware>();

            return @this;
        }

        /// <summary>
        /// Get the <see cref="IBuilder{TException}"/> instance to build handling of given exception type.
        /// </summary>
        /// <typeparam name="TException">Type of exception to handle</typeparam>
        /// <returns>Instance of <see cref="IBuilder{TException}"/></returns>
        public static IBuilder<TException> Catch<TException>(this IApplicationBuilder @this)
            where TException : Exception
        {
            return new Builder<TException>(@this);
        }

        /// <summary>
        /// Get the instance of <see cref="IBuilder{TException}"/> where TException is <see cref="Exception"/> to build handling of all unhandled exception types.
        /// </summary>
        /// <returns>Instance of <see cref="IBuilder{TException}"/></returns>
        public static IBuilder<Exception> CatchDefault(this IApplicationBuilder @this)
        {
            return new Builder<Exception>(@this);
        }
    }
}