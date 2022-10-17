namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument NotDefault<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (EqualityComparer<TArgument>.Default.Equals(guard.ArgumentValue, default))
            {
                var defaultMessage = guard.ArgumentValue is Guid ?
                                        "Value cannot be an empty GUID." :
                                        "Value cannot be the default value.";
                ThrowHelper.ThrowArgumentException(message ?? defaultMessage, guard.ArgumentName);
            }
        }

        return guard.ArgumentValue;
    }

    public static TArgument? NotDefault<TArgument>(this IArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.IsActive)
        {
            if (guard.ArgumentValue.HasValue)
            {
                return Guard.Argument(guard.ArgumentValue.Value, guard.ArgumentName).NotDefault();
            }

            ThrowHelper.ThrowArgumentException(message ?? "Nullable object must have a value.", guard.ArgumentName);
        }

        return guard.ArgumentValue;
    }
}