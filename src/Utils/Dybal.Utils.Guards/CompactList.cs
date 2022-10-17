using System.Collections;

namespace Dybal.Utils.Guards
{
    /// <summary>
    /// Custom List implementation that does not make any dynamic allocation if Count <= 3
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public struct CompactList<TElement> : IList<TElement?>
    {
        private TElement? firstElement;
        private TElement? secondElement;
        private TElement? thirdElement;
        private List<TElement?>? otherElements;
        private int count;

        public TElement? this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    ThrowHelper.ThrowArgumentException("Provided index was outside of the valid range.", nameof(index));

                return index switch
                {
                    0 => firstElement,
                    1 => secondElement,
                    2 => thirdElement,
                    _ => otherElements![index - 3],
                };
            }
            set
            {
                if (index < 0 || index >= count)
                    ThrowHelper.ThrowArgumentException("Provided index was outside of the valid range.", nameof(index));

                switch (index)
                {
                    case 0: firstElement = value; break;
                    case 1: secondElement = value; break;
                    case 2: thirdElement = value; break;
                    default: otherElements![index] = value; break;
                }
            }
        }

        public int Count { get { return count; } set { count = value; } }
        public bool IsReadOnly => false;

        public void Add(TElement? item)
        {
            if (count <= 2)
            {
                switch (count)
                {
                    case 0: firstElement = item; break;
                    case 1: secondElement = item; break;
                    case 2: thirdElement = item; break;
                }
            }
            else
            {
                otherElements ??= new List<TElement?>();
                otherElements.Add(item);
            }

            count++;
        }

        public void Clear()
        {
            firstElement = default;
            secondElement = default;
            thirdElement = default;
            otherElements?.Clear();
        }

        public bool Contains(TElement? item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(TElement?[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TElement?> GetEnumerator()
        {
            for (var index = 0; index < count; index++)
                yield return this[index];
        }

        public int IndexOf(TElement? item)
        {
            if (item == null)
                return -1;

            for (var index = 0; index < count; index++)
            {
                if (item.Equals(this[index]))
                    return index;
            }

            return -1;
        }

        public void Insert(int index, TElement? item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TElement? item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
