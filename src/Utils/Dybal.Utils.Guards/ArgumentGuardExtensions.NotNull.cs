namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument NotNull<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.ArgumentValue is null)
            {
                throw message == null
                    ? new ArgumentNullException(guard.ArgumentName)
                    : new ArgumentNullException(guard.ArgumentName, message);
            }
        }

        return guard.ArgumentValue;
    }
}