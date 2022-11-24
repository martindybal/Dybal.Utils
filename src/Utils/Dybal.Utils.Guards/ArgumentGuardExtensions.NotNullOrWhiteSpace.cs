namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> NotNullOrWhiteSpace(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(covariantGuard.ArgumentValue))
        {
            ThrowHelper.Throw<ArgumentException>(covariantGuard, message ?? "Value cannot be null or white space string.");
        }

        return ArgumentGuard<string>.From(covariantGuard, covariantGuard.ArgumentValue, covariantGuard.ArgumentName);
    }
}