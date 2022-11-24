namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> MinLength(this ArgumentGuard<string> guard, uint minLength, string? message = null)
    {
        if (guard.ArgumentValue.Length < minLength)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"The length of '{guard.ArgumentName}' must be {minLength} characters or more. Parameter has {guard.ArgumentValue.Length} characters.");
        }

        return guard;
    }
}