using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotDefaultTests : UnitTestsBase
{
    [Fact]
    public void NotThrows_When_not_empty_guid()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        Guard.Argument(value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrows_When_not_default_int()
    {
        // Arrange
        var value = 1;

        // Act
        Guard.Argument(value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_empty_guid()
    {
        // Arrange
        var value = Guid.Empty;

        void Act()
        {
            Guard.Argument(value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be an empty GUID. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_default_int()
    {
        // Arrange
        var value = 0;

        void Act()
        {
            Guard.Argument(value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var value = 0;
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).NotDefault(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var value = 0;

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().NotDefault();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(value), customException.ParamName);
        Assert.Equal("Value cannot be the default value.", customException.Message);
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
}