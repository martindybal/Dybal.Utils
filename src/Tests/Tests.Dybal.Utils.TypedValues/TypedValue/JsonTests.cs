using System.Text.Json;
using Tests.Dybal.Utils.TypedValues.TestTypes;
using Xunit;

namespace Tests.Dybal.Utils.TypedValues.TypedValue;

public class JsonTests : TestBase
{
    [Fact]
    public void Should_SerializeTypedValueAsValue()
    {
        //Arrange
        var testIdA = new TestIdA(Guid.NewGuid());

        //Act
        var json = JsonSerializer.Serialize(testIdA);
        
        //Assert
        var expectedJson = JsonSerializer.Serialize(testIdA.Value);
        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void Should_SerializeTypedGuidAsGuid()
    {
        //Arrange
        var testIdA = new TestIdA(new Guid("c6dd9b02-2ec5-4a98-a68e-3dfcff2ae657"));

        //Act
        var json = JsonSerializer.Serialize(testIdA);
        
        //Assert
        var expectedJson = "\"c6dd9b02-2ec5-4a98-a68e-3dfcff2ae657\"";
        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void Should_DeserializeValueAsTypedValue()
    {
        //Arrange
        var idValue = new Guid("c6dd9b02-2ec5-4a98-a68e-3dfcff2ae657");
        var json = $"\"{idValue}\"";
        
        //Act
        var testIdA = JsonSerializer.Deserialize<TestIdA>(json);
        
        //Assert
        Assert.NotNull(testIdA);
        Assert.Equal(idValue, testIdA.Value);
    }
}