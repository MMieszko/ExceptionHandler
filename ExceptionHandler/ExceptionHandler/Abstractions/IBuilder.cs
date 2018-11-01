using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler.Abstractions
{
    public interface IBuilder
    {
        IApplicationBuilder AndWriteResponse(Response response);
        IApplicationBuilder AndWriteResponse(Func<Exception, Response> responseWriter);
        IApplicationBuilder AndWriteResponse(Func<HttpContext, Exception, Response> responseWriter);
    }
}