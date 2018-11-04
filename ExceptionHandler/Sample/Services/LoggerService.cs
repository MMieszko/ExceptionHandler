using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sample.Services
{
    public class LoggerService
    {
        public Task LogAsync(string message)
        {
            Debug.WriteLine(message);

            return Task.CompletedTask;
        }   
    }
}