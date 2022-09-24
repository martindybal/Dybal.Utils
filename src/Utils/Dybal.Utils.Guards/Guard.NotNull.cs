using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the given argument is null.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void NotNull<T>([NotNull] T? value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is null)
        {
            throw message == null
                ? new ArgumentNullException(paramName)
                : new ArgumentNullException(paramName, message);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the given argument is null.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="condition">Condition whether the value should be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void NotNullIf<T>(T? value, bool condition, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (condition)
        {
            NotNull(value, message, paramName);
        }
    }
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is null or an empty string.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotNullOrEmpty([NotNull] string? value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(message ?? "Value cannot be null or an empty string.", paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is null or an empty string.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="condition">Condition whether the value should be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotNullOrEmptyIf(string? value, bool condition, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (condition)
        {
            NotNullOrEmpty(value, message, paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is null or white space string.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotNullOrWhiteSpace([NotNull] string? value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message ?? "Value cannot be null or white space string.", paramName);
        }
    }
}