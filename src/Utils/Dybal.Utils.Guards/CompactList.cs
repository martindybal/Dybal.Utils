using System.Collections;

namespace Dybal.Utils.Guards;

/// <summary>
/// Custom List implementation that does not make any dynamic allocation if Count <= 3
/// </summary>
/// <typeparam name="TElement"></typeparam>
internal readonly struct CompactList<TElement> : IReadOnlyList<TElement>
{
    private readonly TElement item0;
    private readonly TElement item1;
    private readonly TElement? item2 = default;
    private readonly TElement? item3 = default;
    private readonly TElement? item4 = default;
    private readonly TElement? item5 = default;
    private readonly TElement? item6 = default;
    private readonly TElement? item7 = default;
    private readonly TElement? item8 = default;
    private readonly TElement? item9 = default;
    private readonly TElement[] otherItems;

    public int Count { get; }

    public TElement this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                ThrowHelper.Throw<ArgumentOutOfRangeException>(Guard.Argument(index), "Provided index was outside of the valid range.");
            }

            return index switch
            {
                0 => item0,
                1 => item1,
                2 => item2!,
                3 => item3!,
                4 => item4!,
                5 => item5!,
                6 => item6!,
                7 => item7!,
                8 => item8!,
                9 => item9!,
                _ => otherItems[index - 10]!,
            };
        }
    }

    public CompactList(TElement item0, TElement item1)
    {
        otherItems = Array.Empty<TElement>();
        this.item0 = item0;
        this.item1 = item1;
        Count = 2;
    }

    public CompactList(TElement item0, TElement item1, TElement item2)
        : this(item0, item1)
    {
        this.item2 = item2;
        Count = 3;
    }

    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3)
        : this(item0, item1, item2)
    {
        this.item3 = item3;
        Count = 4;
    }

    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3, TElement item4)
        : this(item0, item1, item2, item3)
    {
        this.item4 = item4;
        Count = 5;
    }

    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3, TElement item4, TElement item5)
        : this(item0, item1, item2, item3, item4)
    {
        this.item5 = item5;
        Count = 6;
    }

    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3, TElement item4, TElement item5, TElement item6)
        : this(item0, item1, item2, item3, item4, item5)
    {
        this.item6 = item6;
        Count = 7;
    }

    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3, TElement item4, TElement item5, TElement item6, TElement item7)
        : this(item0, item1, item2, item3, item4, item5, item6)
    {
        this.item7 = item7;
        Count = 8;
    }

    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3, TElement item4, TElement item5, TElement item6, TElement item7, TElement item8)
        : this(item0, item1, item2, item3, item4, item5, item6, item7)
    {
        this.item8 = item8;
        Count = 9;
    }
    
    public CompactList(TElement item0, TElement item1, TElement item2, TElement item3, TElement item4, TElement item5, TElement item6, TElement item7, TElement item8, TElement item9, params TElement[] otherItems)
        : this(item0, item1, item2, item3, item4, item5, item6, item7, item8)
    {
        this.item9 = item9;
        this.otherItems = otherItems;

        Count = 10 + otherItems.Length;
    }

    public IEnumerator<TElement> GetEnumerator()
    {
        for (var index = 0; index < Count; index++)
        {
            yield return this[index];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}