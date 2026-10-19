using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class StringConstraintTests : UnitTestsBase
{
    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("aAa")]
    public void ContainsOnly_accepts_allowed_characters_and_preserves_value(string value)
    {
        var result = Guard.Argument(value).ContainsOnly(['a', 'b', 'c', 'A']);

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
    }

    [Fact]
    public void ContainsOnly_rejects_character_outside_alphabet()
    {
        var value = "abC";

        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).ContainsOnly(['a', 'b', 'c']));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void ContainsOnly_accepts_empty_value_with_empty_alphabet()
    {
        var value = string.Empty;

        Assert.Equal(value, Guard.Argument(value).ContainsOnly([]).Argument.Value);
    }

    [Fact]
    public void ContainsOnly_rejects_nonempty_value_with_empty_alphabet()
    {
        var value = "a";

        Assert.Throws<ArgumentException>(() => Guard.Argument(value).ContainsOnly([]));
    }

    [Fact]
    public void ContainsOnly_rejects_null_value()
    {
        string? value = null;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).ContainsOnly(['a']));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void ContainsOnly_rejects_null_alphabet()
    {
        var value = string.Empty;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).ContainsOnly(null!));

        Assert.Equal("allowedCharacters", exception.ParamName);
    }

    [Fact]
    public void ContainsOnly_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = "invalid";

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().ContainsOnly(['a'], "Invalid alphabet."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Invalid alphabet.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("aA ")]
    public void DoesNotContainDuplicateCharacters_accepts_distinct_characters(string value)
    {
        var result = Guard.Argument(value).DoesNotContainDuplicateCharacters();

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
    }

    [Theory]
    [InlineData("aba")]
    [InlineData("  ")]
    public void DoesNotContainDuplicateCharacters_rejects_repeated_characters(string value)
    {
        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).DoesNotContainDuplicateCharacters());

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void DoesNotContainDuplicateCharacters_rejects_null_value()
    {
        string? value = null;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).DoesNotContainDuplicateCharacters());

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void DoesNotContainDuplicateCharacters_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = "aa";

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().DoesNotContainDuplicateCharacters("Repeated character."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Repeated character.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("text")]
    [InlineData(" padded ")]
    public void NotWhiteSpaceIfNotNull_accepts_optional_nonblank_text(string? value)
    {
        string? result = Guard.Argument(value).NotWhiteSpaceIfNotNull();

        Assert.Same(value, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t\r\n")]
    public void NotWhiteSpaceIfNotNull_rejects_blank_text(string value)
    {
        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotWhiteSpaceIfNotNull());

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void NotWhiteSpaceIfNotNull_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        string? value = " ";

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().NotWhiteSpaceIfNotNull("Blank text."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Blank text.", exception.Message);
    }

    [Fact]
    public void String_alphabet_guards_chain_after_standard_not_null_guard()
    {
        string? value = "abc";

        string result = Guard.Argument(value).NotNull().ContainsOnly(['a', 'b', 'c']).DoesNotContainDuplicateCharacters();

        Assert.Same(value, result);
    }

    private static void RegisterPolicyException() => ThrowHelper.TryRegister((name, message) => new PolicyException(name, message));

    private sealed class PolicyException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }
}
