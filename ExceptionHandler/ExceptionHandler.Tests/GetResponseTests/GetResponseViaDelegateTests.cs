using System;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using ExceptionHandler.Configuration;
using ExceptionHandler.Tests.GetResponseTests;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ExceptionHandler.Tests
{
    public class GetResponseViaDelegateTests : GetResponseTest
    {        
        [Fact]
        public async Task CanGetResponseViaHandlerCreatedByReflection()
        {
            const string message = nameof(CanGetResponseViaHandlerCreatedByReflection);
            const HttpStatusCode statusCode = HttpStatusCode.Ambiguous;

            ApplicationBuilder.UseExceptionMiddleware().Catch<InvalidOperationException>().AndReturnAsync((context, exception) => Task.FromResult(new Response(statusCode, message)));

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new InvalidOperationException(message), null);

            result.StatusCode.Should().BeEquivalentTo(statusCode);
            result.Message.Should().BeEquivalentTo(message);
        }
    }
}