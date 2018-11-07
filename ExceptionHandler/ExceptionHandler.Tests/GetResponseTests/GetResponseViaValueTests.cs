using System.IO;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ExceptionHandler.Tests.GetResponseTests
{
    public class GetResponseViaValueTests : GetResponseTest
    {
        [Fact]
        public async Task GetReponseWithStatusCodeTests()
        {
            const string message = nameof(GetReponseWithStatusCodeTests);
            const HttpStatusCode statusCode = HttpStatusCode.Conflict;

            ApplicationBuilder.UseExceptionMiddleware().Catch<FileNotFoundException>().AndReturnAsync(statusCode);

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new FileNotFoundException(message), null);

            result.StatusCode.Should().BeEquivalentTo(statusCode);
            result.Message.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task GetReponseWithStatusCodeAndTextTests()
        {
            const string message = nameof(GetReponseWithStatusCodeTests);
            const HttpStatusCode statusCode = HttpStatusCode.Conflict;

            ApplicationBuilder.UseExceptionMiddleware().Catch<DirectoryNotFoundException>().AndReturnAsync(statusCode, message);

            var result = await Container.GetResponseAsync(new DefaultHttpContext(), new DirectoryNotFoundException(message), null);

            result.StatusCode.Should().BeEquivalentTo(statusCode);
            result.Message.Should().BeEquivalentTo(message);
        }
    }
}