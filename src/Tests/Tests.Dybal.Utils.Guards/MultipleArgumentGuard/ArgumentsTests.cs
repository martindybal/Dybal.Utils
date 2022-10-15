using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.MultipleArgumentGuard;

public class ArgumentsTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnActiveGuardWithCorrectArguments2()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;

        // Act
        var guard = Guard.Arguments(value1, value2);

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
        var guard = Guard.Arguments(value1, value2, value3);

        // Assert
        Assert.True(guard.IsActive);

        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
    }
}