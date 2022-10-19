namespace Dybal.Utils.Guards;

public static partial class MultipleArgumentGuardExtensions
{
    public static void AtLeastOneIsNotNull(this MultipleArgumentGuard guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.Arguments.Select(static argument => argument.Value).All(static value => value is null))
            {
                guard.Throw<ArgumentException>(message ?? "Some of arguments must be not null.");
            }
        }
    }
}