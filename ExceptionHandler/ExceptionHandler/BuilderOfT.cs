using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public sealed class Builder<TException> : IBuilder<TException>
       where TException : Exception
    {
        private readonly IApplicationBuilder _applicationBuilder;

        public Builder(IApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
        }

        public IApplicationBuilder AndReturnAsync(Func<HttpContext, TException, Task<Response>> responseWriter)
        {
            Container.Add<TException>(responseWriter);

            return _applicationBuilder;
        }

        public IApplicationBuilder AndReturnAsync(HttpStatusCode statusCode)
        {
            Container.Add<TException>((Func<Response>)ResponseCreator);

            return _applicationBuilder;

            Response ResponseCreator() => new Response(statusCode, string.Empty);
        }

        public IApplicationBuilder AndReturnAsync(HttpStatusCode statusCode, string message)
        {
            Container.Add<TException>((Func<Response>)ResponseCreator);

            return _applicationBuilder;

            Response ResponseCreator() => new Response(statusCode, message);
        }

        public IApplicationBuilder AndCall(Func<IHandler<TException>> handlerCreator)
        {
            Container.Add<TException>(handlerCreator);

            return _applicationBuilder;
        }

        public IApplicationBuilder AndCall<THandler>()
            where THandler : IHandler<TException>, new()
        {
            Container.Add<TException>((Func<IHandler<TException>>)HandlerCreator);

            return _applicationBuilder;

            IHandler<TException> HandlerCreator() => Activator.CreateInstance<THandler>();

        }

        public IApplicationBuilder AndCall(IHandler<TException> handler)
        {
            Container.Add<TException>((Func<IHandler<TException>>)HandlerCreator);

            return _applicationBuilder;

            IHandler<TException> HandlerCreator() => handler;
        }
    }
}