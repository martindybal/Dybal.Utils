using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotNullTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_VariableHasValue()
    {
        // Arrange
        object value = "value";

        // Act
        Guard.Argument(value).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableStructVariableHasValue()
    {
        // Arrange
        int? value = 0;

        // Act
        Guard.Argument(value).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyHasValue()
    {
        // Arrange
        var sample = new { Value = (object?)"value" };

        // Act
        Guard.Argument(sample.Value).NotNull();

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
            Guard.Argument(value).NotNull();
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
            Guard.Argument(value).NotNull();
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
            Guard.Argument(sample.Value).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_GuardIsNotActive()
    {
        // Arrange
        object? value = null;

        // Act
        Guard.Argument(value).If(false).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_GuardIsActive()
    {
        // Arrange
        object? value = null;

        void Act()
        {
            Guard.Argument(value).If(true).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }
}