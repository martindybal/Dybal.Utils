using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is not an empty collection.
    /// </summary>
    /// <param name="source">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void Empty<T>(IEnumerable<T>? source, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        NotNull(source, message, paramName);
            
        if (source.Any())
        {
            throw new ArgumentException(message ?? "Collection has to be empty.", paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is not an empty collection.
    /// </summary>
    /// <param name="source">The value to be checked.</param>
    /// <param name="condition">Condition whether the value should be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    public static void EmptyIf<T>(IEnumerable<T>? source, bool condition, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        if (condition)
        {
            Empty(source, message, paramName);
        }
    }
}