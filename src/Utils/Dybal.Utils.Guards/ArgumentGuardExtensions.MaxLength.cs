namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> MaxLength(this ArgumentGuard<string> guard, uint maxLength, string? message = null)
    {
        if (guard.ArgumentValue.Length > maxLength)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"The length of '{guard.ArgumentName}' must be {maxLength} characters or fewer. Parameter has {guard.ArgumentValue.Length} characters.");
        }

        return guard;
    }
}