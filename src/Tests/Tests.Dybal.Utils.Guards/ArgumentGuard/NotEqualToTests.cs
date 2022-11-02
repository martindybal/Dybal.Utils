using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotEqualToTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_is_less()
    {
        // Arrange
        int value = 0;

        // Act
        int guardValue = Guard.Argument(value).NotEqualTo(1);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void NotThrow_When_value_is_greater()
    {
        // Arrange
        int value = 2;

        // Act
        int guardValue = Guard.Argument(value).NotEqualTo(1);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_equal()
    {
        // Arrange
        int value = 1;
        
        void Act()
        {
            value = Guard.Argument(value).NotEqualTo(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must not be equal to value of parameter 'value' (1). (Parameter 'value')", ex.Message);
    }
    
    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        int value = 1;
        var customMessage = "Custom message.";
        
        void Act()
        {
            value = Guard.Argument(value).NotEqualTo(1, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        int value = 1;
        var customMessage = "Custom message.";
        
        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().NotEqualTo(1, customMessage);
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(customMessage, ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
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