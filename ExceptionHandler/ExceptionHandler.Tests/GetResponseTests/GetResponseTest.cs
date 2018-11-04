using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;

namespace ExceptionHandler.Tests.GetResponseTests
{
    public class GetResponseTest
    {
        protected readonly IApplicationBuilder ApplicationBuilder;

        public GetResponseTest()
        {
            ApplicationBuilder = new ApplicationBuilder(null);
        }
    }
}
