namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string?> NotWhiteSpaceIfNotNull(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        var guard = ArgumentGuard<string?>.From(covariantGuard, covariantGuard.Argument);
        if (guard.Argument.Value is not null && string.IsNullOrWhiteSpace(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must be null or a non-whitespace string.");
        }

        return guard;
    }
}
