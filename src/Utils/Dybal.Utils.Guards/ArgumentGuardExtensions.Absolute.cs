namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<Uri> Absolute(this ICovariantArgumentGuard<Uri?> covariantGuard, string? message = null)
    {
        var guard = covariantGuard.NotNull(message);
        if (!guard.Argument.Value.IsAbsoluteUri)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must be an absolute URI.");
        }

        return guard;
    }
}
