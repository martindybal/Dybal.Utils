using Tests.Dybal.Utils.TypedValues.TestTypes;
using Xunit;

namespace Tests.Dybal.Utils.TypedValues.TypedValue;

public class EqualsTests : TestBase
{
    [Fact]
    public void Should_BeEqual_When_ValueIsEqual()
    {
        //Arrange
        var value = "Hi types";
        var typedValue1 = new TestStringTypedValueA(value);
        var typedValue2 = new TestStringTypedValueA(value);
        
        //Act
        var areEqual = typedValue1.Equals(typedValue2);

        //Assert
        Assert.True(areEqual);
    }

    [Fact]
    public void Should_NotBeEqual_When_ValueIsNotEqual()
    {
        //Arrange
        var typedValue1 = new TestStringTypedValueA("1");
        var typedValue2 = new TestStringTypedValueA("2");

        //Act
        var areEqual = typedValue1.Equals(typedValue2);

        //Assert
        Assert.False(areEqual);
    }

    [Fact]
    public void Should_NotBeEqual_When_TypeIsNotEqual()
    {
        //Arrange
        var value = "Hi types";
        var typedValueA = new TestStringTypedValueA(value);
        var typedValueB = new TestStringTypedValueB(value);

        //Act
        var areEqual = typedValueA == typedValueB;

        //Assert
        Assert.False(areEqual);
    }
}