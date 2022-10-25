using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EndsWithTests : UnitTestsBase
{
    [Theory]
    [InlineData("abc")]
    [InlineData("defabc")]
    [InlineData(" abc")]
    public void Should_NotThrows_When_CollectionContainValue(string expectedValue)
    {
        // Act
        var actualValue = Guard.Argument(expectedValue).EndsWith("abc");

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmpty()
    {
        // Arrange
        var value = string.Empty;

        void Act()
        {
            Guard.Argument(value).EndsWith("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"\" has to ends with \"abc\". (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData("abc ")]
    [InlineData("a b c")]
    [InlineData("abc_")]
    [InlineData(" abc xyz")]
    [InlineData("xyz")]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainValue(string value)
    {
        // Arrange
        void Act()
        {
            Guard.Argument(value).EndsWith("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"{value}\" has to ends with \"abc\". (Parameter 'value')", ex.Message);
    }
}