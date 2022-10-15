using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerable<TArgument> ContainNull<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, string? message = null)
    {
        return guard.Contain(item => item is null, message ?? "Collection has to contain null.");
    }
}