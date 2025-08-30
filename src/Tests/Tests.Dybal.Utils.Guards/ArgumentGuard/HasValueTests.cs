using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class HasValueTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_nullable_has_value()
    {
        // Arrange
        var expectedValue = new DateTime(2009, 09, 01);
        DateTime? nullableDateTime = expectedValue;

        // Act
        var actualValue = Guard.Argument(nullableDateTime).HasValue();

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_nullable_is_null()
    {
        // Arrange
        DateTime? nullableDateTime = null;

        void Act()
        {
            Guard.Argument(nullableDateTime).HasValue();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Nullable object must have a value. (Parameter 'nullableDateTime')", ex.Message);
    }


    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        DateTime? nullableDateTime = null;
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(nullableDateTime).HasValue(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'nullableDateTime')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        DateTime? nullableDateTime = null;

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(nullableDateTime).Throws<CustomException>().HasValue();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(nullableDateTime), customException.ParamName);
        Assert.Equal("Nullable object must have a value.", customException.Message);
    }

    class CustomException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }
}