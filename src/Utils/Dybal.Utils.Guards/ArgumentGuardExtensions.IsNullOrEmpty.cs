namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static string NotNullOrEmpty(
#nullable disable // to disable Warning CS8620  Argument of type 'ArgumentGuard<string?>' cannot be used for parameter 'guard' of type 'ArgumentGuard<string>' due to differences in the nullability of reference types.
        this ArgumentGuard<string> guard, 
#nullable enable
        string? message = null)
    {
        if (guard.IsActive && string.IsNullOrEmpty(guard.Argument.Value))
        {
            throw new ArgumentException(message ?? "Value cannot be null or empty string.", guard.Argument.Name);
        }

        return guard.Argument.Value!;
    }
}