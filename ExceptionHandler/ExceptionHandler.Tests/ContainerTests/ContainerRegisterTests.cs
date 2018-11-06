using System;
using System.Configuration;
using System.Data;
using System.Net;
using ExceptionHandler.Configuration;
using ExceptionHandler.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Xunit;

namespace ExceptionHandler.Tests.ContainerTests
{
    public class ContainerRegisterTests
    {
        [Fact]
        public void RegisterTheSameTypeTwiceShouldThrowException()
        {
            IApplicationBuilder applicationBuilder = new ApplicationBuilder(null);

            Action registration = () =>
            {
                applicationBuilder.Catch<InvalidConstraintException>().AndReturnAsync(HttpStatusCode.Accepted);
                applicationBuilder.Catch<InvalidConstraintException>().AndReturnAsync(HttpStatusCode.Ambiguous);
            };

            registration.Should().Throw<DuplicateKeyException>();
        }
    }
}