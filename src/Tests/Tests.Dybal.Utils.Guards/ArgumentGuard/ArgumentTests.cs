using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ArgumentTests : UnitTestsBase
{
    [Fact]
    public void Should_ReturnGuardWithArgument()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard.Argument(value);

        // Assert
        AssertGuard.AssertArgument(value, guard.Argument);
    }

#if DEBUG
    [Fact]
    public void Throws_Should_EnsureExceptionRegistration_When_DebugCompilation()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        void Act()
        {
            Guard.Argument(value).Throws<NotSupportedException>();
        }

        // Assert
        var exceptionNotRegisteredException = Assert.Throws<ExceptionNotRegisteredException>(Act);
        Assert.Equal("Exception System.NotSupportedException was not registered. Use ThrowHelper.Register(NotSupportedExceptionFactory).", exceptionNotRegisteredException.Message);
    }
#else
    [Fact]
    public void Throws_ShouldNot_EnsureExceptionRegistration_When_NotDebugCompilation()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        Guard.Argument(value).Throws<NotSupportedException>();

        // Assert
        // doesn't throw any exception
    }
#endif

    [Fact]
    public void Throws_Should_ThrowExceptionNotRegisteredException_When_ExceptionTypeIsUnknown()
    {
        // Arrange
        DateTime? value = null;
        var argumentGuard = Guard.Argument(value);

        void Act()
        {
            var value = argumentGuard.Throws<NotSupportedException>().NotNull();
        }

        // Assert
        var exceptionNotRegisteredException = Assert.Throws<ExceptionNotRegisteredException>(Act);
        Assert.Equal("Exception System.NotSupportedException was not registered. Use ThrowHelper.Register(NotSupportedExceptionFactory).", exceptionNotRegisteredException.Message);
    }

    class CustomException : Exception
    {
        public string ParamName { get; }

        public CustomException(string paramName, string? message)
            : base(message)
        {
            ParamName = paramName;
        }
    }

    [Fact]
    public void Throws_Should_ThrowCustomException()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);
        var argumentGuard = Guard.Argument(value);

        void Act()
        {
            ThrowHelper.Register((paramName, message) => new CustomException(paramName, message));

            var value = argumentGuard.Throws<CustomException>().Null();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal("value", customException.ParamName);
        Assert.Equal("Value must be null.", customException.Message);
    }
}