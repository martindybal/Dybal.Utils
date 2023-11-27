using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NullOrEmptyTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_null()
    {
        // Arrange
        string? value = null;

        // Act
        Guard.Argument(value).NullOrEmpty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_When_empty_string()
    {
        // Arrange
        var value = string.Empty;

        // Act
        Guard.Argument(value).NullOrEmpty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_not_null_or_empty()
    {
        // Arrange
        string? value = "non-empty";

        void Act()
        {
            Guard.Argument(value).NullOrEmpty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null or empty string. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        string? value = "non-empty";
        var customMessage = "Custom message.";

        void Act()
        {
            value = Guard.Argument(value).NullOrEmpty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        string? value = "non-empty";
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().NullOrEmpty(customMessage);
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