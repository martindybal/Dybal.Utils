namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> None<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> covariantGuard, Func<TArgument, bool> filter, string? message = null)
    {
        var guard = ArgumentGuard<IEnumerable<TArgument>>.From(covariantGuard, covariantGuard.Argument);
        return guard.None(filter, message);
    }

    public static ArgumentGuard<TEnumerable> None<TEnumerable, TArgument>(this ArgumentGuard<TEnumerable> guard, Func<TArgument, bool> filter, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var source = guard.Argument.Value;
        if (source.Any(filter))
        {
            message ??= "An item matches the predicate.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}