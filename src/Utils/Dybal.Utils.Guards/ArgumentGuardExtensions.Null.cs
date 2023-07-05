namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument?> Null<TArgument>(this ArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.Argument.Value is not null)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must be null.");
        }

        return guard!;
    }
}