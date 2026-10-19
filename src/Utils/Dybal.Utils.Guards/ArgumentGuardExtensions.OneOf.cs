namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> OneOf<TArgument>(this ArgumentGuard<TArgument> guard, IEnumerable<TArgument> allowedValues, IEqualityComparer<TArgument>? comparer = null, string? message = null)
    {
        ArgumentGuard<IEnumerable<TArgument>>.From(guard, new Argument<IEnumerable<TArgument>>(allowedValues, nameof(allowedValues))).NotNull();

        if (!allowedValues.Contains(guard.Argument.Value, comparer))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must be one of the allowed values.");
        }

        return guard;
    }
}
