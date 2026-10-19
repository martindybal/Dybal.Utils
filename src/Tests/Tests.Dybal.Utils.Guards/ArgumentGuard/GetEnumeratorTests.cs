using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class GetEnumeratorTests : UnitTestsBase
{
    [Fact]
    public void Enumerate_array_guard_With_foreach()
    {
        var source = new[] { 1, 2, 3 };
        var actual = new List<int>();

        foreach (var item in Guard.Argument(source))
        {
            actual.Add(item);
        }

        Assert.Equal(source, actual);
    }

    [Fact]
    public void Spread_array_guard_Into_collection_expression()
    {
        int[]? source = [1, 2, 3];

        int[] actual = [.. Guard.Argument(source).NotNull()];

        Assert.Equal(source, actual);
    }

    [Fact]
    public void Spread_concrete_collection_guard_Without_explicit_type_arguments()
    {
        var source = new List<int> { 1, 2, 3 };

        int[] actual = [.. Guard.Argument(source)];

        Assert.Equal(source, actual);
    }

    [Fact]
    public void Enumerate_interface_guard_With_foreach()
    {
        IEnumerable<int> source = new[] { 1, 2, 3 };
        var actual = new List<int>();

        foreach (var item in Guard.Argument(source))
        {
            actual.Add(item);
        }

        Assert.Equal(source, actual);
    }

    [Fact]
    public void Spread_empty_collection_To_empty_array()
    {
        var source = new List<int>();

        int[] actual = [.. Guard.Argument(source)];

        Assert.Empty(actual);
    }

    [Fact]
    public void Enumerate_lazily_And_dispose_underlying_enumerator()
    {
        var yieldedItems = 0;
        var disposed = false;
        IEnumerable<int> Source()
        {
            try
            {
                yieldedItems++;
                yield return 1;
                yieldedItems++;
                yield return 2;
            }
            finally
            {
                disposed = true;
            }
        }

        var source = Source();
        var guard = Guard.Argument(source);
        Assert.Equal(0, yieldedItems);

        foreach (var item in guard)
        {
            Assert.Equal(1, item);
            break;
        }

        Assert.Equal(1, yieldedItems);
        Assert.True(disposed);
    }

    [Fact]
    public void Throw_ArgumentNullException_When_enumerating_null_collection()
    {
        IEnumerable<int>? source = null;

        var ex = Assert.Throws<ArgumentNullException>(() => Guard.Argument(source).GetEnumerator());

        Assert.Equal(nameof(source), ex.ParamName);
    }

    [Fact]
    public void Throw_CustomException_When_enumerating_null_collection_with_override()
    {
        ThrowHelper.TryRegister((paramName, message) => new CustomException(paramName, message));
        int[]? source = null;

        var ex = Assert.Throws<CustomException>(() => Guard.Argument(source).Throws<CustomException>().GetEnumerator());

        Assert.Equal(nameof(source), ex.ParamName);
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
