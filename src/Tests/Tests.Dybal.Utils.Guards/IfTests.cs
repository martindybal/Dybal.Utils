using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class IfTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnActiveGuard_When_True()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(true);

        // Assert
        Assert.True(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnNonActiveGuard_When_False()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(false);

        // Assert
        Assert.False(guard.IsActive);
    }
    
    [Fact]
    public void Should_ReturnGuardWithArgumentName_When_True()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(true);

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue_When_True()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(true);

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }
    
    [Fact]
    public void Should_ReturnGuardWithArgumentName_When_False()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(false);

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue_When_False()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(false);

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }
}