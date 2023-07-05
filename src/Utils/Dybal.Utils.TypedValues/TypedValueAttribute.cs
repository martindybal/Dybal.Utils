namespace Dybal.Utils.TypedValues;

[AttributeUsage(AttributeTargets.Struct)]
public class TypedValueAttribute : Attribute
{
    public string ValueName { get; init; } = "Value";

    public Type ValueType { get; }

    public Converters Converters { get; init; }

    public TypedValueAttribute(Type valueType)
    {
        ValueType = valueType;
    }
}

public class TypedValueAttribute<TValue> : TypedValueAttribute
{
    public TypedValueAttribute() : base(typeof(TValue))
    {

    }
}