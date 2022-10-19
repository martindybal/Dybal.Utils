namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument? Default<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.IsActive)
        {
            if (!guard.Argument.Value.Equals(default(TArgument)))
            {
                var defaultMessage = guard.Argument.Value is Guid ? 
                                        "Value must be an empty GUID." : 
                                        "Value must be a default value.";
                guard.Throw<ArgumentException>(message ?? defaultMessage);
            }
        }

        return guard.Argument.Value;
    }
}