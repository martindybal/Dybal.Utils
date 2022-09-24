using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    public static void DoesNotContainNull<T>(IEnumerable<T> source, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        DoesNotContain(source, item => item is null, message, paramName);
    }

    public static void DoesNotContain<T>(IEnumerable<T> source, T value, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        DoesNotContain(source, item => Equals(item, value), message, paramName);
    }

    public static void DoesNotContain<T>(IEnumerable<T> source, Func<T, bool> filter, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        foreach (var item in source.Where(filter))
        {
            if (item is null)
            {
                throw new ArgumentNullException(paramName, message ?? "Collection cannot contain 'null'.");
            }

            throw new ArgumentException(message ?? $"Collection cannot contain '{item}'.", paramName);
        }
    }
}