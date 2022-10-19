using Dybal.Utils.Guards;
using Newtonsoft.Json.Linq;
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
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
    }

#if DEBUG
    [Fact]
    public void Throws_Should_EnsureExceptionRegistrationInDebug()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 5;

        // Act
        void Act()
        {
            Guard.Arguments(value1, value2, value3).Throws<NotSupportedException>();
        }

        // Assert
        var exceptionNotRegisteredException = Assert.Throws<ExceptionNotRegisteredException>(Act);
        Assert.Equal("Exception System.NotSupportedException was not registered. Use ThrowHelper.Register(NotSupportedExceptionFactory).", exceptionNotRegisteredException.Message);
    }
#endif

    [Fact]
    public void Throws_Should_ThrowExceptionNotRegisteredException_When_ExceptionTypeIsUnknown()
    {
        // Arrange
        DateTime? value1 = null;
        string? value2 = null;
        int? value3 = null;
        var guard = Guard.Arguments(value1, value2, value3);

        void Act()
        {
            guard.Throws<NotSupportedException>().AtLeastOneIsNotNull();
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
        DateTime? value1 = null;
        string? value2 = null;
        int? value3 = null;
        var guard = Guard.Arguments(value1, value2, value3);

        void Act()
        {
            ThrowHelper.Register((paramName, message) => new CustomException(paramName, message));

            guard.Throws<CustomException>().AtLeastOneIsNotNull();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal("value1, value2, value3", customException.ParamName);
        Assert.Equal("Some of arguments must be not null.", customException.Message);
    }
}