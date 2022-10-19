namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrWhiteSpace(this IArgumentGuard<string?> guard, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value cannot be null or white space string.");
        }

        return guard.Argument.Value;
    }
}