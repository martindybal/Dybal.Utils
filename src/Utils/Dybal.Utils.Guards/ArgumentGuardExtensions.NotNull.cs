namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> NotNull<TArgument>(this ICovariantArgumentGuard<TArgument?> covariantGuard, string? message = null)
    {
        var guard = ArgumentGuard<TArgument>.From(covariantGuard);
        if (guard.Argument.Value is null)
        {
            ThrowHelper.Throw<ArgumentNullException>(guard, message);
        }

        return guard;
    }
}