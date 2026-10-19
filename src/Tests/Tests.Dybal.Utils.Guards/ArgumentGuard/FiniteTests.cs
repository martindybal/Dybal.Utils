using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class FiniteTests : UnitTestsBase
{
    [Theory]
    [InlineData(0d)]
    [InlineData(-1d)]
    [InlineData(double.MinValue)]
    [InlineData(double.MaxValue)]
    [InlineData(double.Epsilon)]
    public void NotThrow_When_double_is_finite(double value)
    {
        var guard = Guard.Argument(value).Finite();

        AssertGuard.AssertArgument(value, guard.Argument);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Throw_ArgumentException_When_double_is_not_finite(double value)
    {
        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Finite());

        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal($"Value of parameter 'value' ({value}) must be a finite number. (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0d)]
    [InlineData(double.MaxValue)]
    public void NotThrow_And_preserve_nullable_double_When_null_or_finite(double? value)
    {
        ArgumentGuard<double?> guard = Guard.Argument(value).Finite();

        AssertGuard.AssertArgument(value, guard.Argument);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Throw_ArgumentException_When_nullable_double_is_not_finite(double? value)
    {
        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Finite());

        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    [InlineData(float.MinValue)]
    [InlineData(float.MaxValue)]
    [InlineData(float.Epsilon)]
    public void NotThrow_When_float_is_finite(float value)
    {
        var guard = Guard.Argument(value).Finite();

        AssertGuard.AssertArgument(value, guard.Argument);
    }

    [Theory]
    [InlineData(float.NaN)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(float.NegativeInfinity)]
    public void Throw_ArgumentException_When_float_is_not_finite(float value)
    {
        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Finite());

        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal($"Value of parameter 'value' ({value}) must be a finite number. (Parameter 'value')", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0f)]
    [InlineData(float.MaxValue)]
    public void NotThrow_And_preserve_nullable_float_When_null_or_finite(float? value)
    {
        ArgumentGuard<float?> guard = Guard.Argument(value).Finite();

        AssertGuard.AssertArgument(value, guard.Argument);
    }

    [Theory]
    [InlineData(float.NaN)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(float.NegativeInfinity)]
    public void Throw_ArgumentException_When_nullable_float_is_not_finite(float? value)
    {
        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Finite());

        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Fact]
    public void Throw_CustomException_with_custom_message_For_all_overloads()
    {
        ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
        double doubleValue = double.NaN;
        double? nullableDoubleValue = double.PositiveInfinity;
        float floatValue = float.NaN;
        float? nullableFloatValue = float.NegativeInfinity;
        const string customMessage = "Custom message.";

        var doubleException = Assert.Throws<CustomException>(() => Guard.Argument(doubleValue).Throws<CustomException>().Finite(customMessage));
        var nullableDoubleException = Assert.Throws<CustomException>(() => Guard.Argument(nullableDoubleValue).Throws<CustomException>().Finite(customMessage));
        var floatException = Assert.Throws<CustomException>(() => Guard.Argument(floatValue).Throws<CustomException>().Finite(customMessage));
        var nullableFloatException = Assert.Throws<CustomException>(() => Guard.Argument(nullableFloatValue).Throws<CustomException>().Finite(customMessage));

        Assert.Equal(nameof(doubleValue), doubleException.ParamName);
        Assert.Equal(nameof(nullableDoubleValue), nullableDoubleException.ParamName);
        Assert.Equal(nameof(floatValue), floatException.ParamName);
        Assert.Equal(nameof(nullableFloatValue), nullableFloatException.ParamName);
        Assert.All(new[] { doubleException, nullableDoubleException, floatException, nullableFloatException }, ex => Assert.Equal(customMessage, ex.Message));
    }

    class CustomException : Exception
    {
        public string ParamName { get; }

        public CustomException(string paramName, string? message) : base(message)
        {
            ParamName = paramName;
        }
    }
}
