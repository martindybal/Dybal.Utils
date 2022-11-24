namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> NotNullOrEmpty(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        if (string.IsNullOrEmpty(covariantGuard.ArgumentValue))
        {
            ThrowHelper.Throw<ArgumentException>(covariantGuard, message ?? "Value cannot be null or empty string.");
        }

        var guard = ArgumentGuard<string>.From(covariantGuard.ArgumentValue, covariantGuard.ArgumentName, covariantGuard.ExceptionOverrideType);
        return guard;
    }
}