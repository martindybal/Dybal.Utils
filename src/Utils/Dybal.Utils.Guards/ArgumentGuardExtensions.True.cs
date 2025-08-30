namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<bool> True(this ArgumentGuard<bool> guard, string? message = null)
    {
        if (!guard.Argument.Value)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must be true.");
        }

        return guard;
    }

    public static ArgumentGuard<bool> True(this ArgumentGuard<bool?> guard, string? message = null)
    {
        if (guard.Argument.Value != true)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must be true.");
        }

        return ArgumentGuard<bool>.From(guard, new Argument<bool>(guard.Argument.Value.Value, guard.Argument.Name));
    }
}
