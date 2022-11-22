namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> HasValue<TArgument>(this ArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.Argument.Value.HasValue == false)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Nullable object must have a value.");
        }
        
        return ArgumentGuard<TArgument>.From(guard, new Argument<TArgument>(guard.Argument.Value.Value, guard.Argument.Name));
    }
}