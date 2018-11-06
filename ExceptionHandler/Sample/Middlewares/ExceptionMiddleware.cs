using System;
using System.Threading.Tasks;
using ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Sample.Services;

namespace Sample.Middlewares
{
    public class ExceptionMiddleware : Middleware
    {
        public ExceptionMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        protected override async Task WriteResponseAsync(Response response)
        {
            var logger = (LoggerService) base.ServiceProvider.GetService(typeof(LoggerService));

            await logger.LogAsync(response.Message);

            await base.WriteResponseAsync(response);
        }
    }
}