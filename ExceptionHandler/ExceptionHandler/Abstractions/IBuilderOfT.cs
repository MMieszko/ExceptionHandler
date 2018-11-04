using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Abstractions
{
    public interface IBuilder<out TException>
        where TException : Exception
    {
        IApplicationBuilder AndReturnAsync(Func<HttpContext, TException, IServiceProvider, Task<Response>> responseWriter);
        IApplicationBuilder AndReturnAsync(HttpStatusCode statusCode, string message);
        IApplicationBuilder AndReturnAsync(HttpStatusCode statusCode);

        IApplicationBuilder AndCall(Func<IHandler<TException>> handlerCreator);
        IApplicationBuilder AndCall<THandler>() where THandler : IHandler<TException>, new();
        IApplicationBuilder AndCall(IHandler<TException> handler);
    }
}