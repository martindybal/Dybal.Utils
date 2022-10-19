namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrEmpty(this IArgumentGuard<string?> guard, string? message = null)
    {
        if (string.IsNullOrEmpty(guard.Argument.Value))
        {
            guard.Throw<ArgumentException>(message ?? "Value cannot be null or empty string.");
        }

        return guard.Argument.Value;
    }
}