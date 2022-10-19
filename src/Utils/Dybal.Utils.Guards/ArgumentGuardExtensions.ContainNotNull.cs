namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerable<TArgument> ContainNotNull<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.Contain(static item => item is not null, message ?? "Collection has to contain an item with not default value.");
    }
}