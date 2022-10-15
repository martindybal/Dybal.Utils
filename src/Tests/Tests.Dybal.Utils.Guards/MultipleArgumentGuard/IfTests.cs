using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.MultipleArgumentGuard;

public class IfTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnActiveGuard_When_True()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2).If(true);

        // Assert
        Assert.True(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnNonActiveGuard_When_False()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2).If(false);

        // Assert
        Assert.False(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArgument_When_True()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2).If(true);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArgument_When_False()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2).If(false);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
    }

    [Fact]
    public void MultipleIf_Should_ReturnNonActiveGuard_When_AllTrue()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2).If(true).If(true);

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
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2).If(firstCondition).If(secondCondition);

        // Assert
        Assert.False(guard.IsActive);
    }
}