using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Tests
{
    public class CustomMiddleware : Middleware
    {
        public CustomMiddleware(RequestDelegate next) 
            : base(next)
        {
        }
    }
}