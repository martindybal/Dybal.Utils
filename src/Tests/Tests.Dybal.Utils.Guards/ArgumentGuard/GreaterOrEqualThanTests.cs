using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class GreaterOrEqualThanTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_ValueIsGreater()
    {
        // Arrange
        int value = 2;

        // Act
        int guardValue = Guard.Argument(value).GreaterOrEqualThan(1);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Should_NotThrows_When_ValueIsEqual()
    {
        // Arrange
        int value = 1;

        // Act
        int guardValue = Guard.Argument(value).GreaterOrEqualThan(1);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_ValueIsLess()
    {
        // Arrange
        int value = 1;
        
        void Act()
        {
            var greaterValue = 2;
            value = Guard.Argument(value).GreaterOrEqualThan(greaterValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must be greater or equal than value of parameter 'greaterValue' (2). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_WithCustomMessage_When_ValueIsLessAndCustomMessageWasUsed()
    {
        // Arrange
        int value = 1;
        var customMessage = "Custom message.";
        
        void Act()
        {
            value = Guard.Argument(value).GreaterOrEqualThan(2, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_CustomException_WithCustomMessage_When_ValueIsLessAndCustomExceptionAndMessageWasUsed()
    {
        // Arrange
        int value = 1;
        var customMessage = "Custom message.";
        
        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().GreaterOrEqualThan(2, customMessage);
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