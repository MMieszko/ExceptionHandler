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
        public static IApplicationBuilder RegisterExceptionHandler(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<Middleware>();

            return @this;
        }

        public static IBuilder<TException> Catch<TException>(this IApplicationBuilder @this)
            where TException : Exception
        {
            return new Builder<TException>(@this);
        }
    }
}