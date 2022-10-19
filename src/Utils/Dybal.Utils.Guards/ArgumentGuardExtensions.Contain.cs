namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TEnumerable Contain<TEnumerable, TArgument>(this IArgumentGuard<TEnumerable> guard, TArgument? value, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        message ??= $"Collection has to contain '{value}'.";
        return guard.Contain<TEnumerable, TArgument>(item => Equals(item, value), message);
    }
    
    public static IEnumerable<TArgument> Contain<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, Func<TArgument, bool> filter, string? message = null)
    {
        return guard.Contain<IEnumerable<TArgument>, TArgument>(filter, message);
    }

    public static TEnumerable Contain<TEnumerable, TArgument>(this IArgumentGuard<TEnumerable> guard, Func<TArgument, bool> filter, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        if (!guard.Argument.Value.Any(filter))
        {
            message ??= "Collection does not contain required item.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard.Argument.Value;
    }
}