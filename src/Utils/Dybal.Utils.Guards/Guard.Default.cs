using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the given argument is not a default value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="message">Optional custom message.</param>
    /// <param name="paramName">Name of <c>value</c> parameter - DO NOT SET.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void Default<T>(T value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null) 
        where T : struct
    {
        if (!value.Equals(default(T)))
        {
            var defaultMessage = value is Guid ? "Value must be an empty GUID." : "Value must be a default value.";
            throw new ArgumentException(message ?? defaultMessage, paramName);
        }
    }
}