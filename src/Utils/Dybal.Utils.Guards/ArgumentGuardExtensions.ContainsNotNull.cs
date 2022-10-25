namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> ContainsNotNull<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> covariantGuard, string? message = null)
    {
        return covariantGuard.ContainsNotNull<IEnumerable<TArgument>, TArgument>(message);
    }

    public static ArgumentGuard<TEnumerable> ContainsNotNull<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        return guard.Any<TEnumerable, TArgument>(static item => item is not null, message ?? "Collection has to contain an item with not default value.");
    }
}