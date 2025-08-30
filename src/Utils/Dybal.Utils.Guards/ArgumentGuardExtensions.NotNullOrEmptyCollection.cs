namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnumerable> NotNullOrEmpty<TEnumerable, TItem>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TItem>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);

        var value = guard.Argument.Value;
        if (value is null || !value.Any())
        {
            message ??= "Collection cannot be null or empty.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}
