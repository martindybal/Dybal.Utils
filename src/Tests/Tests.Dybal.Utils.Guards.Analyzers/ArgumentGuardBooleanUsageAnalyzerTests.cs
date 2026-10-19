using Dybal.Utils.Guards.Analyzers;
using Microsoft.CodeAnalysis.Testing;
using Tests.Dybal.Utils.Guards.Analyzers.Utilities;
using Xunit;

namespace Tests.Dybal.Utils.Guards.Analyzers;

public class ArgumentGuardBooleanUsageAnalyzerTests
{
    private static Task Verify(string body, params DiagnosticResult[] diagnostics) =>
        VerifyUtils<ArgumentGuardBooleanUsageAnalyzer>.Test(@"
using System;
using Dybal.Utils.Guards;
using Dybal.Utils.Guards.ObjectExtensions;
using Extensions = Dybal.Utils.Guards.ArgumentGuardExtensions;

class Program
{
    void Method(bool flag, bool? nullableFlag, bool expected, int number, object item)
    {
" + body + @"
    }
}

static class OtherAssertions
{
    public static void EqualTo<T>(ArgumentGuard<T> guard, T value) { }
}
", diagnostics);

    private static DiagnosticResult Expected(string helper, string comparison, bool value) =>
        new DiagnosticResult(ArgumentGuardBooleanUsageAnalyzer.UseBooleanGuardRule)
            .WithLocation(0).WithArguments(helper, comparison, value ? "true" : "false");

    [Theory]
    [InlineData("EqualTo", true, "True")]
    [InlineData("EqualTo", false, "False")]
    [InlineData("NotEqualTo", true, "False")]
    [InlineData("NotEqualTo", false, "True")]
    public async Task Warning_For_nonnullable_boolean_constants(string method, bool value, string helper)
    {
        await Verify("{|#0:Guard.Argument(flag)." + method + "(" + (value ? "true" : "false") + ")|};",
            Expected(helper, method, value));
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public async Task Warning_For_nullable_boolean_equality(bool value, string helper)
    {
        await Verify("{|#0:Guard.Argument(nullableFlag).EqualTo(" + (value ? "true" : "false") + ")|};",
            Expected(helper, "EqualTo", value));
    }

    [Fact]
    public async Task Warning_For_static_extension_call_and_named_constant()
    {
        await Verify(@"
const bool required = true;
{|#0:Extensions.EqualTo(value: required, guard: Guard.Argument(flag), message: ""required"")|};",
            Expected("True", "EqualTo", true));
    }

    [Fact]
    public async Task Warning_For_object_guard_extension()
    {
        await Verify(@"{|#0:flag.Guard().NotEqualTo(false)|};", Expected("True", "NotEqualTo", false));
    }

    [Fact]
    public async Task Warning_For_guard_variable()
    {
        await Verify(@"
ArgumentGuard<bool> guard = Guard.Argument(flag);
{|#0:guard.EqualTo(false)|};", Expected("False", "EqualTo", false));
    }

    [Fact]
    public async Task Warning_After_nullable_guard_has_been_narrowed()
    {
        await Verify(@"{|#0:Guard.Argument(nullableFlag).HasValue().NotEqualTo(true)|};",
            Expected("False", "NotEqualTo", true));
    }

    [Fact]
    public async Task NoWarning_For_nullable_inequality_which_also_accepts_null()
    {
        await Verify(@"
Guard.Argument(nullableFlag).NotEqualTo(true);
Guard.Argument(nullableFlag).NotEqualTo(false);
Guard.Argument(nullableFlag).NotEqualTo(null);");
    }

    [Fact]
    public async Task NoWarning_For_nonconstants_other_types_and_unrelated_methods()
    {
        await Verify(@"
Guard.Argument(flag).EqualTo(expected);
Guard.Argument(number).EqualTo(1);
Guard.Argument(item).EqualTo((object)true);
OtherAssertions.EqualTo(Guard.Argument(flag), true);
Guard.Argument(flag).True();
Guard.Argument(nullableFlag).False();");
    }

    [Fact]
    public async Task NoDuplicateWarning_For_computed_boolean_input()
    {
        await Verify(@"
Guard.Argument(double.IsFinite(number), nameof(number)).EqualTo(true);
Guard.Argument(number > 0).NotEqualTo(false);
Guard.Argument((bool?)(number > 0)).EqualTo(true);
Guard.Argument(number > 0).Throws<InvalidOperationException>().EqualTo(true);");
    }
}
