using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class MaxLengthTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_ValueHasFewerCharacters()
    {
        // Arrange
        string value = "ab";

        // Act
        Guard.Argument(value).MaxLength(3);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_ValueHasMaximumCharacters()
    {
        // Arrange
        string value = "abc";

        // Act
        Guard.Argument(value).MaxLength(3);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_ValueHasMoreCharacters()
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
    public void ShouldThrows_ArgumentException_WithCustomMessage_When_ValueHasMoreCharactersAndCustomMessageWasUsed()
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
    public void ShouldThrows_CustomException_WithCustomMessage_When_ValueHasMoreCharactersAndCustomExceptionAndMessageWasUsed()
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