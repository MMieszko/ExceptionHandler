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
        /// <summary>
        /// Produces a response when <see cref="TException"/> occurs.
        /// </summary>
        /// <param name="responseWriter">Delegate with <see cref="HttpContext"/>, <see cref="TException"/>, <see cref="IServiceProvider"/> arguments and <see cref="Response"/> return type. </param>
        IApplicationBuilder AndReturnAsync(Func<HttpContext, TException, IServiceProvider, Task<Response>> responseWriter);

        /// <summary>
        /// Produces a static response when <see cref="TException"/> occurs. 
        /// </summary>
        /// <param name="statusCode">Status code of response</param>
        /// <param name="message">Message of response</param>
        IApplicationBuilder AndReturnAsync(HttpStatusCode statusCode, string message);

        /// <summary>
        /// Produces a static response with just <see cref="HttpStatusCode"/> when <see cref="TException"/> occurs. 
        /// </summary>
        /// <param name="statusCode">Status code of response</param>
        IApplicationBuilder AndReturnAsync(HttpStatusCode statusCode);

        /// <summary>
        /// Call <see cref="IHandler{TException}"/> when <see cref="TException"/> occurs.
        /// </summary>
        /// <param name="handlerCreator">Delegate to create instance of <see cref="IHandler{TException}"/></param>
        /// <returns></returns>
        IApplicationBuilder AndCall(Func<IHandler<TException>> handlerCreator);

        /// <summary>
        /// Call <see cref="IHandler{TException}"/> when <see cref="TException"/> occurs.
        /// The handler is created via reflection by given <see cref="THandler"/> type. Handler must have public and parameterless constructor.
        /// </summary>
        /// <returns></returns>
        IApplicationBuilder AndCall<THandler>() where THandler : IHandler<TException>, new();

        /// <summary>
        /// Call given <see cref="IHandler{TException}"/> when <see cref="TException"/> occurs.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IApplicationBuilder AndCall(IHandler<TException> handler);
    }
}