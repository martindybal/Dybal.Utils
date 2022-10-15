using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerable<TArgument> Contain<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, TArgument? value, string? message = null)
    {
        return guard.Contain(item => Equals(item, value), message ?? $"Collection has to contain '{value}'.");
    }

    public static IEnumerable<TArgument> Contain<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, Func<TArgument, bool> filter, string? message = null)
    {
        if (guard.IsActive)
        {
            if (!guard.ArgumentValue.Any(filter))
            {
                throw new ArgumentException(message ?? "Collection does not contain required item.", guard.ArgumentName);
            }
        }

        return guard.ArgumentValue;
    }
}