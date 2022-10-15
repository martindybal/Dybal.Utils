namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument NotNull<TArgument>(this ArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive && guard.ArgumentValue is null)
        {
            throw message == null
                ? new ArgumentNullException(guard.ArgumentName)
                : new ArgumentNullException(guard.ArgumentName, message);
        }

        return guard.ArgumentValue;
    }
}