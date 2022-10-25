namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnumerable> Contain<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, TArgument? value, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard);
        message ??= $"Collection has to contain '{value}'.";
        return guard.Contain<TEnumerable, TArgument>(item => Equals(item, value), message);
    }

    public static ArgumentGuard<IEnumerable<TArgument>> Contain<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> covariantGuard, Func<TArgument, bool> filter, string? message = null)
    {
        var guard = ArgumentGuard<IEnumerable<TArgument>>.From(covariantGuard);
        return guard.Contain(filter, message);
    }

    public static ArgumentGuard<TEnumerable> Contain<TEnumerable, TArgument>(this ArgumentGuard<TEnumerable> guard, Func<TArgument, bool> filter, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        if (!guard.Argument.Value.Any(filter))
        {
            message ??= "Collection does not contain required item.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}