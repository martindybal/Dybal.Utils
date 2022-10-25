namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> Empty<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.Empty<IEnumerable<TArgument>, TArgument>(message);
    }

    public static ArgumentGuard<TEnumerable> Empty<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        if (guard.Argument.Value.Any())
        {
            message ??= "Collection has to be empty.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}