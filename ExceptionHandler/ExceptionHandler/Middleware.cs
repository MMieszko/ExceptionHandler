using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public class Middleware
    {
        protected HttpContext HttpContext;
        protected readonly RequestDelegate Next;
        protected IServiceProvider ServiceProvider;

        public Middleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                this.ServiceProvider = serviceProvider;
                this.HttpContext = context;
                await Next(context);
            }
            catch (Exception ex)
            {
                await this.WriteResponseAsync(await CreateResponseAsync(ex));
            }
        }

        protected virtual async Task WriteResponseAsync(Response response)
        {
            HttpContext.Response.StatusCode = (int)response.StatusCode;
            await HttpContext.Response.WriteAsync(response.Message);
        }

        protected virtual async Task<Response> CreateResponseAsync(Exception ex)
        {
            return await Container.GetResponseAsync(HttpContext, (dynamic)ex, ServiceProvider);
        }
    }
}
