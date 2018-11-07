using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ExceptionHandler.Tests.GetResponseTests
{
    public class GetResponseViaDelegateTests : GetResponseTest
    {        
        [Fact]
        public async Task CanGetResponseViaHandlerCreatedByReflection()
        {
            const string message = nameof(CanGetResponseViaHandlerCreatedByReflection);
            const HttpStatusCode statusCode = HttpStatusCode.Ambiguous;

            ApplicationBuilder.UseExceptionMiddleware().Catch<InvalidOperationException>().AndReturnAsync((context, exception, serviceProvider) => Task.FromResult(new Response(statusCode, message)));

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new InvalidOperationException(message), null);

            result.StatusCode.Should().BeEquivalentTo(statusCode);
            result.Message.Should().BeEquivalentTo(message);
        }
    }
}