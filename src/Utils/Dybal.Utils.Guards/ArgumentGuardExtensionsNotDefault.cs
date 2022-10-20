namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument NotDefault<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
    {
        if (EqualityComparer<TArgument>.Default.Equals(guard.Argument.Value, default))
        {
            var defaultMessage = guard.Argument.Value is Guid ?
                                    "Value cannot be an empty GUID." :
                                    "Value cannot be the default value.";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }

        return guard.Argument.Value;
    }

    public static TArgument? NotDefault<TArgument>(this IArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.Argument.Value.HasValue == false)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Nullable object must have a value.");
        }

        return Guard.Argument(guard.Argument.Value.Value, guard.Argument.Name).NotDefault();
    }
}