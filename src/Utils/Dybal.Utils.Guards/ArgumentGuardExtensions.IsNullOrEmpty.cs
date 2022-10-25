namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> NotNullOrEmpty(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        var guard = ArgumentGuard<string>.From(covariantGuard);
        if (string.IsNullOrEmpty(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value cannot be null or empty string.");
        }

        return guard;
    }
}