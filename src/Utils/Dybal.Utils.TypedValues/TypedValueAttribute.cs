namespace Dybal.Utils.TypedValues;

[AttributeUsage(AttributeTargets.Struct)]
public class TypedValueAttribute : Attribute
{
    public string ValueName { get; set; } = "Value";

    public Type TValueType { get; }

    public TypedValueAttribute(Type valueType)
    {
        TValueType = valueType;
    }
}

public class TypedValueAttribute<TValue> : TypedValueAttribute
{
    public TypedValueAttribute() : base(typeof(TValue))
    {

    }
}