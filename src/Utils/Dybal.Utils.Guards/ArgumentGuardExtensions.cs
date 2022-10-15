namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument?> IfHasValue<TArgument>(this ArgumentGuard<TArgument?> guard)
        where TArgument : struct
    {
        return guard.If(guard.ArgumentValue.HasValue);
    }
}