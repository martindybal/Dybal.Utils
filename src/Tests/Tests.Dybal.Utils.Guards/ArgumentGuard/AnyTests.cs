﻿using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class AnyTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_one_item_matches_predicate()
    {
        // Arrange
        var source = new[] { "a", "b", " " };

        // Act
        Guard.Argument(source).Any(string.IsNullOrWhiteSpace);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_collection_is_empty()
    {
        // Arrange
        var source = Array.Empty<string>();

        void Act()
        {
            Guard.Argument(source).Any(static _ => false);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("No item matches the predicate. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_no_item_matches_predicate()
    {
        // Arrange
        var source = new[] { "a", "b" };

        void Act()
        {
            Guard.Argument(source).Any(string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("No item matches the predicate. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var source = new[] { "a", "b" };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(source).Any(string.IsNullOrWhiteSpace, customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var source = new[] { "a", "b" };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(source).Throws<CustomException>().Any(string.IsNullOrWhiteSpace);
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(source), customException.ParamName);
        Assert.Equal("No item matches the predicate.", customException.Message);
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