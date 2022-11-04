using System.Text.Json.Serialization;
using Dybal.Utils.TypedValues;

namespace Tests.Dybal.Utils.TypedValues.TestTypes;

[JsonConverter(typeof(TypedIdJsonConverter<TestIdB>))]
public record TestIdB : TypedId<TestIdB>
{
    private TestIdB(Guid value) : base(value)
    {
    }
}