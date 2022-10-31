using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ContainNullTests : UnitTestsBase
{
    [Fact]
    public void NotThrows_When_collection_contain_null()
    {
        // Arrange
        var source = new int?[] { 1, 2, 3, 4, null };

        // Act
        Guard.Argument(source).ContainsNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_is_empty()
    {
        // Arrange
        var source = Array.Empty<int>();

        void Act()
        {
            Guard.Argument(source).ContainsNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_contains_only_default_value()
    {
        // Arrange
        var source = new int[] { default };

        void Act()
        {
            Guard.Argument(source).ContainsNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_does_not_contain_null()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        void Act()
        {
            Guard.Argument(source).ContainsNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).ContainsNull(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().ContainsNull();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("Collection has to contain null.", customException.Message);
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