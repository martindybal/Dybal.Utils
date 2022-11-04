using System.Text.Json.Serialization;
using Dybal.Utils.TypedValues;

namespace Tests.Dybal.Utils.TypedValues.TestTypes;


[JsonConverter(typeof(TypedValueJsonConverter<string, TestStringTypedValueB>))]
public record TestStringTypedValueB(string Value) : TypedValue<string>(Value);