using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Abstractions
{
    public interface IBuilder<out TException>
        where TException : Exception
    {
        IApplicationBuilder AndReturnAsync(Func<TException, Task<Response>> responseWriter);
        IApplicationBuilder AndReturnAsync(Func<HttpContext, TException, Task<Response>> responseWriter);

        IApplicationBuilder AndCall(Func<IHandler<TException>> handlerCreator);
        IApplicationBuilder AndCall<THandler>() where THandler : IHandler<TException>;
        IApplicationBuilder AndCall(IHandler<TException> handler);

        IApplicationBuilder AndWriteResponseAsync(HttpStatusCode statusCode);
        IApplicationBuilder AndWriteResponseAsync(HttpStatusCode statusCode, Response message);
    }
}