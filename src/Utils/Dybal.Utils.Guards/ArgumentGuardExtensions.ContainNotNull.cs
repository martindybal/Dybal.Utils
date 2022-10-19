namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerable<TArgument> ContainNotNull<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.ContainNotNull<IEnumerable<TArgument>, TArgument>(message);
    }

    public static TEnumerable ContainNotNull<TEnumerable, TArgument>(this IArgumentGuard<TEnumerable> guard, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        return guard.Contain<TEnumerable, TArgument>(static item => item is not null, message ?? "Collection has to contain an item with not default value.");
    }
}