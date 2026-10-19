using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.MultipleArgumentGuard;

public class AtLeastOneIsNotNullOrWhiteSpaceTests : UnitTestsBase
{
    [Theory]
    [InlineData(null, "text", " ")]
    [InlineData("text", null, "")]
    [InlineData("", "\t", "text")]
    public void Accepts_any_nonblank_string_and_preserves_guard(string? first, string? second, string? third)
    {
        var original = Guard.Arguments(first, second, value3: third);

        var result = original.AtLeastOneIsNotNullOrWhiteSpace();

        Assert.Equal(original, result);
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, "", " ")]
    [InlineData("\t", "\r\n", "")]
    public void Rejects_all_blank_strings(string? first, string? second, string? third)
    {
        var exception = Assert.Throws<ArgumentException>(() => Guard.Arguments(first, second, value3: third).AtLeastOneIsNotNullOrWhiteSpace());

        Assert.Equal("first, second, third", exception.ParamName);
    }

    [Fact]
    public void Rejects_nonstring_even_when_another_value_is_nonblank()
    {
        var first = "text";
        var second = 1;

        var exception = Assert.Throws<ArgumentException>(() => Guard.Arguments(first, second).AtLeastOneIsNotNullOrWhiteSpace());

        Assert.Equal("first, second", exception.ParamName);
    }

    [Fact]
    public void Preserves_override_and_custom_message_for_blank_strings()
    {
        RegisterPolicyException();
        string? first = null;
        var second = " ";

        var exception = Assert.Throws<PolicyException>(() => Guard.Arguments(first, second).Throws<PolicyException>().AtLeastOneIsNotNullOrWhiteSpace("Message or attachment required."));

        Assert.Equal("first, second", exception.ParamName);
        Assert.Equal("Message or attachment required.", exception.Message);
    }

    [Fact]
    public void Preserves_override_for_invalid_argument_type()
    {
        RegisterPolicyException();
        var first = "text";
        var second = 1;

        var exception = Assert.Throws<PolicyException>(() => Guard.Arguments(first, second).Throws<PolicyException>().AtLeastOneIsNotNullOrWhiteSpace());

        Assert.Equal("first, second", exception.ParamName);
    }

    private static void RegisterPolicyException() => ThrowHelper.TryRegister((name, message) => new PolicyException(name, message));

    private sealed class PolicyException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }
}
