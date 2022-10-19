namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IArgumentGuard<TArgument?> IfHasValue<TArgument>(this IArgumentGuard<TArgument?> guard)
        where TArgument : struct
    {
        return guard.If(guard.Argument.Value.HasValue);
    }
}