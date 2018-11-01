using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public class ExceptionHandler : ExceptionHandlerBase
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
            : base(next)
        {
            _next = next;
        }

        public virtual async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch
            {

            }
        }

        protected override Task<string> WriteResponseAsync(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
