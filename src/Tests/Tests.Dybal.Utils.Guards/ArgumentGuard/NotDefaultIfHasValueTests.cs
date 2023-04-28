using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotDefaultIfHasValueTests : UnitTestsBase
{
    [Fact]
    public void NotThrows_When_null()
    {
        // Arrange
        Guid? value = null;

        // Act
        value = Guard.Argument(value).NotDefaultIfHasValue();

        // Assert
        // doesn't throw any exception
    }
    
    [Fact]
    public void NotThrows_When_not_empty_guid()
    {
        // Arrange
        Guid? value = Guid.NewGuid();

        // Act
        value = Guard.Argument(value).NotDefaultIfHasValue();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void NotThrows_When_not_default_int()
    {
        // Arrange
        int? value = 1;

        // Act
        value = Guard.Argument(value).NotDefaultIfHasValue();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_empty_guid()
    {
        // Arrange
        Guid? defaultValue = Guid.Empty;

        void Act()
        {
            defaultValue = Guard.Argument(defaultValue).NotDefaultIfHasValue();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be an empty GUID. (Parameter 'defaultValue')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_default_int()
    {
        // Arrange
        int? defaultValue = 0;

        void Act()
        {
            defaultValue = Guard.Argument(defaultValue).NotDefaultIfHasValue();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'defaultValue')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        int? defaultValue = 0;
        var customMessage = "Custom message.";

        void Act()
        {
            defaultValue = Guard.Argument(defaultValue).NotDefaultIfHasValue(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'defaultValue')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        int? defaultValue = 0;

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            defaultValue = Guard.Argument(defaultValue).Throws<CustomException>().NotDefaultIfHasValue();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(defaultValue), customException.ParamName);
        Assert.Equal("Value cannot be the default value.", customException.Message);
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