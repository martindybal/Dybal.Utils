using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards;

public static class ThrowHelper
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

    internal static void EnsureRegistration<TException>() where TException : Exception
    {
        var exceptionType = typeof(TException);
        if (!supportedExceptions.ContainsKey(exceptionType))
        {
            throw new ExceptionNotRegisteredException(exceptionType);
        }
    }

    public static void Register<TException>(ExceptionFactory<TException> exceptionFactory)
        where TException : Exception
    {
        supportedExceptions.Add(typeof(TException), exceptionFactory);
    }

    [DoesNotReturn]
    public static void Throw<TException>(IExceptionOverride guard, string? message)
        where TException : Exception
    {
        var exceptionType = guard.ExceptionOverrideType ?? typeof(TException);
        Throw(exceptionType, guard.ArgumentName, message);
    }
        
    [DoesNotReturn]
    private static void Throw(Type exceptionType, string paramName, string? message = null)
    {
        if (supportedExceptions.TryGetValue(exceptionType, out var exceptionFactory))
        {
            throw exceptionFactory(paramName, message);
        }

        throw new ExceptionNotRegisteredException(exceptionType);
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