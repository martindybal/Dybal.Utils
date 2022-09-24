using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NullIfTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_VariableNullConditionFalse()
    {
        // Arrange
        object value = "value";

        // Act
        Guard.NullIf(value, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_VariableNull()
    {
        // Arrange
        object? value = null;

        // Act
        Guard.NullIf(value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableStructVariableNull()
    {
        // Arrange
        int? value = null;

        // Act
        Guard.NullIf(value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyNull()
    {
        // Arrange
        var sample = new { Value = (object?)null };

        // Act
        Guard.NullIf(sample.Value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableWithValue()
    {
        // Arrange
        object value = "value";

        void Act()
        {
            Guard.NullIf(value, true);
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
            Guard.NullIf(value, true);
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
            Guard.NullIf(sample.Value, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'sample.Value')", ex.Message);
    }
}