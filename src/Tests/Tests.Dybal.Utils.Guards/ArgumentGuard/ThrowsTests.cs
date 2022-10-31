using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ThrowsTests : UnitTestsBase
{
    [Fact]
    public void Throw_CustomException_When_guard_failed()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);
        var argumentGuard = Guard.Argument(value);

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            argumentGuard.Throws<CustomException>().Null();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal("value", customException.ParamName);
        Assert.Equal("Value must be null.", customException.Message);
    }

    [Fact]
    public void Throw_ExceptionNotRegisteredException_When_exception_was_not_registered()
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

#if DEBUG
    [Fact]
    public void Ensure_exception_registration_When_debug_compilation()
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
    public void Do_not_ensure_exception_registration_When_non_debug_compilation()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        Guard.Argument(value).Throws<NotSupportedException>();

        // Assert
        // doesn't throw any exception
    }
#endif

    class CustomException : Exception
    {
        public string ParamName { get; }

        public CustomException(string paramName, string? message)
            : base(message)
        {
            ParamName = paramName;
        }
    }
}