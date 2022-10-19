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
            guard.Throw<ArgumentException>(message ?? defaultMessage);
        }

        return guard.Argument.Value;
    }

    public static TArgument? NotDefault<TArgument>(this IArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.Argument.Value.HasValue)
        {
            return Guard.Argument(guard.Argument.Value.Value, guard.Argument.Name).NotDefault();
        }

        guard.Throw<ArgumentException>(message ?? "Nullable object must have a value.");

        return guard.Argument.Value;
    }
}