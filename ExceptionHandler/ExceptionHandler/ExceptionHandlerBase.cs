using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public abstract class ExceptionHandlerBase
    {
        protected RequestDelegate Next;

        protected ExceptionHandlerBase(RequestDelegate next)
        {
            Next = next;
        }

        public virtual async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch(Exception ex)
            {
                await this.WriteResponseAsync(ex);
            }
        }

        protected abstract Task<string> WriteResponseAsync(Exception exception);
    }
}