namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> NotDefault<TArgument>(this ArgumentGuard<TArgument> guard, string? message = null)
        where TArgument : struct
    {
        if (EqualityComparer<TArgument>.Default.Equals(guard.Argument.Value, default))
        {
            var defaultMessage = guard.Argument.Value is Guid
                ? "Value cannot be an empty GUID."
                : "Value cannot be the default value.";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }

        return guard;
    }
}