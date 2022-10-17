namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrWhiteSpace(
        this IArgumentGuard<string?> guard, 
        string? message = null)
    {
        if (guard.IsActive)
        {
            if (string.IsNullOrWhiteSpace(guard.ArgumentValue))
            {
                ThrowHelper.ThrowArgumentException(message ?? "Value cannot be null or white space string.", guard.ArgumentName);
            }
        }

        return guard.ArgumentValue!;
    }
}