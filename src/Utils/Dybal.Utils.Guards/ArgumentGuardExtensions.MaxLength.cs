namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> MaxLength(this ArgumentGuard<string> guard, uint maxLength, string? message = null)
    {
        if (guard.Argument.Value.Length > maxLength)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"The length of '{guard.Argument.Name}' must be {maxLength} characters or fewer. Parameter has {guard.Argument.Value.Length} characters.");
        }

        return guard;
    }
}