using Tests.Dybal.Utils.TypedValues.TestTypes;
using Xunit;

namespace Tests.Dybal.Utils.TypedValues.TypedValue;

public class ComparableTests : TestBase
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void Should_ReturnSameResultAsValueCompare(int value1, int value2)
    {
        //Arrange
        var typedValue1 = new TestIntTypedValueA(value1);
        var typedValue2 = new TestIntTypedValueA(value2);
        var expectedResult = value1.CompareTo(value2);

        //Act
        var compareResult = typedValue1.CompareTo(typedValue2);

        //Assert
        Assert.Equal(expectedResult, compareResult);
    }
}