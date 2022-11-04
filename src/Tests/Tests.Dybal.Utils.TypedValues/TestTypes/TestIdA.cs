using System.Text.Json.Serialization;
using Dybal.Utils.TypedValues;

namespace Tests.Dybal.Utils.TypedValues.TestTypes;

[JsonConverter(typeof(TypedIdJsonConverter<TestIdA>))]
public record TestIdA(Guid Value) : TypedId<TestIdA>(Value);