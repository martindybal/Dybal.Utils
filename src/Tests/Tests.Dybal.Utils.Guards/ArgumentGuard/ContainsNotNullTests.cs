using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ContainsNotNullTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_one_item_is_not_null()
    {
        // Arrange
        var source = new int?[] { 1, null, null };

        // Act
        Guard.Argument(source).ContainsNotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_ArgumentException_When_collection_contains_only_default_value()
    {
        // Arrange
        var source = new int[] { default };
        
        // Act
        Guard.Argument(source).ContainsNotNull();

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
            Guard.Argument(source).ContainsNotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain an item which is not null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_contains_only_null()
    {
        // Arrange
        var source = new int?[] { null };

        void Act()
        {
            Guard.Argument(source).ContainsNotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain an item which is not null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new int?[] { null };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).ContainsNotNull(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = new int?[] { null };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().ContainsNotNull();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("Collection has to contain an item which is not null.", customException.Message);
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