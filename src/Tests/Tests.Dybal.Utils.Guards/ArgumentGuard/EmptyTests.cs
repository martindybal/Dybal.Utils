using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EmptyTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_collection_is_empty()
    {
        // Arrange
        var value = Array.Empty<string>();

        // Act
        Guard.Argument(value).Empty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_When_collection_is_empty_typed()
    {
        // Arrange
        var value = Array.Empty<string>();

        // Act
        string[] guardValue = Guard.Argument(value).Empty<string[], string>();

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_is_not_empty()
    {
        // Arrange
        var source = new[] { "" };

        // Act

        void Act()
        {
            Guard.Argument(source).Empty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to be empty. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_contain_null()
    {
        // Arrange
        var source = new string?[] { null };

        // Act

        void Act()
        {
            Guard.Argument(source).Empty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to be empty. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new[] { "" };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).Empty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = new[] { "" };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().Empty();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("Collection has to be empty.", customException.Message);
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