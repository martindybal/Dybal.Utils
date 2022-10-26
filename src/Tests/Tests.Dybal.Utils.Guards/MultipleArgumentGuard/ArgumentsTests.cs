using Dybal.Utils.Guards;
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

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments4()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments5()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4, value5);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments6()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4, value5, value6);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments7()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4, value5, value6, value7);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments8()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;
        int value8 = 8;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, value8);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
        AssertGuard.AssertArgument(value8, guard.Arguments[7]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments9()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;
        int value8 = 8;
        int value9 = 9;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, value8, value9);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
        AssertGuard.AssertArgument(value8, guard.Arguments[7]);
        AssertGuard.AssertArgument(value9, guard.Arguments[8]);
    }

    [Fact]
    public void Should_ReturnGuardWithCorrectArguments10()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;
        int value8 = 8;
        int value9 = 9;
        int value10 = 10;

        // Act
        var guard = Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
        AssertGuard.AssertArgument(value8, guard.Arguments[7]);
        AssertGuard.AssertArgument(value9, guard.Arguments[8]);
        AssertGuard.AssertArgument(value10, guard.Arguments[9]);
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
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));

            guard.Throws<CustomException>().AtLeastOneIsNotNull();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal("value1, value2, value3", customException.ParamName);
        Assert.Equal("Some of arguments must be not null.", customException.Message);
    }
}