using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EnumDefinedTests : UnitTestsBase
{
    private enum SampleEnum
    {
        First = 1,
        Second = 2,
    }

    [Fact]
    public void NotThrow_When_value_is_defined()
    {
        // Arrange
        SampleEnum value = SampleEnum.First;

        // Act
        SampleEnum guardValue = Guard.Argument(value).EnumDefined();

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_not_defined()
    {
        // Arrange
        SampleEnum value = (SampleEnum)0;

        void Act()
        {
            value = Guard.Argument(value).EnumDefined();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (0) must be defined in enum 'SampleEnum'. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        SampleEnum value = (SampleEnum)0;
        var customMessage = "Custom message.";

        void Act()
        {
            value = Guard.Argument(value).EnumDefined(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void NotThrow_When_nullable_value_is_defined()
    {
        // Arrange
        SampleEnum? value = SampleEnum.Second;

        // Act
        SampleEnum guardValue = Guard.Argument(value).EnumDefined();

        // Assert
        Assert.Equal(SampleEnum.Second, guardValue);
    }

    [Fact]
    public void Throw_ArgumentException_When_nullable_value_is_not_defined()
    {
        // Arrange
        SampleEnum? value = (SampleEnum)0;

        void Act()
        {
            Guard.Argument(value).EnumDefined();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (0) must be defined in enum 'SampleEnum'. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_null()
    {
        // Arrange
        SampleEnum? value = null;

        void Act()
        {
            Guard.Argument(value).EnumDefined();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (null) must be defined in enum 'SampleEnum'. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_nullable_was_used()
    {
        // Arrange
        SampleEnum? value = null;
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).EnumDefined(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        SampleEnum value = (SampleEnum)0;
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            value = Guard.Argument(value).Throws<CustomException>().EnumDefined(customMessage);
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(customMessage, ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Fact]
    public void Throw_CustomException_When_nullable_Throws_was_used()
    {
        // Arrange
        SampleEnum? value = null;
        var customMessage = "Custom message.";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().EnumDefined(customMessage);
        }

        // Assert
        var ex = Assert.Throws<CustomException>(Act);
        Assert.Equal(customMessage, ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Flags]
    private enum SampleFlags
    {
        Read = 1,
        Write = 2,
    }

    [Fact]
    public void Throw_ArgumentException_When_combination_of_flags_is_used()
    {
        // Arrange
        // Enum.IsDefined does not understand [Flags] combinations - this is documented behaviour, not a bug.
        SampleFlags value = SampleFlags.Read | SampleFlags.Write;

        void Act()
        {
            Guard.Argument(value).EnumDefined();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'value' (Read, Write) must be defined in enum 'SampleFlags'. (Parameter 'value')", ex.Message);
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
