namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> HasValue<TArgument>(this ArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.ArgumentValue.HasValue == false)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Nullable object must have a value.");
        }
        
        return ArgumentGuard<TArgument>.From(guard.ArgumentValue.Value, guard.ArgumentName, ((IExceptionOverride)guard).ExceptionOverrideType);
    }
}