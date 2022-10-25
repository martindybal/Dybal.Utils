using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class StartsWithTests : UnitTestsBase
{
    [Theory]
    [InlineData("abc")]
    [InlineData("abcdef")]
    [InlineData("abc ")]
    public void Should_NotThrows_When_CollectionContainValue(string expectedValue)
    {
        // Act
        var actualValue = Guard.Argument(expectedValue).StartsWith("abc");

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("Abc")]
    [InlineData("ABC")]
    public void Should_NotThrows_When_CollectionContainValueIgnoreCase(string expectedValue)
    {
        // Act
        var actualValue = Guard.Argument(expectedValue).StartsWith("abc", StringComparison.CurrentCultureIgnoreCase);

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
            Guard.Argument(value).StartsWith("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"\" has to starts with \"abc\". (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData(" abc")]
    [InlineData("a b c")]
    [InlineData("_abc ")]
    [InlineData("xyz abc ")]
    [InlineData("xyz")]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainValue(string value)
    {
        // Arrange
        void Act()
        {
            Guard.Argument(value).StartsWith("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"{value}\" has to starts with \"abc\". (Parameter 'value')", ex.Message);
    }
}