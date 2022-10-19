namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerable<TArgument> ContainNull<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.ContainNull<IEnumerable<TArgument>, TArgument>();
    }

    public static TEnumerable ContainNull<TEnumerable, TArgument>(this IArgumentGuard<TEnumerable> guard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        return guard.Contain<TEnumerable, TArgument>(static item => item is null, message ?? "Collection has to contain null.");
    }
}