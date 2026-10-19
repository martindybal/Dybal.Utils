using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class CollectionInferenceTests : UnitTestsBase
{
    [Fact]
    public void NotNullOrEmpty_infers_array_item_type()
    {
        var value = new[] { 1, 2 };

        var result = Guard.Argument(value).NotNullOrEmpty();

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
    }

    [Fact]
    public void NotNullOrEmpty_infers_list_item_type()
    {
        var value = new List<string> { "item" };

        var result = Guard.Argument(value).NotNullOrEmpty();

        Assert.Same(value, result.Argument.Value);
    }

    [Fact]
    public void NotNullOrEmpty_infers_nullable_sequence_item_type_and_rejects_null()
    {
        IEnumerable<int>? value = null;

        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).NotNullOrEmpty());

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void NotNullOrEmpty_preserves_exception_override_through_inference()
    {
        ThrowHelper.TryRegister((name, message) => new PolicyException(name, message));
        var value = Array.Empty<int>();

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().NotNullOrEmpty("Items required."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Items required.", exception.Message);
    }

    [Fact]
    public void NotNullOrEmpty_keeps_string_overload_resolution()
    {
        string? value = "text";

        string result = Guard.Argument(value).NotNullOrEmpty();

        Assert.Same(value, result);
    }

    private sealed class PolicyException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }
}
