namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> NotDefault<TArgument>(this ArgumentGuard<TArgument> guard, string? message = null)
    {
        NotDefault(guard.Argument.Value, guard, message);

        return guard;
    }

    public static ArgumentGuard<TArgument> NotDefault<TArgument>(this ArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.Argument.Value.HasValue == false)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Nullable object must have a value.");
        }

        NotDefault(guard.Argument.Value.Value, guard, message);

        return ArgumentGuard<TArgument>.From(guard, new Argument<TArgument>(guard.Argument.Value.Value, guard.Argument.Name));
    }
    
    private static void NotDefault<TArgument>(TArgument argumentValue, IExceptionOverride guard, string? message)
    {
        if (EqualityComparer<TArgument>.Default.Equals(argumentValue, default))
        {
            var defaultMessage = argumentValue is Guid
                ? "Value cannot be an empty GUID."
                : "Value cannot be the default value.";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }
    }
}