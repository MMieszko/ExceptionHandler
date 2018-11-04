using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using ExceptionHandler.Configuration;
using ExceptionHandler.Tests.GetResponseTests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ExceptionHandler.Tests
{
    public class GetResponseViaHandlerTests : GetResponseTest
    {
        [Fact]
        public async Task CanGetResponseViaHandlerCreatedByReflection()
        {
            ApplicationBuilder.UseExceptionMiddleware().Catch<InvalidCastException>().AndCall<InvalidCastExceptionHandler>();

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new InvalidCastException(), null);

            result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task GetReseponseViaHandlerCreatedByDelegate()
        {
            ApplicationBuilder.UseExceptionMiddleware().Catch<IndexOutOfRangeException>().AndCall(() => new IndexOutOfRangeExceptionHandler());

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new IndexOutOfRangeException(), null);

            result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Ambiguous);
        }

        [Fact]
        public async Task GetReseponseViaHandlerCreadtedManually()
        {
            const string message = "Test";

            ApplicationBuilder.UseExceptionMiddleware().Catch<DivideByZeroException>().AndCall(new DivideByZeroExceptionHandler());

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new DivideByZeroException(message), null);

            result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.AlreadyReported);
            result.Message.Should().Be(message);
        }

        private class InvalidCastExceptionHandler : IHandler<InvalidCastException>
        {
            public Task<Response> HandleAsync(HttpContext context, InvalidCastException exception, IServiceProvider serviceProvider)
            {
                return Task.FromResult(new Response(HttpStatusCode.Accepted, exception.Message));
            }
        }

        private class IndexOutOfRangeExceptionHandler : IHandler<IndexOutOfRangeException>
        {
            public Task<Response> HandleAsync(HttpContext context, IndexOutOfRangeException exception, IServiceProvider serviceProvider)
            {
                return Task.FromResult(new Response(HttpStatusCode.Ambiguous, exception.Message));
            }
        }

        private class DivideByZeroExceptionHandler : IHandler<DivideByZeroException>
        {
            public Task<Response> HandleAsync(HttpContext context, DivideByZeroException exception, IServiceProvider serviceProvider)
            {
                return Task.FromResult(new Response(HttpStatusCode.AlreadyReported, exception.Message));
            }
        }
    }
}
