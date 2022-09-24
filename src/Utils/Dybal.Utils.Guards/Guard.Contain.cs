using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    public static void ContainNull<T>(IEnumerable<T?> source, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        Contain(source, item => item is null, message ?? "Collection has to contain null.", paramName);
    }

    public static void ContainNotNull<T>(IEnumerable<T> source, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        Contain(source, item => item is not null, message ?? "Collection has to contain an item with not default value.");
    }

    public static void Contain<T>(IEnumerable<T> source, T? value, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {
        Contain(source, item => Equals(item, value), message ?? $"Collection has to contain '{value}'.");
    }

    public static void Contain<T>(IEnumerable<T> source, Func<T, bool> filter, string? message = null, [CallerArgumentExpression("source")] string? paramName = null)
    {

        if (!source.Any(filter))
        {
            throw new ArgumentException(message ?? "Collection does not contain required item.", paramName);
        }
    }
}