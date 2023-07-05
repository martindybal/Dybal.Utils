namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument?> NotDefaultIfHasValue<TArgument>(this ArgumentGuard<TArgument?> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.Argument.Value.HasValue)
        {
             var valueGuard = ArgumentGuard<TArgument>.From(guard, new Argument<TArgument>(guard.Argument.Value.Value, guard.Argument.Name));
             valueGuard.NotDefault(message);
        }

        return guard;
    }
}