using System.Text.RegularExpressions;
using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class MatchesPatternTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_matches_pattern()
    {
        var value = "abc";

        var actualValue = Guard.Argument(value).MatchesPattern("^abc$");

        Assert.Equal(value, actualValue);
    }

    [Theory]
    [InlineData("ABC")]
    [InlineData("Abc")]
    [InlineData("aBC")]
    public void NotThrow_When_value_matches_pattern_ignore_case(string value)
    {
        var actualValue = Guard.Argument(value).MatchesPattern("^abc$", RegexOptions.IgnoreCase);

        Assert.Equal(value, actualValue);
    }

    [Theory]
    [InlineData("abd")]
    [InlineData("ab")]
    [InlineData("abcd")]
    public void Throw_ArgumentException_When_value_does_not_match_pattern(string value)
    {
        void Act()
        {
            Guard.Argument(value).MatchesPattern("^abc$");
        }

        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"\"{value}\" has to match pattern \"^abc$\". (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentException_with_custom_message_When_value_does_not_match_pattern()
    {
        var value = "abd";
        var customMessage = "Custom message.";

        void Act()
        {
            Guard.Argument(value).MatchesPattern("^abc$", message: customMessage);
        }

        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"{customMessage} (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_CustomException_When_Throws_was_used_and_value_does_not_match_pattern()
    {
        var value = "abd";

        void Act()
        {
            ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(value).Throws<CustomException>().MatchesPattern("^abc$");
        }

        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal("value", customException.ParamName);
        Assert.Equal("\"abd\" has to match pattern \"^abc$\".", customException.Message);
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
