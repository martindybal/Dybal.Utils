using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class MaxLengthTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_has_fewer_characters()
    {
        // Arrange
        string value = "ab";

        // Act
        Guard.Argument(value).MaxLength(3);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_When_value_has_maximum_characters()
    {
        // Arrange
        string value = "abc";

        // Act
        Guard.Argument(value).MaxLength(3);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_value_has_more_characters()
    {
        // Arrange
        string value = "abcD";

        // Act

        void Act()
        {
            Guard.Argument(value).MaxLength(3);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("The length of 'value' must be 3 characters or fewer. Parameter 4 has characters. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        string value = "abcD";
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            value = Guard.Argument(value).MaxLength(3, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        string value = "abcD";
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().MaxLength(3, customMessage);
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