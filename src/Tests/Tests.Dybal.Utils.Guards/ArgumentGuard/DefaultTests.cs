using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class DefaultTests : UnitTestsBase
{
    [Fact]
    public void NotThrows_When_empty_guid()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        Guard.Argument(value).Default();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrows_When_zero()
    {
        // Arrange
        var value = 0;

        // Act
        Guard.Argument(value).Default();

        // Assert
        // doesn't throw any exception
    }
    
    [Fact]
    public void Throw_ArgumentException_When_not_empty_guid()
    {
        // Arrange
        var value = Guid.NewGuid();

        void Act()
        {
            Guard.Argument(value).Default();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be an empty GUID. (Parameter 'value')", ex.Message);
    }
    
    [Fact]
    public void Throw_ArgumentException_When_not_default_int()
    {
        // Arrange
        var value = 1;

        void Act()
        {
            Guard.Argument(value).Default();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be a default value. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var value = 1;
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).Default(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var value = 1;

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().Default();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(value), customException.ParamName);
        Assert.Equal("Value must be a default value.", customException.Message);
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