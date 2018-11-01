using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace ExceptionHandler.Configuration
{
    public static class HttpContextExtensions
    {
        public static async Task<string> ReadRequestBodyAsync(this HttpContext context)
        {
            string body;

            context.Request.EnableRewind();

            using (var reader = new StreamReader(context.Request.Body))
            {
                body = await reader.ReadToEndAsync();

                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
            }

            return body;
        }
    }
}