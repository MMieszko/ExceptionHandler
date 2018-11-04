using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExceptionHandler.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandler
{
    public static class Container
    {
        private static readonly Dictionary<Type, Delegate> Dictionary;

        static Container()
        {
            Dictionary = new Dictionary<Type, Delegate>();
        }

        public static void Add<TException>(Delegate @delegate)
            where TException : Exception
        {
            if (Dictionary.ContainsKey(typeof(TException)))
            {
                throw new DuplicateKeyException($"The exception -  {typeof(TException).FullName} is already catched.");
            }

            Dictionary.Add(typeof(TException), @delegate);
        }

        public static async Task<Response> GetResponseAsync<TException>(HttpContext httpContext, TException exception, IServiceProvider serviceProvider)
            where TException : Exception
        {
            if (!Dictionary.ContainsKey(exception.GetType()))
            {
                //TODO: Handle global
            }

            var @delegate = Dictionary[exception.GetType()];

            switch (@delegate.Method.ReturnType)
            {
                case Type type when type == typeof(IHandler<TException>):
                    return await ((IHandler<TException>)@delegate.DynamicInvoke()).HandleAsync(httpContext, exception, serviceProvider);
                case Type type when type == typeof(Task<Response>):
                    return await (Task<Response>)@delegate.DynamicInvoke(httpContext, exception);
                case Type type when type == typeof(Response):
                    return (Response)@delegate.DynamicInvoke();
            }

            return null;
        }
    }
}