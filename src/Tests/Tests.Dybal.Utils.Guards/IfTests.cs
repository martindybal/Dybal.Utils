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
    public void Should_ReturnGuardWithCorrectArgument_When_True()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(true);

        // Assert
        AssertGuard.AssertArgument(value, guard.Argument);
    }
    
    [Fact]
    public void Should_ReturnGuardWithCorrectArgument_When_False()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(false);

        // Assert
        AssertGuard.AssertArgument(value, guard.Argument);
    }

    public void MultipleIf_Should_ReturnNonActiveGuard_When_AllTrue()
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(true).If(true);

        // Assert
        Assert.True(guard.IsActive);
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public void MultipleIf_Should_ReturnNonActiveGuard_When_AnyFalse(bool firstCondition, bool secondCondition)
    {
        // Arrange
        object? value = null;

        // Act
        var guard = Guard.Argument(value).If(firstCondition).If(secondCondition);

        // Assert
        Assert.False(guard.IsActive);
    }
}