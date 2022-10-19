using System;
using System.Collections;

namespace Dybal.Utils.Guards
{
    /// <summary>
    /// Custom List implementation that does not make any dynamic allocation if Count <= 3
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    internal readonly struct CompactList<TElement> : IReadOnlyList<TElement?>
    {
        private readonly TElement? firstElement = default;
        private readonly TElement? secondElement = default;
        private readonly TElement? thirdElement = default;
        private readonly TElement?[]? otherElements = default;

        public CompactList(TElement? firstElement, TElement? secondElement)
        {
            this.firstElement = firstElement;
            this.secondElement = secondElement;
            Count = 2;
        }

        public CompactList(TElement? firstElement, TElement? secondElement, TElement? thirdElement, params TElement?[] otherElements)
            : this(firstElement, secondElement)
        {
            this.firstElement = firstElement;
            this.secondElement = secondElement;
            this.thirdElement = thirdElement;
            this.otherElements = otherElements;

            Count = 3 + otherElements.Length;
        }
        
        public TElement? this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    ThrowHelper.ThrowArgumentException("Provided index was outside of the valid range.", nameof(index));
                }

                return index switch
                {
                    0 => firstElement,
                    1 => secondElement,
                    2 => thirdElement,
                    _ => otherElements![index - 3],
                };
            }
        }

        public int Count { get; }

        public IEnumerator<TElement?> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return this[index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
