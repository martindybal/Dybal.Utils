using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class LessOrEqualThanTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_ValueIsLess()
    {
        // Arrange
        int value = 1;

        // Act
        int guardValue = Guard.Argument(value).LessOrEqualThan(2);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Should_NotThrows_When_ValueIsEqual()
    {
        // Arrange
        int value = 1;

        // Act
        int guardValue = Guard.Argument(value).LessOrEqualThan(1);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_ValueIsGreater()
    {
        // Arrange
        int value = 2;
        
        void Act()
        {
            var lessValue = 1;
            value = Guard.Argument(value).LessOrEqualThan(lessValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (2) must be less or equal than value of parameter 'lessValue' (1). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_WithCustomMessage_When_ValueIsGreaterAndCustomMessageWasUsed()
    {
        // Arrange
        int value = 2;
        var customMessage = "Custom message.";
        
        void Act()
        {
            value = Guard.Argument(value).LessOrEqualThan(1, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_CustomException_WithCustomMessage_When_ValueIsGreaterAndCustomExceptionAndMessageWasUsed()
    {
        // Arrange
        int value = 2;
        var customMessage = "Custom message.";
        
        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().LessOrEqualThan(1, customMessage);
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