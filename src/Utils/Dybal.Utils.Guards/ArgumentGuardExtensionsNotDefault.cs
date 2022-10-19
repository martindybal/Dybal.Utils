namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument NotDefault<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (EqualityComparer<TArgument>.Default.Equals(guard.Argument.Value, default))
            {
                var defaultMessage = guard.Argument.Value is Guid ?
                                        "Value cannot be an empty GUID." :
                                        "Value cannot be the default value.";
                ThrowHelper.ThrowArgumentException(message ?? defaultMessage, guard.Argument.Name);
            }
        }

        return guard.Argument.Value;
    }

    public static TArgument? NotDefault<TArgument>(this IArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.IsActive)
        {
            if (guard.Argument.Value.HasValue)
            {
                return Guard.Argument(guard.Argument.Value.Value, guard.Argument.Name).NotDefault();
            }

            ThrowHelper.ThrowArgumentException(message ?? "Nullable object must have a value.", guard.Argument.Name);
        }

        return guard.Argument.Value;
    }
}