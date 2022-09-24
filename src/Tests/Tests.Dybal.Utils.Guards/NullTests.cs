using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NullTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_VariableNull()
    {
        // Arrange
        object? value = null;

        // Act
        Guard.Null(value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableStructVariableNull()
    {
        // Arrange
        int? value = null;

        // Act
        Guard.Null(value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyNull()
    {
        // Arrange
        var sample = new { Value = (object?)null };

        // Act
        Guard.Null(sample.Value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableWithValue()
    {
        // Arrange
        object? value = "value";

        void Act()
        {
            Guard.Null(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_NullableStructVariableWithValue()
    {
        // Arrange
        int? value = 0;

        void Act()
        {
            Guard.Null(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = (object?)"value" };

        void Act()
        {
            Guard.Null(sample.Value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'sample.Value')", ex.Message);
    }
}