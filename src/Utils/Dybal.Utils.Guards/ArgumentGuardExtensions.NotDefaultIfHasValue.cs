namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument?> NotDefaultIfHasValue<TArgument>(this ArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.ArgumentValue.HasValue)
        {
             var valueGuard = ArgumentGuard<TArgument>.From(guard.ArgumentValue.Value, guard.ArgumentName, ((IExceptionOverride)guard).ExceptionOverrideType);
             valueGuard.NotDefault(message);
        }

        return guard;
    }
}