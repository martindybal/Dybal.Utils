namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> NullOrWhiteSpace(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        var guard = ArgumentGuard<string>.From(covariantGuard, covariantGuard.Argument!);
        if (!string.IsNullOrWhiteSpace(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must be null or white space string.");
        }

        return guard;
    }
}