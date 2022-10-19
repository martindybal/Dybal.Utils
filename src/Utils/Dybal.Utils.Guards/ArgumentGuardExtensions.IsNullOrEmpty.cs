namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrEmpty(this IArgumentGuard<string?> guard, string? message = null)
    {
        if (string.IsNullOrEmpty(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value cannot be null or empty string.");
        }

        return guard.Argument.Value;
    }
}