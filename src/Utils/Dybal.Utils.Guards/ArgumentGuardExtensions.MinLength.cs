namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> MinLength(this ArgumentGuard<string> guard, uint minLength, string? message = null)
    {
        if (guard.Argument.Value.Length < minLength)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"The length of '{guard.Argument.Name}' must be {minLength} characters or more. Parameter has {guard.Argument.Value.Length} characters.");
        }

        return guard;
    }
}