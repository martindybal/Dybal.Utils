using System;
using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards
{
    internal static class ThrowHelper
    {
        public delegate TException ExceptionFactory<out TException>(string paramName, string? message = null)
            where TException : Exception;

        private static readonly Dictionary<Type, ExceptionFactory<Exception>> supportedExceptions = new()
        {
            { typeof(ArgumentException), ArgumentExceptionFactory },
            {typeof(ArgumentNullException), ArgumentNullExceptionFactory }
        };

        [DoesNotReturn]
        public static void Throw<TException>(string paramName, string? message = null)
            where TException : Exception
        {
            var exceptionFactory = supportedExceptions[typeof(TException)];
            throw exceptionFactory(paramName, message);
        }

        private static ArgumentException ArgumentExceptionFactory(string paramName, string? message)
        {
            return new ArgumentException(message, paramName);
        }

        private static ArgumentNullException ArgumentNullExceptionFactory(string paramName, string? message)
        {
            if (message is null)
            {
                return new ArgumentNullException(paramName);
            }
            return new ArgumentNullException(paramName, message);
        }
    }
}
