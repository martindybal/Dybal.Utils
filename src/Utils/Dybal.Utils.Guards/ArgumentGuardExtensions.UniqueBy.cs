namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<IEnumerable<TItem>> UniqueBy<TItem, TKey>(this ICovariantArgumentGuard<IEnumerable<TItem>?> covariantGuard, Func<TItem, TKey> keySelector, IEqualityComparer<TKey>? comparer = null, string? message = null)
    {
        var guard = ArgumentGuard<IEnumerable<TItem>>.From(covariantGuard, covariantGuard.Argument!);
        return guard.UniqueBy<IEnumerable<TItem>, TItem, TKey>(keySelector, comparer, message);
    }

    public static ArgumentGuard<TEnumerable> UniqueBy<TEnumerable, TItem, TKey>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, Func<TItem, TKey> keySelector, IEqualityComparer<TKey>? comparer = null, string? message = null)
        where TEnumerable : IEnumerable<TItem>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        if (guard.Argument.Value is null)
        {
            ThrowHelper.Throw<ArgumentNullException>(guard, message);
        }

        ArgumentGuard<Func<TItem, TKey>>.From(guard, new Argument<Func<TItem, TKey>>(keySelector, nameof(keySelector))).NotNull();

        var seen = new HashSet<TKey>(comparer);
        foreach (var item in guard.Argument.Value)
        {
            if (!seen.Add(keySelector(item)))
            {
                ThrowHelper.Throw<ArgumentException>(guard, message ?? "Collection must not contain items with duplicate keys.");
            }
        }

        return guard;
    }
}
