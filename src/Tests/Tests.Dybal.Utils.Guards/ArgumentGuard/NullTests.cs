using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NullTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_VariableNull()
    {
        // Arrange
        object? value = null;

        // Act
        Guard.Argument(value).Null();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableStructVariableNull()
    {
        // Arrange
        int? value = null;

        // Act
        Guard.Argument(value).Null();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyNull()
    {
        // Arrange
        var sample = new { Value = (object?)null };

        // Act
        Guard.Argument(sample.Value).Null();

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
            Guard.Argument(value).Null();
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
            Guard.Argument(value).Null();
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
            Guard.Argument(sample.Value).Null();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_GuardIsNotActive()
    {
        // Arrange
        object value = "value";

        // Act
        Guard.Argument(value).If(false).Null();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuardIsActive()
    {
        // Arrange
        object? value = "value";

        void Act()
        {
            Guard.Argument(value).If(true).Null();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'value')", ex.Message);
    }
}