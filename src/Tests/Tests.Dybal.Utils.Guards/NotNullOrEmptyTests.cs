using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotNullOrEmptyTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_VariableWithValue()
    {
        // Arrange
        var value = "non-empty";
        
        // Act
        Guard.Argument(value).NotNullOrEmpty();
        
        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = "non-empty" };

        // Act
        Guard.Argument(sample.Value).NotNullOrEmpty();

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
            Guard.Argument(value).NotNullOrEmpty(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or empty string. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyNull()
    {
        var sample = new { Value = (string?)null };

        void Act()
        {
            Guard.Argument(sample.Value).NotNullOrEmpty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or empty string. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableEmpty()
    {
        string value = string.Empty;

        void Act()
        {
            Guard.Argument(value).NotNullOrEmpty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or empty string. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyEmpty()
    {
        var sample = new { Value = string.Empty };

        void Act()
        {
            Guard.Argument(sample.Value).NotNullOrEmpty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or empty string. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_GuardIsNotActive()
    {
        // Arrange
        string? value = null;

        // Act
        Guard.Argument(value).If(false).NotNullOrEmpty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuardIsActive()
    {
        // Arrange
        string? value = null;

        void Act()
        {
            Guard.Argument(value).If(true).NotNullOrEmpty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or empty string. (Parameter 'value')", ex.Message);
    }
}