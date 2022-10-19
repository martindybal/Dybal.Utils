namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument? Null<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.Argument.Value is not null)
            {
                ThrowHelper.Throw<ArgumentException>(guard.Argument.Name, message ?? "Value must be null.");
            }
        }

        return guard.Argument.Value;
    }
}