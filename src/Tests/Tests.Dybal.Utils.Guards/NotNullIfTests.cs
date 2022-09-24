using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotNullIfTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_VariableNullConditionFalse()
    {
        // Arrange
        object? value = null;

        // Act
        Guard.NotNullIf(value, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_VariableHasValue()
    {
        // Arrange
        object value = "value";

        // Act
        Guard.NotNullIf(value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableStructVariableHasValue()
    {
        // Arrange
        int? value = 0;

        // Act
        Guard.NotNullIf(value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyHasValue()
    {
        // Arrange
        var sample = new { Value = (object?)"value" };

        // Act
        Guard.NotNullIf(sample.Value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_VariableWithoutValue()
    {
        // Arrange
        object? value = null;
            
        void Act()
        {
            Guard.NotNullIf(value, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_NullableStructVariableWithoutValue()
    {
        // Arrange
        int? value = null;
            
        void Act()
        {
            Guard.NotNullIf(value, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_PropertyWithoutValue()
    {
        // Arrange
        var sample = new { Value = (object?)null };
            
        void Act()
        {
            Guard.NotNullIf(sample.Value, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'sample.Value')", ex.Message);
    }
}