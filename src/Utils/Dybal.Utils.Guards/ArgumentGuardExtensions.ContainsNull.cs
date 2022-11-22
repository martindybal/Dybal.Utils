namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> ContainsNull<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.ContainsNull<IEnumerable<TArgument>, TArgument>(message);
    }

    public static ArgumentGuard<TEnumerable> ContainsNull<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        return guard.Any<TEnumerable, TArgument>(static item => item is null, message ?? "Collection has to contain null.");
    }
}