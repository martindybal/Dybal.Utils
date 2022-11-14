namespace Dybal.Utils.TypedValues;

public interface ITypedValue<TValue> : IReadonlyTypedValue<TValue>
    where TValue : IComparable<TValue>
{
    new TValue Value { get; set; }

    TValue IReadonlyTypedValue<TValue>.Value
    {
        get => Value;
        init => Value = value;
    }
}

public interface ITypedValue<TTypedValue, TValue> : IReadonlyTypedValue<TTypedValue, TValue>, ITypedValue<TValue>
    where TTypedValue : IReadonlyTypedValue<TTypedValue, TValue>
    where TValue : IComparable<TValue>
{
}