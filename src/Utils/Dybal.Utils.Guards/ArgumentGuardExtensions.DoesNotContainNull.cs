namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> DoesNotContainNull<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.DoesNotContainNull<IEnumerable<TArgument>, TArgument>(message);
    }

    public static ArgumentGuard<TEnumerable> DoesNotContainNull<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        return guard.All<TEnumerable, TArgument>(static item => item is not null, message ?? "Collection must not contain null.");
    }
}