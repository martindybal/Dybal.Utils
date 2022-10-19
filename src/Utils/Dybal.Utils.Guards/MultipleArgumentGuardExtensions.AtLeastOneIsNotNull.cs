namespace Dybal.Utils.Guards;

public static partial class MultipleArgumentGuardExtensions
{
    public static void AtLeastOneIsNotNull(this MultipleArgumentGuard guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.Arguments.Select(static argument => argument.Value).All(static value => value is null))
            {
                var argumentNames = string.Join(", ", guard.Arguments.Select(static argument => argument.Name));
                ThrowHelper.Throw<ArgumentException>(argumentNames, message ?? $"Some of {argumentNames} must be not null.");
            }
        }
    }
}