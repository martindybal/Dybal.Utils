namespace Dybal.Utils.TypedValues;

public interface IReadonlyTypedValue<TValue>
    where TValue : IComparable<TValue>
{
    TValue Value { get; init; }
}

public interface IReadonlyTypedValue<TTypedValue, TValue> : IReadonlyTypedValue<TValue>, IEquatable<TTypedValue>, IComparable<TTypedValue>
    where TTypedValue : IReadonlyTypedValue<TTypedValue, TValue>
    where TValue : IComparable<TValue>
{
}