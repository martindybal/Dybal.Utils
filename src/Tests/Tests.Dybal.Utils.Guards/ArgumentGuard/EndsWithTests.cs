using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EndsWithTests : UnitTestsBase
{
    [Theory]
    [InlineData("abc")]
    [InlineData("defabc")]
    [InlineData(" abc")]
    public void NotThrow_When_value_ends_with_abc(string value)
    {
        // Act
        var actualValue = Guard.Argument(value).EndsWith("abc");

        // Assert
        Assert.Equal(value, actualValue);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("Abc")]
    [InlineData("ABC")]
    public void NotThrow_When_value_ends_with_abc_with_ignore_case(string expectedValue)
    {
        // Act
        var actualValue = Guard.Argument(expectedValue).EndsWith("abc", StringComparison.CurrentCultureIgnoreCase);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_empty_string()
    {
        // Arrange
        var value = string.Empty;

        void Act()
        {
            Guard.Argument(value).EndsWith("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"\" has to end with \"abc\". (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData("abc ")]
    [InlineData("a b c")]
    [InlineData("abc_")]
    [InlineData(" abc xyz")]
    [InlineData("xyz")]
    [InlineData("Abc")]
    [InlineData("ABC")]
    public void Throw_ArgumentException_When_value_does_not_end_with_abc(string value)
    {
        // Arrange
        void Act()
        {
            Guard.Argument(value).EndsWith("abc");
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"{value}\" has to end with \"abc\". (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var value = "value";
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).EndsWith("abc", customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var value = "value";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().EndsWith("abc");
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(value), customException.ParamName);
        Assert.Equal($"\"{value}\" has to end with \"abc\".", customException.Message);
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