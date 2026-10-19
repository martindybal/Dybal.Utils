using Dybal.Utils.Guards.Analyzers;
using Microsoft.CodeAnalysis.Testing;
using Tests.Dybal.Utils.Guards.Analyzers.Utilities;
using Xunit;

namespace Tests.Dybal.Utils.Guards.Analyzers;

public class GuardArgumentConditionUsageAnalyzerTests
{
    private const string Usings = @"
using System;
using Dybal.Utils.Guards;
using Dybal.Utils.Guards.ObjectExtensions;
";

    // "using static GuardProvider" makes the name "Guard" resolve to the shortcut method instead of the
    // Guard class, so it is only pulled in by the tests that actually exercise the shortcut.
    private const string GuardProviderUsing = @"using static Dybal.Utils.Guards.GuardProvider;
";

    private const string Types = @"
enum State
{
    Draft = 1,
    Published = 2,
}

class Options
{
    public bool IsEnabled { get; set; }
}
";

    private static Task Verify(string body, params DiagnosticResult[] diagnostics) =>
        Verify(extraUsings: "", body, diagnostics);

    private static Task Verify(string extraUsings, string body, params DiagnosticResult[] diagnostics)
    {
        var source = Usings + extraUsings + Types + @"
class Program
{
    void Method(State state, bool flag, bool? nullableFlag, string text, int number, double value, Options options)
    {
" + body + @"
    }

    Options GetOptions() => new Options();
}";

        return VerifyUtils<GuardArgumentConditionUsageAnalyzer>.Test(source, diagnostics);
    }

    private static DiagnosticResult Expected(string condition) =>
        new DiagnosticResult(GuardArgumentConditionUsageAnalyzer.DoNotGuardComputedConditionRule)
            .WithLocation(0)
            .WithArguments(condition);

    [Fact]
    public async Task Warning_When_Enum_IsDefined_is_evaluated_before_the_guard()
    {
        // The usage this rule exists for.
        await Verify(
            @"        Guard.Argument({|#0:Enum.IsDefined(state)|}).EqualTo(true);",
            Expected("Enum.IsDefined(state)"));
    }

    [Fact]
    public async Task Warning_When_string_IsNullOrEmpty_is_evaluated_before_the_guard()
    {
        await Verify(
            @"        Guard.Argument({|#0:string.IsNullOrEmpty(text)|}).False();",
            Expected("string.IsNullOrEmpty(text)"));
    }

    [Fact]
    public async Task Warning_When_comparison_is_evaluated_before_the_guard()
    {
        await Verify(
            @"        Guard.Argument({|#0:number > 5|}).True();",
            Expected("number > 5"));
    }

    [Fact]
    public async Task Warning_When_negation_is_evaluated_before_the_guard()
    {
        await Verify(
            @"        Guard.Argument({|#0:!flag|}).False();",
            Expected("!flag"));
    }

    [Fact]
    public async Task Warning_When_is_pattern_is_evaluated_before_the_guard()
    {
        await Verify(
            @"        Guard.Argument({|#0:text is not null|}).True();",
            Expected("text is not null"));
    }

    [Fact]
    public async Task Warning_When_literal_is_guarded()
    {
        await Verify(
            @"        Guard.Argument({|#0:true|}).True();",
            Expected("true"));
    }

    [Fact]
    public async Task Warning_When_GuardProvider_shortcut_is_used()
    {
        await Verify(
            GuardProviderUsing,
            @"        Guard({|#0:Enum.IsDefined(state)|}).True();",
            Expected("Enum.IsDefined(state)"));
    }

    [Fact]
    public async Task Warning_When_object_extension_is_used()
    {
        await Verify(
            @"        {|#0:Enum.IsDefined(state)|}.Guard().True();",
            Expected("Enum.IsDefined(state)"));
    }

    [Fact]
    public async Task Warning_When_condition_is_passed_to_multiple_argument_guard()
    {
        await Verify(
            @"        Guard.Arguments({|#0:Enum.IsDefined(state)|}, text).AtLeastOneIsNotNull();",
            Expected("Enum.IsDefined(state)"));
    }

    [Fact]
    public async Task NoWarning_When_argument_is_passed_to_EnumDefined()
    {
        // The fix the rule is steering towards.
        await Verify(@"        Guard.Argument(state).EnumDefined();");
    }

    [Fact]
    public async Task NoWarning_When_bool_argument_is_guarded_directly()
    {
        await Verify(@"        Guard.Argument(flag).True();");
    }

