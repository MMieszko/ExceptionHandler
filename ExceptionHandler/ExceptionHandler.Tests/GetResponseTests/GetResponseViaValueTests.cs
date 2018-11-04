using System.IO;
using ExceptionHandler.Abstractions;
using ExceptionHandler.Configuration;
using ExceptionHandler.Tests.GetResponseTests;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ExceptionHandler.Tests
{
    public class GetResponseViaValueTests : GetResponseTest
    {
        [Fact]
        public async Task GetReponseWithStatusCodeTests()
        {
            const string message = nameof(GetReponseWithStatusCodeTests);
            const HttpStatusCode statusCode = HttpStatusCode.Conflict;

            ApplicationBuilder.RegisterExceptionHandler().Catch<FileNotFoundException>().AndReturnAsync(statusCode);

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new FileNotFoundException(message));

            result.StatusCode.Should().BeEquivalentTo(statusCode);
            result.Message.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task GetReponseWithStatusCodeAndTextTests()
        {
            const string message = nameof(GetReponseWithStatusCodeTests);
            const HttpStatusCode statusCode = HttpStatusCode.Conflict;

            ApplicationBuilder.RegisterExceptionHandler().Catch<DirectoryNotFoundException>().AndReturnAsync(statusCode, message);

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new DirectoryNotFoundException(message));

            result.StatusCode.Should().BeEquivalentTo(statusCode);
            result.Message.Should().BeEquivalentTo(message);
        }
    }
}