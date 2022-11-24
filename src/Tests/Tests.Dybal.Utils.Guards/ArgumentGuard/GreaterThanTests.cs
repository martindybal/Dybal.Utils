using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class GreaterThanTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_is_greater()
    {
        // Arrange
        int value = 2;

        // Act
        int guardValue = Guard.Argument(value).GreaterThan(1);

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
            value = Guard.Argument(value).GreaterThan(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must be greater than value of parameter 'value' (1). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_less()
    {
        // Arrange
        int value = 1;
        
        void Act()
        {
            var lessValue = 2;
            value = Guard.Argument(value).GreaterThan(lessValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must be greater than value of parameter 'lessValue' (2). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        int value = 1;
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            value = Guard.Argument(value).GreaterThan(2, customMessage);
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

        // Act

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().GreaterThan(2, customMessage);
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