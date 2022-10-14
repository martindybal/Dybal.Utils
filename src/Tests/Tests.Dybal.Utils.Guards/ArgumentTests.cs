using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class ArgumentTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnActiveGuard()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value);

        // Assert
        Assert.True(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentName()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value);

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value);

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }
}