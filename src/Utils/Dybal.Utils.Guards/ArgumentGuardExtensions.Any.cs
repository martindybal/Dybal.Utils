namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> Any<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> covariantGuard, Func<TArgument, bool> filter, string? message = null)
    {
        var guard = ArgumentGuard<IEnumerable<TArgument>>.From(covariantGuard, covariantGuard.ArgumentValue, covariantGuard.ArgumentName);
        return guard.Any(filter, message);
    }

    public static ArgumentGuard<TEnumerable> Any<TEnumerable, TArgument>(this ArgumentGuard<TEnumerable> guard, Func<TArgument, bool> filter, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        if (!guard.ArgumentValue.Any(filter))
        {
            message ??= "No item matches the predicate.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}