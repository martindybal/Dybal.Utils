using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class FalseTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_false()
    {
        // Arrange
        var value = false;

        // Act
        var actual = Guard.Argument(value).False();

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void Throw_ArgumentException_When_true()
    {
        // Arrange
        var value = true;

        void Act()
        {
            Guard.Argument(value).False();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be false. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_true()
    {
        // Arrange
        var value = true;
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).False(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void NotThrow_When_nullable_false()
    {
        // Arrange
        bool? value = false;

        // Act
        var actual = Guard.Argument(value).False();

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void Throw_ArgumentException_When_nullable_true()
    {
        // Arrange
        bool? value = true;

        void Act()
        {
            Guard.Argument(value).False();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be false. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_null()
    {
        // Arrange
        bool? value = null;

        void Act()
        {
            Guard.Argument(value).False();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be false. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_nullable_true()
    {
        // Arrange
        bool? value = true;
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).False(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var value = true;

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().False();
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal("Value must be false.", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_nullable_Throws_was_used()
    {
        // Arrange
        bool? value = true;

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().False();
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal("Value must be false.", ex.Message);
    }

    class CustomException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }
}
