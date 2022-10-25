namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TArgument>> ContainNull<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.ContainNull<IEnumerable<TArgument>, TArgument>();
    }

    public static ArgumentGuard<TEnumerable> ContainNull<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        return guard.Contain<TEnumerable, TArgument>(static item => item is null, message ?? "Collection has to contain null.");
    }
}