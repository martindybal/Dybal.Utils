namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> NullOrEmpty(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        var guard = ArgumentGuard<string>.From(covariantGuard, covariantGuard.Argument!);
        if (!string.IsNullOrEmpty(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must be null or empty string.");
        }

        return guard;
    }
}