using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

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

    [Fact]
    public void With_Should_ReturnGuardWithSameArgument()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);
        var argumentGuard = Guard.Argument(value);

        // Act
        var guardWithException = argumentGuard.With<Exception>();

        // Assert
        Assert.Equal(argumentGuard.Argument, guardWithException.Argument);
    }

    [Fact]
    public void With_Should_ThrowExceptionNotRegisteredException_When_ExceptionTypeIsUnknown()
    {
        // Arrange
        DateTime? value = null;
        var argumentGuard = Guard.Argument(value);
        
        void Act()
        {
            var value = argumentGuard.With<NotSupportedException>().NotNull();
        }

        // Assert
        var exceptionNotRegisteredException = Assert.Throws<ExceptionNotRegisteredException>(Act);
        Assert.Equal("Exception System.NotSupportedException was not registered. Use ThrowHelper.Register(NotSupportedExceptionFactory).", exceptionNotRegisteredException.Message);
    }

    [Fact]
    public void With_Should_ThrowCustomException()
    {
        // Arrange
        DateTime? value = null;
        var argumentGuard = Guard.Argument(value);
        
        void Act()
        {
            ThrowHelper.Register((paramName, message) => new NullReferenceException(message));
            var value = argumentGuard.With<NullReferenceException>().NotNull();
        }

        // Assert
        var nullException = Assert.Throws<NullReferenceException>(Act);
    }
}