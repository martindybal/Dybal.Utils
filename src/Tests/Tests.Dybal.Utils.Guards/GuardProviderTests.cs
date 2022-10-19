using Xunit;

using static Dybal.Utils.Guards.GuardProvider;

namespace Tests.Dybal.Utils.Guards;

public class GuardProviderTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnActiveGuard()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard(value);

        // Assert
        Assert.True(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentName()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard(value);

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard(value);

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }


    [Fact]
    public void Should_ReturnActiveGuardWithCorrectArguments2()
    {
        // Arrange
        string? value1 = null;
        DateTime? value2 = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard(value1, value2);

        // Assert
        Assert.True(guard.IsActive);

        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
    }

    [Fact]
    public void Should_ReturnActiveGuardWithCorrectArguments3()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 5;

        // Act
        var guard = Guard(value1, value2, value3);

        // Assert
        Assert.True(guard.IsActive);

        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
    }
}