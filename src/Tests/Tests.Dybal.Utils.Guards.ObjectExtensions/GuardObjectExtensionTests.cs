using Dybal.Utils.Guards.ObjectExtensions;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ObjectExtensions;

[Trait("Category", "Unit")]
public class GuardObjectExtensionTests
{
    [Fact]
    public void Should_ReturnActiveGuard()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = value.Guard();

        // Assert
        Assert.True(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentName()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = value.Guard();

        // Assert
        Assert.Equal(nameof(value), guard.ArgumentName);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = value.Guard();

        // Assert
        Assert.Equal(value, guard.ArgumentValue);
    }
}