using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotNullOrWhiteSpaceTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_VariableWithValue()
    {
        // Arrange
        var value = "non-empty";

        // Act
        Guard.Argument(value).NotNullOrWhiteSpace();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = (string?)"non-empty" };

        // Act
        Guard.Argument(sample.Value).NotNullOrWhiteSpace();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableNull()
    {
        // Arrange
        string? value = null;

        void Act()
        {
            Guard.Argument(value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void ShouldThrows_ArgumentException_When_VariableWhitespace(string value)
    {
        void Act()
        {
            Guard.Argument(value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'value')", ex.Message);
    }


    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyNull()
    {
        // Arrange
        var sample = new { Value = (string?)null };

        void Act()
        {
            Guard.Argument(sample.Value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'sample.Value')", ex.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void ShouldThrows_ArgumentException_When_PropertyWhitespace(string value)
    {
        // Arrange
        var sample = new { Value = value };

        void Act()
        {
            Guard.Argument(sample.Value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'sample.Value')", ex.Message);
    }
}