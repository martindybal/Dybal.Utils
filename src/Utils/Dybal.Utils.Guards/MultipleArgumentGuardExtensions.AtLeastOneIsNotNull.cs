namespace Dybal.Utils.Guards;

public static partial class MultipleArgumentGuardExtensions
{
    public static MultipleArgumentGuard AtLeastOneIsNotNull(this MultipleArgumentGuard guard, string? message = null)
    {
        if (guard.Arguments.Select(static argument => argument.Value).All(static value => value is null))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Some of arguments must be not null.");
        }

        return guard;
    }
}