    [Fact]
    public async Task NoWarning_When_bool_property_is_guarded_directly()
    {
        await Verify(@"        Guard.Argument(options.IsEnabled).True();");
    }

    [Fact]
    public async Task NoWarning_When_guarded_invocation_does_not_return_bool()
    {
        // The value still reaches the exception, so nothing is lost.
        await Verify(@"        Guard.Argument(GetOptions()).NotNull();");
    }

    [Fact]
    public async Task NoWarning_When_object_extension_guards_the_argument_directly()
    {
        await Verify(@"        state.Guard().EnumDefined();");
    }

    [Fact]
    public async Task NoWarning_When_condition_is_passed_to_an_unrelated_method()
    {
        await Verify(@"        Console.WriteLine(Enum.IsDefined(state));");
    }

    [Fact]
    public async Task NoWarning_When_multiple_arguments_are_guarded_directly()
    {
        await Verify(@"        Guard.Arguments(text, options).AtLeastOneIsNotNull();");
    }

    [Theory]
    [InlineData("(number > 5)")]
    [InlineData("(bool)(number > 5)")]
    [InlineData("(bool?)(number > 5)")]
    [InlineData("(bool)(object)(number > 5)")]
    [InlineData("nullableFlag & true")]
    [InlineData("flag | options.IsEnabled")]
    [InlineData("flag ^ options.IsEnabled")]
    [InlineData("flag && options.IsEnabled")]
    [InlineData("flag || options.IsEnabled")]
    [InlineData("flag ? nullableFlag : false")]
    [InlineData("nullableFlag ?? false")]
    [InlineData("default(bool)")]
    [InlineData("default(bool?)")]
    [InlineData("text is object")]
    [InlineData("text?.StartsWith(\"a\")")]
    public async Task Warning_For_wrapped_nullable_and_other_computed_conditions(string expression)
    {
        await Verify("Guard.Argument({|#0:" + expression + "|});", Expected(expression));
    }

    [Fact]
    public async Task Warning_With_explicit_argument_name()
    {
        await Verify(
            @"Guard.Argument({|#0:double.IsFinite(value)|}, nameof(value)).EqualTo(true);",
            Expected("double.IsFinite(value)"));
    }

    [Fact]
    public async Task Warning_With_named_arguments_in_different_order()
    {
        await Verify(
            @"Guard.Argument(argumentName: nameof(value), argumentValue: {|#0:double.IsFinite(value)|});",
            Expected("double.IsFinite(value)"));
    }

    [Fact]
    public async Task Warning_With_Guard_alias()
    {
        await Verify("using G = Dybal.Utils.Guards.Guard;", @"G.Argument({|#0:number > 0|});", Expected("number > 0"));
    }

    [Fact]
    public async Task Warning_With_static_object_extension_invocation()
    {
        await Verify(
            @"GuardObjectExtensionsExtensions.Guard({|#0:number > 0|}, nameof(number));",
            Expected("number > 0"));
    }

    [Fact]
    public async Task Warning_For_each_computed_argument_in_a_multiple_guard()
    {
        var second = new DiagnosticResult(GuardArgumentConditionUsageAnalyzer.DoNotGuardComputedConditionRule)
            .WithLocation(1).WithArguments("number > 0");
        await Verify(
            @"Guard.Arguments(value2: {|#1:number > 0|}, value1: {|#0:double.IsFinite(value)|}, paramName1: nameof(value));",
            Expected("double.IsFinite(value)"), second);
    }

    [Fact]
    public async Task Warning_With_multiple_GuardProvider_arguments()
    {
        await Verify(GuardProviderUsing, @"Guard(text, {|#0:number > 0|});", Expected("number > 0"));
    }

    [Fact]
    public async Task NoWarning_For_wrapped_direct_booleans_and_metadata()
    {
        await Verify(@"
Guard.Argument((bool?)flag);
Guard.Argument(nullableFlag);
Guard.Argument(options?.IsEnabled);
Guard.Argument(flag, argumentName: (string)(object)(number > 0));
Guard.Arguments(flag, nullableFlag, paramName1: (string)(object)(number > 0));
GuardObjectExtensionsExtensions.Guard(flag, (string)(object)(number > 0));");
    }

    [Fact]
    public async Task NoWarning_For_an_unrelated_Guard_type()
    {
        await Verify("class OtherGuard { public static void Argument(bool value) { } }", @"OtherGuard.Argument(number > 0);");
    }
}
