using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class UtcTests : UnitTestsBase
{
    [Theory]
    [InlineData(0L)]
    [InlineData(638000000000000000L)]
    public void NotThrow_When_kind_is_utc_Including_minimum_ticks(long ticks)
    {
        var value = new DateTime(ticks, DateTimeKind.Utc);

        var guard = Guard.Argument(value).Utc();

        AssertGuard.AssertArgument(value, guard.Argument);
        Assert.Equal(DateTimeKind.Utc, guard.Argument.Value.Kind);
    }

    [Theory]
    [InlineData(DateTimeKind.Local)]
    [InlineData(DateTimeKind.Unspecified)]
    public void Throw_ArgumentException_When_kind_is_not_utc(DateTimeKind kind)
    {
        var value = new DateTime(638000000000000000L, kind);

        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Utc());

        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal($"Kind of parameter 'value' must be Utc but was {kind}. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_When_value_is_default()
    {
        DateTime value = default;

        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Utc());

        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Fact]
    public void Throw_CustomException_with_custom_message_When_Throws_was_used()
    {
        ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
        DateTime value = default;
        const string customMessage = "Custom message.";

        var ex = Assert.Throws<CustomException>(() => Guard.Argument(value).Throws<CustomException>().Utc(customMessage));

        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal(customMessage, ex.Message);
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
