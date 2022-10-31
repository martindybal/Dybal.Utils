using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class CollectionContainsTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_contain_value()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var sourceWithFive = Guard.Argument(source).Contains(5);

        // Assert
        Assert.Equal(source, sourceWithFive);
    }

    [Fact]
    public void Throws_ArgumentException_When_collection_is_empty()
    {
        // Arrange
        var source = Array.Empty<int>();

        void Act()
        {
            Guard.Argument(source).Contains(5);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain '5'. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throws_ArgumentException_When_collection_does_not_contain_value()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };

        void Act()
        {
            Guard.Argument(source).Contains(5);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain '5'. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).Contains(5, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().Contains(5);
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("Collection has to contain '5'.", customException.Message);
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