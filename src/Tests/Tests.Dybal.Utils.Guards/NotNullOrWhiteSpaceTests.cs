using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotNullOrWhiteSpaceTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_VariableWithValue()
    {
        // Arrange
        var value = "non-empty";

        // Act
        Guard.NotNullOrWhiteSpace(value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = "non-empty" };

        // Act
        Guard.NotNullOrWhiteSpace(sample.Value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableWithoutValue()
    {
        // Arrange
        string? value = null;
            
        void Act()
        {
            Guard.NotNullOrWhiteSpace(value);
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
            Guard.NotNullOrWhiteSpace(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void ShouldThrows_ArgumentException_When_PropertyWithoutValue(string value)
    {
        // Arrange
        var sample = new { Value = value };
            
        void Act()
        {
            Guard.NotNullOrWhiteSpace(sample.Value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'sample.Value')", ex.Message);
    }
}