using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotNullTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_has_value()
    {
        // Arrange
        string? value = "value";

        // Act
        string actualValue = Guard.Argument(value).NotNull();

        // Assert
        Assert.Equal(value, actualValue);
        AssertNonNullableString(actualValue);
    }

    [Fact]
    public void NotThrow_When_nullable_struct_has_value()
    {
        // Arrange
        int? value = 0;

        // Act
        int? actualValue = Guard.Argument(value).NotNull();

        // Assert
        Assert.Equal(value, actualValue);
    }

    [Fact]
    public void NotThrow_When_struct_default_value()
    {
        // Arrange
        int value = 0;

        // Act
        int actualValue = Guard.Argument(value).NotNull();

        // Assert
        Assert.Equal(value, actualValue);
    }

    [Fact]
    public void Throw_ArgumentNullException_When_null()
    {
        // Arrange
        string? value = null;

        void Act()
        {
            string actualValue = Guard.Argument(value).NotNull();
            AssertNonNullableString(actualValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentNullException_When_nullable_struct_is_null()
    {
        // Arrange
        int? value = null;

        void Act()
        {
            int? _ = Guard.Argument(value).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentNullException_with_custom_message_When_was_used()
    {
        // Arrange
        string? value = null;
        var customMessage = "Custom message.";

        void Act()
        {
            string actualValue = Guard.Argument(value).NotNull(customMessage);
            AssertNonNullableString(actualValue);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        string? value = null;
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            string actualValue = Guard.Argument(value).Throws<CustomException>().NotNull(customMessage);
            AssertNonNullableString(actualValue);
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(customMessage, ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
    }

    private void AssertNonNullableString(string nonNullableValue)
    {
        // doesn't break build with error CS8600: Converting null literal or possible null value to non-nullable type.
        // doesn't break build with error CS8603: Possible null reference return.
        // doesn't break build with error CS8604: Possible null reference argument for parameter
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