using System.Net;
using FluentAssertions;
using Xunit;

namespace ExceptionHandler.Tests.ResponseTests
{
    public class ResponseTests
    {
        [Theory]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public void CreateEmptyResponseShouldReturnEmptyMessageWithGivenStatusCode(HttpStatusCode statusCode)
        {
            var response = Response.Empty(statusCode);

            response.StatusCode.Should().BeEquivalentTo(statusCode);
            response.Message.Should().Be(string.Empty);
        }
    }
}