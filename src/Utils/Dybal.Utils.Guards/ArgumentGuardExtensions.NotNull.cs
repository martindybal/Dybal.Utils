namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> NotNull<TArgument>(this ICovariantArgumentGuard<TArgument?> covariantGuard, string? message = null)
    {
        if (covariantGuard.ArgumentValue is null)
        {
            ThrowHelper.Throw<ArgumentNullException>(covariantGuard, message);
        }
        
        var guard = ArgumentGuard<TArgument>.From(covariantGuard, covariantGuard.ArgumentValue, covariantGuard.ArgumentName);
        return guard;
    }
}