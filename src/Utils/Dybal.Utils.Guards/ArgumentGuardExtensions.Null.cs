namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument? Null<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.ArgumentValue is not null)
            {
                ThrowHelper.ThrowArgumentException(message ?? "Value must be null.", guard.ArgumentName);
            }
        }

        return guard.ArgumentValue;
    }
}