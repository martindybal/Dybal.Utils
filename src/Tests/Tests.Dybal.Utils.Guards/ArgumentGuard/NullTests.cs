using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NullTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_null()
    {
        // Arrange
        string? value = null;

        // Act
        string? actualValue = Guard.Argument(value).Null();

        // Assert
        Assert.Equal(value, actualValue);
    }

    [Fact]
    public void NotThrow_When_nullable_nullable_struct_is_null()
    {
        // Arrange
        int? value = null;

        // Act
        int? actualValue = Guard.Argument(value).Null();

        // Assert
        Assert.Equal(value, actualValue);
    }
    
    [Fact]
    public void Throw_ArgumentException_When_has_value()
    {
        // Arrange
        string? value = "value";

        void Act()
        {
            Guard.Argument(value).Null();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_nullable_struct_has_value()
    {
        // Arrange
        int? value = 0;

        void Act()
        {
            Guard.Argument(value).Null();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_struct_default_value()
    {
        // Arrange
        int value = 0;

        void Act()
        {
            Guard.Argument(value).Null();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        string? value = "value";
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).Null(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        string? value = "value";
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().Null(customMessage);
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(customMessage, ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
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