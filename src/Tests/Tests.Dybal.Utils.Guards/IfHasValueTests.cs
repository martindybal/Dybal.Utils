using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class IfHasValueTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnActiveGuard_When_ParameterHasValue()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value).IfHasValue();

        // Assert
        Assert.True(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnNonActiveGuard_When_ParameterHasNotValue()
    {
        // Arrange
        DateTime? value = null;

        // Act
        var guard = Guard.Argument(value).IfHasValue();

        // Assert
        Assert.False(guard.IsActive);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentName_When_ParameterHasValue()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value).IfHasValue();

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue_When_ParameterHasValue()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value).IfHasValue();

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentName_When_ParameterHasNotValue()
    {
        // Arrange
        DateTime? value = null;

        // Act
        var guard = Guard.Argument(value).IfHasValue();

        // Assert
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Should_ReturnGuardWithArgumentValue_When_ParameterHasNotValue()
    {
        // Arrange
        DateTime? value = null;

        // Act
        var guard = Guard.Argument(value).IfHasValue();

        // Assert
        Assert.Equal(value, guard.Argument.Value);
    }
}