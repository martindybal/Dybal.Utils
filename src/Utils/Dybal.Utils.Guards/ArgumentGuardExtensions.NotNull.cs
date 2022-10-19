namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument NotNull<TArgument>(this IArgumentGuard<TArgument?> guard, string? message = null)
    {
        if (guard.Argument.Value is null)
        {
            ThrowHelper.Throw<ArgumentNullException>(guard, message);
        }

        return guard.Argument.Value;
    }
}