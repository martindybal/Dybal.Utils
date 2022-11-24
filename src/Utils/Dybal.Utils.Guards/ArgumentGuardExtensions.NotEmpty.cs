namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> NotEmpty<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.NotEmpty<IEnumerable<TArgument>, TArgument>(message);
    }

    public static ArgumentGuard<TEnumerable> NotEmpty<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        if (!guard.Argument.Value.Any())
        {
            message ??= "Collection cannot be empty.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}