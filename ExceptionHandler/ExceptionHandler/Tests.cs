using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using ExceptionHandler.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public class Tests
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler().Catch<Exception>().AndDo((exception) => Task.FromResult("Ship happens"))
                                     .Catch<InvalidCastException>().AndCall(() => new InvalidCastExceptionHandler())
                                     .Catch<DivideByZeroException>().AndCall<DivideByZeroExceptionHandler>()
                                     .Catch<IndexOutOfRangeException>().AndWriteResponse(HttpStatusCode.Accepted, string.Empty);

            app.UseExceptionHandler().CatchAny().AndWriteResponse(HttpStatusCode.Conflict);
            app.UseExceptionHandler().CatchAny().AndWriteResponse(HttpStatusCode.OK, "Its always ok :)");


            app.UseExceptionHandler<MyExceptionHandler>();

        }
    }

    public class MyExceptionHandler : ExceptionHandlerBase
    {
        public MyExceptionHandler(RequestDelegate next) 
            : base(next)
        {
        }
        
        protected override Task<string> WriteResponseAsync(Exception exception)
        {
            return Task.FromResult("Pizda nad glowa");
        }
    }

    public class InvalidCastExceptionHandler : IExceptionHandler<Exception>
    {
        public Task<string> HandleAsync(Exception exception)
        {
            throw new NotImplementedException();
        }
    }

    public class DivideByZeroExceptionHandler : IExceptionHandler<DivideByZeroException>
    {
        public Task<string> HandleAsync(DivideByZeroException exception)
        {
            throw new NotImplementedException();
        }
    }
}