using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class MinLengthTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_has_more_characters()
    {
        // Arrange
        string value = "abc";

        // Act
        string guardValue = Guard.Argument(value).MinLength(2);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void NotThrow_When_value_has_minimum_characters()
    {
        // Arrange
        string value = "abc";

        // Act
        string guardValue = Guard.Argument(value).MinLength(3);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_has_fewer_characters()
    {
        // Arrange
        string value = "ab";

        // Act

        void Act()
        {
            value = Guard.Argument(value).MinLength(3);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("The length of 'value' must be 3 characters or more. Parameter 2 has characters. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_has_more_characters()
    {
        // Arrange
        string value = "ab";
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            value = Guard.Argument(value).MinLength(3, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        string value = "ab";
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().MinLength(3, customMessage);
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