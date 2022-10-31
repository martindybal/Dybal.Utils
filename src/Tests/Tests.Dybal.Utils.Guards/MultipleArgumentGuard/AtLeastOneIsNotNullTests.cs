using Dybal.Utils.Guards;
using Tests.Dybal.Utils.Guards.ArgumentGuard;
using Xunit;

namespace Tests.Dybal.Utils.Guards.MultipleArgumentGuard;

public class AtLeastOneIsNotNullTests : UnitTestsBase
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(1, null, null)]
    [InlineData("A", 1, null)]
    [InlineData("A", null, 1)]
    [InlineData("A", null, null)]
    [InlineData(null, "A", null)]
    [InlineData(null, null, "A")]
    public void NotThrow_When_at_least_one_value_is_not_null(object value1, object value2, object value3)
    {
        // Arrange
        var sample = new
        {
            Value1 = value1,
            Value2 = value2,
            Value3 = value3
        };

        // Act
        Guard.Arguments(sample.Value1, sample.Value2, sample.Value3).AtLeastOneIsNotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Throw_ArgumentException_When_all_values_are_null()
    {
        // Arrange
        var sample = new
        {
            Value1 = (int?)null,
            Value2 = (string?)null,
            Value3 = (object?)null
        };

        void Act()
        {
            Guard.Arguments(sample.Value1, sample.Value2, sample.Value3).AtLeastOneIsNotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Some of arguments must be not null. (Parameter 'sample.Value1, sample.Value2, sample.Value3')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_was_used()
    {
        // Arrange
        var sample = new
        {
            Value1 = (int?)null,
            Value2 = (string?)null,
            Value3 = (object?)null
        };
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Arguments(sample.Value1, sample.Value2, sample.Value3).AtLeastOneIsNotNull(customMessage);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'sample.Value1, sample.Value2, sample.Value3')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used()
    {
        // Arrange
        var sample = new
        {
            Value1 = (int?)null,
            Value2 = (string?)null,
            Value3 = (object?)null
        };

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Arguments(sample.Value1, sample.Value2, sample.Value3).Throws<CustomException>().AtLeastOneIsNotNull();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal("sample.Value1, sample.Value2, sample.Value3", customException.ParamName);
        Assert.Equal("Some of arguments must be not null.", customException.Message);
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