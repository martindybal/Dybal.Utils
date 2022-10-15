namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument? Null<TArgument>(this ArgumentGuard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive && guard.ArgumentValue is not null)
        {
            throw new ArgumentException(message ?? "Value must be null.", guard.ArgumentName);
        }

        return guard.ArgumentValue;
    }
}