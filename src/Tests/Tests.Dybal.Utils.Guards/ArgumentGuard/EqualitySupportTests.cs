using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class EqualitySupportTests : UnitTestsBase
{
    [Fact]
    public void Compare_enum_values_Without_IEquatable_constraint()
    {
        var value = DayOfWeek.Monday;

        DayOfWeek equalValue = Guard.Argument(value).EqualTo(DayOfWeek.Monday);
        DayOfWeek differentValue = Guard.Argument(value).NotEqualTo(DayOfWeek.Tuesday);

        Assert.Equal(value, equalValue);
        Assert.Equal(value, differentValue);
        var equalException = Assert.Throws<ArgumentException>(() => Guard.Argument(value).EqualTo(DayOfWeek.Tuesday));
        var differentException = Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotEqualTo(DayOfWeek.Monday));
        Assert.Equal(nameof(value), equalException.ParamName);
        Assert.Equal(nameof(value), differentException.ParamName);
    }

    [Fact]
    public void Compare_undefined_enum_values_Without_validating_definition()
    {
        var value = (DayOfWeek)99;

        DayOfWeek actual = Guard.Argument(value).EqualTo((DayOfWeek)99).NotEqualTo(DayOfWeek.Monday);

        Assert.Equal(value, actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(1)]
    public void Compare_nullable_values_Including_null(int? value)
    {
        int? equalValue = Guard.Argument(value).EqualTo(value);
        int? differentValue = Guard.Argument(value).NotEqualTo(2);

        Assert.Equal(value, equalValue);
        Assert.Equal(value, differentValue);
        Assert.Throws<ArgumentException>(() => Guard.Argument(value).EqualTo(2));
        Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotEqualTo(value));
    }

    [Theory]
    [InlineData(null)]
    [InlineData(DayOfWeek.Monday)]
    public void Compare_nullable_enum_values(DayOfWeek? value)
    {
        DayOfWeek? actual = Guard.Argument(value).EqualTo(value).NotEqualTo(DayOfWeek.Tuesday);

        Assert.Equal(value, actual);
    }

    [Fact]
    public void Compare_null_references_Without_dereferencing_them()
    {
        string? value = null;

        string? actual = Guard.Argument(value).EqualTo(null).NotEqualTo("other");

        Assert.Null(actual);
        var equalException = Assert.Throws<ArgumentException>(() => Guard.Argument(value).EqualTo("other"));
        var differentException = Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotEqualTo(null));
        Assert.Equal(nameof(value), equalException.ParamName);
        Assert.Equal(nameof(value), differentException.ParamName);
    }

    [Fact]
    public void Preserve_custom_IEquatable_semantics()
    {
        var value = new CaseInsensitiveValue("abc");

        var guard = Guard.Argument(value).EqualTo(new CaseInsensitiveValue("ABC")).NotEqualTo(new CaseInsensitiveValue("def"));

        Assert.Same(value, guard.Argument.Value);
        Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotEqualTo(new CaseInsensitiveValue("ABC")));
    }

    [Fact]
    public void Use_object_equality_For_types_without_IEquatable()
    {
        var value = new ObjectEqualityValue(1);

        var guard = Guard.Argument(value).EqualTo(new ObjectEqualityValue(1)).NotEqualTo(new ObjectEqualityValue(2));

        Assert.Same(value, guard.Argument.Value);
    }

    [Fact]
    public void Preserve_double_NaN_equality_semantics()
    {
        double value = double.NaN;

        double actual = Guard.Argument(value).EqualTo(double.NaN).NotEqualTo(0d);

        Assert.True(double.IsNaN(actual));
        Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotEqualTo(double.NaN));
    }

    [Fact]
    public void Preserve_custom_exception_and_message_For_newly_supported_types()
    {
        ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
        var enumValue = DayOfWeek.Monday;
        string? nullValue = null;
        const string customMessage = "Custom message.";

        var equalException = Assert.Throws<CustomException>(() => Guard.Argument(enumValue).Throws<CustomException>().EqualTo(DayOfWeek.Tuesday, customMessage));
        var differentException = Assert.Throws<CustomException>(() => Guard.Argument(nullValue).Throws<CustomException>().NotEqualTo(null, customMessage));

        Assert.Equal(nameof(enumValue), equalException.ParamName);
        Assert.Equal(nameof(nullValue), differentException.ParamName);
        Assert.Equal(customMessage, equalException.Message);
        Assert.Equal(customMessage, differentException.Message);
    }

    sealed class CaseInsensitiveValue : IEquatable<CaseInsensitiveValue>
    {
        private readonly string value;

        public CaseInsensitiveValue(string value)
        {
            this.value = value;
        }

        public bool Equals(CaseInsensitiveValue? other) => other is not null && StringComparer.OrdinalIgnoreCase.Equals(value, other.value);
    }

    sealed class ObjectEqualityValue
    {
        private readonly int value;

        public ObjectEqualityValue(int value)
        {
            this.value = value;
        }

        public override bool Equals(object? obj) => obj is ObjectEqualityValue other && value == other.value;

        public override int GetHashCode() => value.GetHashCode();
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
