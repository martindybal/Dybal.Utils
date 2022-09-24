using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is an empty collection.
    /// </summary>
    /// <param name="source">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotEmpty<T>(IEnumerable<T>? source, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        // ReSharper disable PossibleMultipleEnumeration, just null check no enumeration
        NotNull(source, message);
            
        if (source.Any() != true)
        {
            throw new ArgumentException(message ?? "Collection cannot be empty.", paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is an empty collection.
    /// </summary>
    /// <param name="source">The value to be checked.</param>
    /// <param name="condition">Condition whether the value should be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotEmptyIf<T>(IEnumerable<T>? source, bool condition, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        if (condition)
        {
            NotEmpty(source, message, paramName);
        }
    }
}