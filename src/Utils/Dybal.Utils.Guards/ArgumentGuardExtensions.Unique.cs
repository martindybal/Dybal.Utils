namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TItem>> Unique<TItem>(this ICovariantArgumentGuard<IEnumerable<TItem>?> covariantGuard, IEqualityComparer<TItem>? comparer = null, string? message = null)
    {
        var guard = ArgumentGuard<IEnumerable<TItem>>.From(covariantGuard, covariantGuard.Argument!);
        return guard.Unique<IEnumerable<TItem>, TItem>(comparer, message);
    }

    public static ArgumentGuard<TEnumerable> Unique<TEnumerable, TItem>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, IEqualityComparer<TItem>? comparer = null, string? message = null)
        where TEnumerable : IEnumerable<TItem>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        if (guard.Argument.Value is null)
        {
            ThrowHelper.Throw<ArgumentNullException>(guard, message);
        }

        var seen = new HashSet<TItem>(comparer);
        foreach (var item in guard.Argument.Value)
        {
            if (!seen.Add(item))
            {
                ThrowHelper.Throw<ArgumentException>(guard, message ?? "Collection must not contain duplicate items.");
            }
        }

        return guard;
    }
}
