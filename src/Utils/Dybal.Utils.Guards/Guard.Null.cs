using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the given argument is not null.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Null<T>(T value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is not null)
        {
            throw new ArgumentException(message ?? "Value must be null.", paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the given argument is not null.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="condition">Condition whether the value should be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void NullIf<T>(T value, bool condition, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (condition)
        {
            Null(value, message, paramName);
        }
    }
}