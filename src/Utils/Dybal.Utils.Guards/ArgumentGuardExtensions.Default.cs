namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument? Default<TArgument>(this IArgumentGuard<TArgument> guard, string? message = null)
        where TArgument : struct
    {
        if (guard.IsActive)
        {
            if (!guard.ArgumentValue.Equals(default(TArgument)))
            {
                var defaultMessage = guard.ArgumentValue is Guid ? 
                                        "Value must be an empty GUID." : 
                                        "Value must be a default value.";
                throw new ArgumentException(message ?? defaultMessage, guard.ArgumentName);
            }
        }

        return guard.ArgumentValue;
    }
}