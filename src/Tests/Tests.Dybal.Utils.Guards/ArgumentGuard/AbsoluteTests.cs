using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class AbsoluteTests : UnitTestsBase
{
    [Theory]
    [InlineData("https://example.com/path")]
    [InlineData("mailto:person@example.com")]
    [InlineData("file:///C:/example.txt")]
    public void NotThrow_When_uri_is_absolute(string uri)
    {
        Uri? value = new Uri(uri);

        ArgumentGuard<Uri> guard = Guard.Argument(value).Absolute();

        Assert.Same(value, guard.Argument.Value);
        Assert.Equal(nameof(value), guard.Argument.Name);
    }

    [Fact]
    public void Throw_ArgumentException_When_uri_is_relative()
    {
        var value = new Uri("relative/path", UriKind.Relative);

        var ex = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Absolute());

        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal("Value of parameter 'value' (relative/path) must be an absolute URI. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentNullException_When_uri_is_null()
    {
        Uri? value = null;

        var ex = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).Absolute());

        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("relative/path")]
    public void Throw_CustomException_with_custom_message_For_null_and_relative_uri(string? uri)
    {
        ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
        Uri? value = uri is null ? null : new Uri(uri, UriKind.Relative);
        const string customMessage = "Custom message.";

        var ex = Assert.Throws<CustomException>(() => Guard.Argument(value).Throws<CustomException>().Absolute(customMessage));

        Assert.Equal(nameof(value), ex.ParamName);
        Assert.Equal(customMessage, ex.Message);
    }

    [Fact]
    public void Preserve_exception_override_After_successful_uri_validation()
    {
        ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
        Uri? value = new Uri("https://example.com/");

        var ex = Assert.Throws<CustomException>(() => Guard.Argument(value).Throws<CustomException>().Absolute().Null());

        Assert.Equal(nameof(value), ex.ParamName);
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
