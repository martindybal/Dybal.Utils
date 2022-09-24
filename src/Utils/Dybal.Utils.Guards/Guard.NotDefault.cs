using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument has default value.
    /// </summary>
    /// <param name="value">Value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotDefault<T>(T value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (Equals(value, default(T)))
        {
            var defaultMessage = GetDefaultMessageForNotDefault(value);
            throw new ArgumentException(message ?? defaultMessage, paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument has default value.
    /// </summary>
    /// <param name="value">Value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotDefaultIfHasValue<T>(T? value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
        where T : struct
    {
        if (value.HasValue)
        {
            NotDefault(value.Value, message, paramName);
        }
    }

    private static string GetDefaultMessageForNotDefault<T>(T result)
    {
        return result is Guid ? "Value cannot be an empty GUID." : "Value cannot be the default value.";
    }
}