using Dybal.Utils.Guards.Analyzers;
using Microsoft.CodeAnalysis.Testing;
using Tests.Dybal.Utils.Guards.Analyzers.Utilities;
using Xunit;

namespace Tests.Dybal.Utils.Guards.Analyzers;

public class ArgumentGuardComputedSizeUsageAnalyzerTests
{
    private static Task Verify(string body, params DiagnosticResult[] diagnostics) =>
        VerifyUtils<ArgumentGuardComputedSizeUsageAnalyzer>.Test(@"
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dybal.Utils.Guards;
using Dybal.Utils.Guards.ObjectExtensions;
using G = Dybal.Utils.Guards.Guard;
using Extensions = Dybal.Utils.Guards.ArgumentGuardExtensions;

class Program
{
    void Method(List<string> items, List<string> others, string[] array, string text, int limit, FakeSize custom)
    {
" + body + @"
    }

    object GetConfig() => new object();
    List<string> GetItems() => new List<string>();
    List<string> Items => GetItems();
}

class FakeSize
{
    public int Length => 0;
    public int Count => 0;
    public int CountItems() => 0;
}

static class OtherCounters
{
    public static int Count(IEnumerable<string> source) => 0;
    public static IEnumerable<string> Distinct(IEnumerable<string> source) => source;
    public static void EqualTo<T>(ArgumentGuard<T> guard, T value) { }
}
", diagnostics);

    private static DiagnosticResult Expected(string size, string suggestion) =>
        new DiagnosticResult(ArgumentGuardComputedSizeUsageAnalyzer.DoNotGuardComputedSizeRule)
            .WithLocation(0).WithArguments(size, suggestion);

    [Theory]
    [InlineData("items.Count", "EqualTo", 0, "Empty()")]
    [InlineData("array.Length", "NotEqualTo", 0, "NotEmpty()")]
    [InlineData("items.Count()", "GreaterThan", 0, "NotEmpty()")]
    [InlineData("Enumerable.Count(items)", "GreaterThanOrEqualTo", 1, "NotEmpty()")]
    [InlineData("items.LongCount()", "LessThan", 1, "Empty()")]
    [InlineData("array.LongLength", "LessThanOrEqualTo", 0, "Empty()")]
    public async Task Warning_For_collection_emptiness(string size, string comparison, int bound, string suggestion)
    {
        await Verify("G.Argument({|#0:" + size + "|})." + comparison + "(" + bound + ");",
            Expected(size, suggestion));
    }

    [Theory]
    [InlineData("GreaterThan", 0, "NotNullOrEmpty()")]
    [InlineData("NotEqualTo", 0, "NotNullOrEmpty()")]
    [InlineData("GreaterThanOrEqualTo", 1, "NotNullOrEmpty()")]
    [InlineData("EqualTo", 0, "MaxLength(0)")]
    [InlineData("EqualTo", 4, "MinLength(4).MaxLength(4)")]
    [InlineData("GreaterThan", 4, "MinLength(5)")]
    [InlineData("GreaterThanOrEqualTo", 4, "MinLength(4)")]
    [InlineData("LessThan", 4, "MaxLength(3)")]
    [InlineData("LessThanOrEqualTo", 4, "MaxLength(4)")]
    public async Task Warning_For_string_length(string comparison, int bound, string suggestion)
    {
        await Verify("Guard.Argument({|#0:text.Length|}, nameof(text))." + comparison + "(" + bound + ");",
            Expected("text.Length", suggestion));
    }

    [Theory]
    [InlineData("items.Distinct().Count()", "items.Count", "Unique()")]
    [InlineData("items.Distinct(StringComparer.OrdinalIgnoreCase).Count()", "items.Count", "Unique(...)")]
    [InlineData("items.DistinctBy(item => item.Length).Count()", "items.Count", "UniqueBy(...)")]
    [InlineData("items.Select(item => item.Length).Distinct().Count()", "items.Count", "UniqueBy(...)")]
    [InlineData("items.Select(item => item.ToLower()).Distinct(StringComparer.Ordinal).Count()", "items.Count", "UniqueBy(...)")]
    [InlineData("items.Distinct().LongCount()", "items.LongCount()", "Unique()")]
    [InlineData("Enumerable.Count(Enumerable.Distinct(array))", "array.Length", "Unique()")]
    [InlineData("items.Count", "items.DistinctBy(item => item.Length).Count()", "UniqueBy(...)")]
    public async Task Warning_For_collection_uniqueness(string size, string bound, string suggestion)
    {
        await Verify("Guard.Argument({|#0:" + size + "|}).EqualTo(" + bound + ");", Expected(size, suggestion));
    }

    [Fact]
    public async Task Warning_For_static_comparison_and_exception_override()
    {
        await Verify(@"Extensions.GreaterThan(Guard.Argument({|#0:items.Count|}).Throws<InvalidOperationException>(), 0);",
            Expected("items.Count", "NotEmpty()"));
    }

    [Fact]
    public async Task Warning_For_object_extension_entrypoint()
    {
        await Verify(@"{|#0:text.Length|}.Guard().LessThanOrEqualTo(4);", Expected("text.Length", "MaxLength(4)"));
    }

    [Fact]
    public async Task Warning_For_collection_interfaces()
    {
        await Verify(@"
IReadOnlyCollection<string> collection = items;
Guard.Argument({|#0:collection.Count|}).EqualTo(0);", Expected("collection.Count", "Empty()"));
    }

    [Fact]
    public async Task NoWarning_For_arbitrary_size_members_or_custom_Linq_names()
    {
        await Verify(@"
Guard.Argument(custom.Length).GreaterThan(0);
Guard.Argument(custom.Count).EqualTo(0);
Guard.Argument(custom.CountItems()).GreaterThan(0);
Guard.Argument(OtherCounters.Count(items)).GreaterThan(0);
Guard.Argument(OtherCounters.Distinct(items).Count()).EqualTo(items.Count);
OtherCounters.EqualTo(Guard.Argument(items.Count), 0);
Guard.Argument(GetConfig()).NotNull();
");
    }

    [Fact]
    public async Task NoWarning_For_unrelated_counts_predicates_or_indexed_selectors()
    {
        await Verify(@"
Guard.Argument(items.Distinct().Count()).EqualTo(others.Count);
Guard.Argument(items.Count(item => item.Length > 0)).GreaterThan(0);
Guard.Argument(items.Select((item, index) => index).Distinct().Count()).EqualTo(items.Count);
Guard.Argument(items.Count).GreaterThan(1);
Guard.Argument(text.Length).GreaterThanOrEqualTo(0);
Guard.Argument(text.Length).LessThan(0);
Guard.Argument(text.Length).LessThanOrEqualTo(-1);
Guard.Argument(text.Length).LessThan(limit);
Guard.Argument(items.Count).EqualTo(others.Count);
");
    }

    [Fact]
    public async Task NoWarning_For_explicit_numeric_conversions_that_change_size_semantics()
    {
        await Verify(@"
Guard.Argument((byte)text.Length).EqualTo((byte)0);
Guard.Argument((byte)items.Count).EqualTo((byte)0);
Guard.Argument((short)items.Distinct().Count()).EqualTo((short)items.Count);
Guard.Argument((int)array.LongLength).EqualTo(0);
");
    }

    [Fact]
    public async Task NoWarning_For_sequences_unsupported_by_generic_collection_guards()
    {
        await Verify(@"
ICollection nonGeneric = new ArrayList();
Guard.Argument(nonGeneric.Count).EqualTo(0);
ArrayList nonGenericList = new ArrayList();
Guard.Argument(nonGenericList.Count).GreaterThan(0);
int[,] matrix = new int[1, 1];
Guard.Argument(matrix.Length).EqualTo(0);
Guard.Argument(matrix.LongLength).GreaterThan(0);
");
    }

    [Fact]
    public async Task NoWarning_For_repeated_sources_that_can_return_different_collections()
    {
        await Verify(@"
Guard.Argument(GetItems().Distinct().Count()).EqualTo(GetItems().Count);
Guard.Argument(Items.Distinct().Count()).EqualTo(Items.Count);
Guard.Argument(GetItems().Select(item => item.Length).Distinct().Count()).EqualTo(GetItems().Count);
");
    }
}
