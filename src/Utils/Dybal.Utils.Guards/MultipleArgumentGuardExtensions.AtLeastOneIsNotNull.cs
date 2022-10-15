namespace Dybal.Utils.Guards;

public static partial class MultipleArgumentGuardExtensions
{
    public static void AtLeastOneIsNotNull(this MultipleArgumentGuard guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.Arguments.Select(argument => argument.Value).All(value => value is null))
            {
                var argumentNames = string.Join(", ", guard.Arguments.Select(argument => argument.Name));
                throw new ArgumentException(message ?? $"Some of {argumentNames} must be not null.", argumentNames);
            }
        }
    }
}