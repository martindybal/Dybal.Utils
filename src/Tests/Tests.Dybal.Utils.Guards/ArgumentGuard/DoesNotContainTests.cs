using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class DoesNotContainTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_collection_does_not_contain_value()
    {
        // Arrange
        int[] source = new [] { 2, 3, 4 };

        // Act
        int[] actualSource = Guard.Argument(source).DoesNotContain(1);

        // Assert
        Assert.Equal(source, actualSource);
    }

    [Fact]
    public void NotThrow_When_collection_is_empty()
    {
        // Arrange
        var source = Array.Empty<int>();

        // Act
        Guard.Argument(source).DoesNotContain(1);

        // Assert
        // doesn't throw any exception
    }
    
    [Fact]
    public void Throw_ArgumentException_When_collection_contains_value()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };

        void Act()
        {
            Guard.Argument(source).DoesNotContain(1);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection must not contain '1'. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).DoesNotContain(1, customMessage);
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
            Guard.Argument(source).Throws<CustomException>().DoesNotContain(1);
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("Collection must not contain '1'.", customException.Message);
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