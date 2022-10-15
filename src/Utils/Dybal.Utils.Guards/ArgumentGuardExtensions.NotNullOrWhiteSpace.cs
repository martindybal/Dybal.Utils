namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrWhiteSpace(
        this IArgumentGuard<string?> guard, 
        string? message = null)
    {
        if (guard.IsActive && string.IsNullOrWhiteSpace(guard.ArgumentValue))
        {
            throw new ArgumentException(message ?? "Value cannot be null or white space string.", guard.ArgumentName);
        }

        return guard.ArgumentValue!;
    }
}