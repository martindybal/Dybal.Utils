using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EqualToTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_ValueIsEqual()
    {
        // Arrange
        int value = 1;

        // Act
        int guardValue = Guard.Argument(value).EqualTo(1);

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
            value = Guard.Argument(value).EqualTo(2);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must be equal to value of parameter '2' (2). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_ValueIsGreater()
    {
        // Arrange
        int value = 2;
        
        void Act()
        {
            var lessValue = 1;
            value = Guard.Argument(value).EqualTo(lessValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (2) must be equal to value of parameter 'lessValue' (1). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_WithCustomMessage_When_ValueIsGreaterAndCustomMessageWasUsed()
    {
        // Arrange
        int value = 2;
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            value = Guard.Argument(value).EqualTo(1, customMessage);
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

        // Act

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().EqualTo(1, customMessage);
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