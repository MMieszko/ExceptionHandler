using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public class Handler
    {
        private readonly RequestDelegate _next;

        public Handler(RequestDelegate next)
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

        protected Task<string> WriteResponseAsync(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
