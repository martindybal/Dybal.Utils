using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class LessThanTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_is_less()
    {
        // Arrange
        int value = 1;

        // Act
        int guardValue = Guard.Argument(value).LessThan(2);

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
            value = Guard.Argument(value).LessThan(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must be less than value of parameter 'value' (1). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_greater()
    {
        // Arrange
        int value = 2;
        
        void Act()
        {
            var lessValue = 1;
            value = Guard.Argument(value).LessThan(lessValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (2) must be less than value of parameter 'lessValue' (1). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        int value = 2;
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            value = Guard.Argument(value).LessThan(1, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        int value = 2;
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().LessThan(1, customMessage);
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