using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class GuardExtensions
{
    public static string NotNullOrEmpty(
#nullable disable // to disable Warning CS8620  Argument of type 'Guard<string?>' cannot be used for parameter 'guard' of type 'Guard<string>' due to differences in the nullability of reference types.
        this Guard<string> guard, 
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