using Dybal.Utils.Guards;
using Dybal.Utils.Guards.ObjectExtensions;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ObjectExtensions;

[Trait("Category", "Unit")]
public class GuardObjectExtensionTests
{
    [Fact]
    public void Return_ArgumentGuard_with_correct_argument_name()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        ArgumentGuard<DateTime?> guard = value.Guard();

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Return_ArgumentGuard_with_correct_argument_value()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = value.Guard();

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }
}