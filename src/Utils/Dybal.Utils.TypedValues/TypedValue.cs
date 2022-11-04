namespace Dybal.Utils.TypedValues
{
    public abstract record TypedValue<TValue>(TValue Value) : IComparable<TypedValue<TValue>>
        where TValue : IComparable<TValue>
    {
        public static implicit operator TValue(TypedValue<TValue> tValue) => tValue.Value;

        public int CompareTo(TypedValue<TValue>? other)
        {
            var otherValue = other is null ? default : other.Value;
            return Value.CompareTo(otherValue);
        }
    }
}