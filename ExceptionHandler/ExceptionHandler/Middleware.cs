using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public class Middleware
    {
        protected HttpContext HttpContext;
        protected readonly RequestDelegate Next;

        public Middleware(RequestDelegate next)
        {
            Next = next;
        }

        public virtual async Task Invoke(HttpContext context)
        {
            try
            {
                this.HttpContext = context;
                await Next(context);
            }
            catch (Exception ex)
            {
                await this.WriteResponseAsync(await GetResponse(ex));
            }
        }

        protected virtual async Task WriteResponseAsync(Response response)
        {
            HttpContext.Response.StatusCode = (int)response.StatusCode;
            await HttpContext.Response.WriteAsync(response.Message);
        }

        protected virtual async Task<Response> GetResponse(Exception ex)
        {
            return await Container.GetResponseAsync(HttpContext, (dynamic)ex);
        }
    }
}
