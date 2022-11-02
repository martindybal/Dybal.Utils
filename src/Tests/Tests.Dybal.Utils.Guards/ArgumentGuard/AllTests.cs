using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class AllTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_all_items_match_predicate()
    {
        // Arrange
        var source = new[] { "  ", " " };

        // Act
        Guard.Argument(source).All(string.IsNullOrWhiteSpace);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrow_When_collection_is_empty()
    {
        // Arrange
        var source = Array.Empty<string>();

        // Act
        Guard.Argument(source).All(static _ => true);
        
        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_no_item_matches_predicate()
    {
        // Arrange
        var source = new[] { "a", "b" };

        void Act()
        {
            Guard.Argument(source).All(string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("All items must match the predicate. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_one_item_does_not_match_predicate()
    {
        // Arrange
        var source = new[] { "a", " " };

        void Act()
        {
            Guard.Argument(source).All(string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("All items must match the predicate. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new[] { "a", " " };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).All(string.IsNullOrWhiteSpace, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = new[] { "a", " " };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().All(string.IsNullOrWhiteSpace);
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("All items must match the predicate.", customException.Message);
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