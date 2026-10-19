using System.Collections;
using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class CollectionConstraintTests : UnitTestsBase
{
    [Fact]
    public void Unique_infers_array_item_type_and_preserves_original_value()
    {
        var value = new[] { 1, 2, 3 };

        var result = Guard.Argument(value).Unique();

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
    }

    [Fact]
    public void Unique_accepts_empty_collection()
    {
        var value = Array.Empty<int>();

        Assert.Same(value, Guard.Argument(value).Unique().Argument.Value);
    }

    [Fact]
    public void Unique_enumerates_source_once()
    {
        var value = new SinglePassEnumerable<int>([1, 2, 3]);

        var result = Guard.Argument(value).Unique();

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(1, value.EnumerationCount);
    }

    [Fact]
    public void Unique_uses_custom_comparer()
    {
        var value = new[] { "Key", "KEY" };

        Guard.Argument(value).Unique(StringComparer.Ordinal);
        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Unique(StringComparer.OrdinalIgnoreCase));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void Unique_rejects_repeated_null_items()
    {
        var value = new string?[] { null, "text", null };

        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).Unique());

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void Unique_rejects_null_collection()
    {
        IEnumerable<int>? value = null;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).Unique());

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void Unique_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = new[] { 1, 1 };

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().Unique(message: "Duplicates are forbidden."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Duplicates are forbidden.", exception.Message);
    }

    [Fact]
    public void Unique_preserving_overload_returns_concrete_collection()
    {
        var value = new List<int> { 1, 2 };

        List<int> result = Guard.Argument(value).Unique<List<int>, int>();

        Assert.Same(value, result);
    }

    [Fact]
    public void UniqueBy_infers_item_and_key_types_and_calls_selector_once_per_item()
    {
        var value = new SinglePassEnumerable<NamedItem>([new("first"), new("second")]);
        var selectorCalls = 0;

        var result = Guard.Argument(value).UniqueBy(item =>
        {
            selectorCalls++;
            return item.Name;
        });

        Assert.Same(value, result.Argument.Value);
        Assert.Equal(nameof(value), result.Argument.Name);
        Assert.Equal(1, value.EnumerationCount);
        Assert.Equal(2, selectorCalls);
    }

    [Fact]
    public void UniqueBy_rejects_duplicate_keys_with_custom_comparer()
    {
        var value = new[] { new NamedItem("A"), new NamedItem("a") };

        Guard.Argument(value).UniqueBy(item => item.Name, StringComparer.Ordinal);
        var exception = Assert.Throws<ArgumentException>(() => Guard.Argument(value).UniqueBy(item => item.Name, StringComparer.OrdinalIgnoreCase));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void UniqueBy_rejects_repeated_null_keys()
    {
        var value = new[] { new NamedItem(null), new NamedItem(null) };

        Assert.Throws<ArgumentException>(() => Guard.Argument(value).UniqueBy(item => item.Name));
    }

    [Fact]
    public void UniqueBy_accepts_empty_collection_without_invoking_selector()
    {
        var value = Array.Empty<NamedItem>();

        var result = Guard.Argument(value).UniqueBy<NamedItem, string>(_ => throw new InvalidOperationException());

        Assert.Same(value, result.Argument.Value);
    }

    [Fact]
    public void UniqueBy_rejects_null_collection()
    {
        IEnumerable<NamedItem>? value = null;

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).UniqueBy(item => item.Name));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void UniqueBy_rejects_null_selector_even_for_empty_collection()
    {
        var value = Array.Empty<NamedItem>();

        var exception = Assert.Throws<ArgumentNullException>(() => Guard.Argument(value).UniqueBy<NamedItem, string>(null!));

        Assert.Equal("keySelector", exception.ParamName);
    }

    [Fact]
    public void UniqueBy_preserves_override_and_custom_message()
    {
        RegisterPolicyException();
        var value = new[] { new NamedItem("same"), new NamedItem("same") };

        var exception = Assert.Throws<PolicyException>(() => Guard.Argument(value).Throws<PolicyException>().UniqueBy(item => item.Name, message: "Keys must be distinct."));

        Assert.Equal(nameof(value), exception.ParamName);
        Assert.Equal("Keys must be distinct.", exception.Message);
    }

    [Fact]
    public void UniqueBy_preserving_overload_returns_concrete_collection()
    {
        var value = new List<NamedItem> { new("first"), new("second") };

        List<NamedItem> result = Guard.Argument(value).UniqueBy<List<NamedItem>, NamedItem, string?>(item => item.Name);

        Assert.Same(value, result);
    }

    private static void RegisterPolicyException() => ThrowHelper.TryRegister((name, message) => new PolicyException(name, message));

    private sealed class PolicyException(string paramName, string? message) : Exception(message)
    {
        public string ParamName { get; } = paramName;
    }

    private sealed record NamedItem(string? Name);

    private sealed class SinglePassEnumerable<T>(IEnumerable<T> values) : IEnumerable<T>
    {
        public int EnumerationCount { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            EnumerationCount++;
            if (EnumerationCount > 1)
            {
                throw new InvalidOperationException("Source was enumerated more than once.");
            }

            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
