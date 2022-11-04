using System.Text.Json.Serialization;
using Dybal.Utils.TypedValues;

namespace Tests.Dybal.Utils.TypedValues.TestTypes
{
    [JsonConverter(typeof(TypedValueJsonConverter<string, TestStringTypedValueA>))]
    public record TestStringTypedValueA(string Value) : TypedValue<string>(Value);
}