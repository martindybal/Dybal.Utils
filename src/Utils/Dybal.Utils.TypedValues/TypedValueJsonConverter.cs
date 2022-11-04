using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dybal.Utils.TypedValues;

public class TypedValueJsonConverter<TValue, TTypedValue> : JsonConverter<TTypedValue>
    where TValue : IComparable<TValue>
    where TTypedValue : TypedValue<TValue>
{
    public override TTypedValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = JsonSerializer.Deserialize<TValue>(ref reader, options);

        return value is null ?
            null :
            CreateTypedValue(value);
    }

    public override void Write(Utf8JsonWriter writer, TTypedValue typedValue, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, typedValue.Value, options);
    }

    private TTypedValue CreateTypedValue(TValue value)
    {
        TTypedValue CreateInstanceWithPrivateConstructor()
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            var constructorParameters = new object[] { value };
            var typedValueInstance = Activator.CreateInstance(typeof(TTypedValue), bindingFlags, null, constructorParameters, null);

            return (TTypedValue)typedValueInstance!;
        }

        return CreateInstanceWithPrivateConstructor();
    }
}

public class TypedIdJsonConverter<TTypedId> : TypedValueJsonConverter<Guid, TTypedId>
    where TTypedId : TypedId<TTypedId>
{

}