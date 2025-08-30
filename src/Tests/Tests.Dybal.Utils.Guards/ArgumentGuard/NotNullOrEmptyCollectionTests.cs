using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotNullOrEmptyCollectionTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_collection_is_not_empty()
    {
        // Arrange
        IEnumerable<string>? value = new[] { "item" };

        // Act
        ArgumentGuardExtensions.NotNullOrEmpty<IEnumerable<string>, string>(Guard.Argument<IEnumerable<string>?>(value));

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_null()
    {
        // Arrange
        IEnumerable<string>? value = null;

        void Act()
        {
            ArgumentGuardExtensions.NotNullOrEmpty<IEnumerable<string>, string>(Guard.Argument<IEnumerable<string>?>(value));
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot be null or empty. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_is_empty()
    {
        // Arrange
        IEnumerable<string>? value = Array.Empty<string>();

        void Act()
        {
            ArgumentGuardExtensions.NotNullOrEmpty<IEnumerable<string>, string>(Guard.Argument<IEnumerable<string>?>(value));
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot be null or empty. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        IEnumerable<string>? value = Array.Empty<string>();
        var customMessage = "Custom message.";

        void Act()
        {
            ArgumentGuardExtensions.NotNullOrEmpty<IEnumerable<string>, string>(Guard.Argument<IEnumerable<string>?>(value), customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        IEnumerable<string>? value = Array.Empty<string>();
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument<IEnumerable<string>?>(value)
                .Throws<CustomException>()
                .NotNullOrEmpty<IEnumerable<string>, string>(customMessage);
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
