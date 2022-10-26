using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EmptyTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionIsEmpty()
    {
        // Arrange
        var value = Array.Empty<string>();

        // Act
        Guard.Argument(value).Empty();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionIsEmpty_Typed()
    {
        // Arrange
        var value = Array.Empty<string>();

        // Act
        string[] guardValue = Guard.Argument(value).Empty<string[], string>();

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionIsNotEmpty()
    {
        // Arrange
        var value = new[] { "" };

        // Act

        void Act()
        {
            Guard.Argument(value).Empty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to be empty. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionContainsNull()
    {
        // Arrange
        var value = new string?[] { null };

        // Act

        void Act()
        {
            Guard.Argument(value).Empty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to be empty. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_WithCustomMessage_When_ValueHasFewerCharactersAndCustomMessageWasUsed()
    {
        // Arrange
        var value = new[] { "" };
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            Guard.Argument(value).Empty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_CustomException_WithCustomMessage_When_ValueHasFewerCharactersAndCustomExceptionAndMessageWasUsed()
    {
        // Arrange
        var value = new[] { "" };
        var customMessage = "Custom message.";

        // Act

        void Act()
        {
            ThrowHelper.Register((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().Empty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(customMessage, ex.Message);
    }

    [Fact]
    public void Should_NotThrow_When_ValueIsEmptyGuid()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        var guardedValue = Guard.Argument(value).Empty();

        // Assert
        Assert.Equal(value, guardedValue);
    }

    [Fact]
    public void Should_Throw_ArgumentException_When_ValueIsNotEmptyGuid()
    {
        // Arrange
        var value = new Guid("D9394783-85D4-401D-A01F-58443840384F");

        // Act
        void Act()
        {
            Guard.Argument(value).Empty();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be an empty GUID. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_WithCustomMessage_When_ValueIsNotEmptyGuidAndCustomMessageWasUsed()
    {
        // Arrange
        var value = new Guid("D9394783-85D4-401D-A01F-58443840384F");
        var customMessage = "Custom message.";

        // Act
        void Act()
        {
            Guard.Argument(value).Empty(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_CustomException_WithCustomMessage_When_ValueIsNotEmptyGuidAndCustomExceptionAndMessageWasUsed()
    {
        // Arrange
        var value = new Guid("D9394783-85D4-401D-A01F-58443840384F");
        var customMessage = "Custom message.";

        // Act
        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().Empty(customMessage);
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