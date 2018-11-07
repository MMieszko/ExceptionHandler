using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;

namespace ExceptionHandler.Tests.GetResponseTests
{
    public abstract class GetResponseTest
    {
        protected readonly IApplicationBuilder ApplicationBuilder;

        protected GetResponseTest()
        {
            ApplicationBuilder = new ApplicationBuilder(null);
        }
    }
}
