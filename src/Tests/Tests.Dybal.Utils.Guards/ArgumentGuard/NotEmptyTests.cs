using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotEmptyTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_collection_is_not_empty()
    {
        // Arrange
        var source = new[] { "" };

        // Act
        Guard.Argument(source).NotEmpty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_When_collection_contains_null()
    {
        // Arrange
        var source = new string?[] { null };

        // Act
        Guard.Argument(source).NotEmpty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_When_collection_is_not_empty_typed()
    {
        // Arrange
        var source = new[] { "" };

        // Act
        string[] guardValue = Guard.Argument(source).NotEmpty<string[], string>();

        // Assert
        Assert.Equal(source, guardValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_is_empty()
    {
        // Arrange
        var source = Array.Empty<string>();

        // Act

        void Act()
        {
            Guard.Argument(source).NotEmpty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot be empty. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = Array.Empty<string>();
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).NotEmpty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = Array.Empty<string>();

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().NotEmpty();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("Collection cannot be empty.", customException.Message);
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