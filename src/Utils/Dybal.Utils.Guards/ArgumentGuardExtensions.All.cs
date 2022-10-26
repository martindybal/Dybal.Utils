namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> All<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> covariantGuard, Func<TArgument, bool> filter, string? message = null)
    {
        var guard = ArgumentGuard<IEnumerable<TArgument>>.From(covariantGuard, covariantGuard.Argument);
        return guard.All(filter, message);
    }

    public static ArgumentGuard<TEnumerable> All<TEnumerable, TArgument>(this ArgumentGuard<TEnumerable> guard, Func<TArgument, bool> filter, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var source = guard.Argument.Value;
        if (source.Any() && !source.All(filter))
        {   
            message ??= "All items of collection must match predicate.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}