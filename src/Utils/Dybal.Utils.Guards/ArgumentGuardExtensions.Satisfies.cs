namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> Satisfies<TArgument>(this ArgumentGuard<TArgument> guard, Func<TArgument, bool> predicate, string? message = null)
    {
        ArgumentGuard<Func<TArgument, bool>>.From(guard, new Argument<Func<TArgument, bool>>(predicate, nameof(predicate))).NotNull();

        if (!predicate(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must satisfy the predicate.");
        }

        return guard;
    }
}
