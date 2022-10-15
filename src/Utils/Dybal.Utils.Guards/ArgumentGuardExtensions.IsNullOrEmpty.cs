namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrEmpty(
        this IArgumentGuard<string?> guard,
        string? message = null)
    {
        if (guard.IsActive)
        {
            if (string.IsNullOrEmpty(guard.ArgumentValue))
            {
                throw new ArgumentException(message ?? "Value cannot be null or empty string.", guard.ArgumentName);
            }
        }

        return guard.ArgumentValue!;
    }
}