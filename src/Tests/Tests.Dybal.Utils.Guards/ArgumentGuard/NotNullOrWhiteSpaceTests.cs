using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotNullOrWhiteSpaceTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_non_empty_string()
    {
        // Arrange
        var value = "non-empty";

        // Act
        Guard.Argument(value).NotNullOrWhiteSpace();

        // Assert
        // doesn't throw any exception
    }
    
    [Fact]
    public void Throw_ArgumentException_When_value_is_null()
    {
        // Arrange
        string? value = null;

        void Act()
        {
            Guard.Argument(value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'value')", ex.Message);
    }
    
    [Fact]
    public void Throw_ArgumentException_When_value_is_empty_string()
    {
        // Arrange
        string value = string.Empty;

        void Act()
        {
            Guard.Argument(value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void Throw_ArgumentException_When_value_is_white_space(string value)
    {
        void Act()
        {
            Guard.Argument(value).NotNullOrWhiteSpace();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or white space string. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        string value = string.Empty;
        var customMessage = "Custom message.";

        void Act()
        {
            value = Guard.Argument(value).NotNullOrEmpty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        string value = string.Empty;
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().NotNullOrEmpty(customMessage);
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