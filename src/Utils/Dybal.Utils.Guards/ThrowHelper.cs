using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards
{
    internal static class ThrowHelper
    {
        public delegate TException ExceptionFactory<out TException>(string paramName, string? message = null)
            where TException : Exception;

        private static readonly Dictionary<Type, ExceptionFactory<Exception>> supportedExceptions = new();

        static ThrowHelper()
        {
            Register(ArgumentExceptionFactory);
            Register(ArgumentNullExceptionFactory);
            Register(ArgumentOutOfRangeExceptionFactory);
        }

        public static void Register<TException>(ExceptionFactory<TException> exceptionFactory) 
            where TException : Exception
        {
            supportedExceptions.Add(typeof(TException), exceptionFactory);
        }

        [DoesNotReturn]
        public static void Throw<TException>(string paramName, string? message = null)
            where TException : Exception
        {
            Throw(typeof(TException), paramName, message);
        }

        [DoesNotReturn]
        public static void Throw(Type exceptionOverrideType, string paramName, string? message = null)
        {
            if (supportedExceptions.TryGetValue(exceptionOverrideType, out var exceptionFactory))
            {
                throw exceptionFactory(paramName, message);
            }

            throw new ExceptionNotRegisteredException(exceptionOverrideType);
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

        private static ArgumentOutOfRangeException ArgumentOutOfRangeExceptionFactory(string paramName, string? message)
        {
            return new ArgumentOutOfRangeException(paramName, message);
        }
    }
}
