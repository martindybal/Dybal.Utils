using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ArgumentTests : UnitTestsBase
{
    [Fact]
    public void Return_ArgumentGuard_with_correct_argument()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value);

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
        Assert.Equal(value, guard.Argument.Value);
        AssertGuard.AssertArgument(value, guard.Argument);
    }
}