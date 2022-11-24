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
        Assert.Equal(nameof(value), guard.ArgumentName);
        Assert.Equal(value, guard.ArgumentValue);
        AssertGuard.AssertArgument(value, guard.ArgumentValue, guard.ArgumentName);
    }
}