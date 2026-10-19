using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ScalarConstraintTests : UnitTestsBase
{
    [Theory]
    [InlineData(16, 8)]
    [InlineData(-16, 8)]
    [InlineData(16, -8)]
    [InlineData(0, 8)]
    [InlineData(int.MinValue, -1)]
    public void DivisibleBy_accepts_divisible_values_and_preserves_value(int value, int divisor)
    {
        var result = Guard.Argument(value).DivisibleBy(divisor);

        Assert.Equal(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
    }

    [Fact]
    public void DivisibleBy_rejects_nonzero_remainder()
    {
        var value = 10;

        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).DivisibleBy(8));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void DivisibleBy_rejects_zero_divisor_with_parameter_name()
    {
        var value = 8;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Argument(value).DivisibleBy(0));

        Assert.Equal("divisor", exception.ParamName);
    }

    [Fact]
    public void DivisibleBy_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = 10;

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().DivisibleBy(8, "Whole bytes required."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Whole bytes required.", exception.Message);
    }

    [Fact]
    public void DivisibleBy_preserves_override_when_divisor_is_invalid()
    {
        RegisterPolicyException();
        var value = 8;

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().DivisibleBy(0));

        Assert.Equal("divisor", exception.ParamName);
    }

    [Fact]
    public void OneOf_preserves_original_value_and_uses_custom_comparer()
    {
        var value = "KEY";

        var result = Guard.Argument(value).OneOf(["key", "other"], StringComparer.OrdinalIgnoreCase);

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
        Assert.Throws<ArgumentException>(() => Guard.Argument(value).OneOf(["key", "other"], StringComparer.Ordinal));
    }

    [Fact]
    public void OneOf_accepts_null_when_explicitly_allowed()
    {
        string? value = null;

        var result = Guard.Argument(value).OneOf([null, "other"]);

        Assert.Null(result.Argument.Value);
    }

    [Fact]
    public void OneOf_rejects_null_when_not_allowed()
    {
        string? value = null;

        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).OneOf(["other"]));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void OneOf_rejects_empty_allowed_values()
    {
        var value = 1;

        Assert.Throws<ArgumentException>(() => Guard.Argument(value).OneOf([]));
    }

    [Fact]
    public void OneOf_rejects_null_allowed_values()
    {
        var value = 1;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).OneOf(null!));

        Assert.Equal("allowedValues", exception.ParamName);
    }

    [Fact]
    public void OneOf_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = 3;

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().OneOf([1, 2], message: "Unsupported value."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Unsupported value.", exception.Message);
    }

    [Fact]
    public void Satisfies_receives_original_value_once_and_preserves_concrete_type()
    {
        var value = new List<int> { 1, 2 };
        var calls = 0;

        List<int> result = Guard.Argument(value).Satisfies(argument =>
        {
            calls++;
            Assert.Same(value, argument);
            return argument.Count == 2;
        });

        Assert.Same(value, result);
        Assert.Equal(1, calls);
    }

    [Fact]
    public void Satisfies_allows_predicate_to_validate_null()
    {
        string? value = null;

        var result = Guard.Argument(value).Satisfies(argument => argument is null);

        Assert.Null(result.Argument.Value);
    }

    [Fact]
    public void Satisfies_rejects_null_predicate()
    {
        var value = 1;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).Satisfies(null!));

        Assert.Equal("predicate", exception.ParamName);
    }

    [Fact]
    public void Satisfies_failure_reports_original_argument_and_value()
    {
        var amount = 42;

        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(amount).Satisfies(value => value < 10));

        Assert.Equal(nameof(amount), exception.ParamName);
        Assert.Contains("42", exception.Message);
    }

    [Fact]
    public void Satisfies_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = 0;

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().Satisfies(argument => argument > 0, "Positive value required."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Positive value required.", exception.Message);
    }

    [Fact]
    public void Satisfies_propagates_predicate_exception()
    {
        var value = 1;
        var expected = new InvalidOperationException("Predicate failed.");

        var exception = Assert.Throws<InvalidOperationException>(() => Guard.Argument(value).Satisfies(_ => throw expected));

        Assert.Same(expected, exception);
    }

    private static void RegisterPolicyException() => ThrowHelper.TryRegister((name, message) => new PolicyException(name, message));

    private sealed class PolicyException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }
}
