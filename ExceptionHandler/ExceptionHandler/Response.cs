using System.Net;

namespace ExceptionHandler
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public static Response Empty(HttpStatusCode statucCode) => new Response(statucCode, string.Empty);

        public Response(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}