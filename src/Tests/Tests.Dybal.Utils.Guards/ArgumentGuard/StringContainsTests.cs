using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class StringContainsTests : UnitTestsBase
{
    [Theory]
    [InlineData("abc")]
    [InlineData("abc ")]
    [InlineData(" abc")]
    [InlineData(" abc ")]
    public void Should_NotThrows_When_CollectionContainValue(string expectedValue)
    {
        // Act
        var actualValue = Guard.Argument(expectedValue).Contains("abc");

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
            Guard.Argument(value).Contains("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("\"\" has to contain \"abc\". (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainValue()
    {
        // Arrange
        var value = "abc";

        void Act()
        {
            Guard.Argument(value).Contains("d");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("\"abc\" has to contain \"d\". (Parameter 'value')", ex.Message);
    }
}