using System.Collections.Immutable;
using Dybal.Utils.Guards.Analyzers.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dybal.Utils.Guards.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ArgumentGuardComputedSizeUsageAnalyzer : DiagnosticAnalyzer
{
    public static DiagnosticDescriptor DoNotGuardComputedSizeRule = new DiagnosticDescriptor(
#pragma warning disable RS2008
        id: DiagnosticIds.ArgumentGuard_DoNotGuardComputedSize,
#pragma warning restore RS2008
        title: "Guard the original string or collection instead of its size",
        messageFormat: @"Guard received the computed size ""{0}"". Pass the original string or collection to {1} instead.",
        DiagnosticCategories.ErrorProne,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Use collection guards for emptiness or uniqueness and string guards for length. This preserves the original argument name instead of reporting a Count or Length expression.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DoNotGuardComputedSizeRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterOperationAction(Analyze, OperationKind.Invocation);
    }

    private static void Analyze(OperationAnalysisContext context)
    {
        var invocation = (IInvocationOperation)context.Operation;
        var method = invocation.TargetMethod.ReducedFrom ?? invocation.TargetMethod;
        if (method.Name is not ("EqualTo" or "NotEqualTo" or "GreaterThan" or "GreaterThanOrEqualTo" or "LessThan" or "LessThanOrEqualTo") ||
            !GuardOperation.IsGuardExtension(invocation, context.Compilation))
            return;

        var value = GuardOperation.GuardedValue(GuardOperation.ExtensionReceiver(invocation), context.Compilation);
        var bound = GuardOperation.Argument(invocation, "value");
        if (value is null || bound is null || !TryGetSize(value, context.Compilation, out var source, out var isString))
            return;

        string? suggestion = null;
        if (method.Name == "EqualTo" && !isString &&
            TryGetSize(bound, context.Compilation, out var otherSource, out var otherIsString) && !otherIsString)
        {
            suggestion = UniqueSuggestion(source, otherSource, context.Compilation) ??
                         UniqueSuggestion(otherSource, source, context.Compilation);
        }

        if (suggestion is null && TryGetIntegerConstant(bound, out var limit))
        {
            suggestion = isString
                ? StringSuggestion(method.Name, limit)
                : CollectionSuggestion(method.Name, limit);
        }

        if (suggestion is not null)
            context.ReportDiagnostic(Diagnostic.Create(DoNotGuardComputedSizeRule,
                value.Syntax.GetLocation(), value.Syntax.ToString(), suggestion));
    }

    private static bool TryGetSize(IOperation operation, Compilation compilation, out IOperation source, out bool isString)
    {
        operation = GuardOperation.UnwrapImplicit(operation);
        source = operation;
        isString = false;
        if (operation is IPropertyReferenceOperation { Instance: not null } property)
        {
            var instance = GuardOperation.UnwrapImplicit(property.Instance);
            if (property.Property.Name == "Length" && instance.Type?.SpecialType == SpecialType.System_String)
            {
                source = instance;
                isString = true;
                return true;
            }

            if ((property.Property.Name is "Length" or "LongLength" && instance.Type is IArrayTypeSymbol { Rank: 1 }) ||
                (property.Property.Name == "Count" && IsCollection(instance.Type, compilation)))
            {
                source = instance;
                return property.Type?.SpecialType is SpecialType.System_Int32 or SpecialType.System_Int64;
            }
        }

        if (operation is IInvocationOperation invocation && IsLinq(invocation, compilation, "Count", "LongCount") &&
            invocation.Arguments.All(argument => argument.Parameter?.Name == "source"))
        {
            var sequence = SequenceSource(invocation);
            if (sequence is not null)
            {
                source = GuardOperation.UnwrapImplicit(sequence);
                return true;
            }
        }

        return false;
    }

    private static string? UniqueSuggestion(IOperation distinctSource, IOperation originalSource, Compilation compilation)
    {
        distinctSource = GuardOperation.UnwrapImplicit(distinctSource);
        if (distinctSource is not IInvocationOperation distinct || !IsLinq(distinct, compilation, "Distinct", "DistinctBy"))
            return null;

        var source = SequenceSource(distinct);
        if (source is null)
            return null;

        source = GuardOperation.UnwrapImplicit(source);
        var helper = distinct.TargetMethod.Name == "DistinctBy" ? "UniqueBy(...)" :
            GuardOperation.Argument(distinct, "comparer") is not null ? "Unique(...)" : "Unique()";
        if (source is IInvocationOperation select && IsLinq(select, compilation, "Select"))
        {
            if (GuardOperation.Argument(select, "selector")?.Type is not INamedTypeSymbol selector ||
                selector.DelegateInvokeMethod?.Parameters.Length != 1)
                return null;
            source = SequenceSource(select);
            helper = "UniqueBy(...)";
        }

        return source is not null && IsStableSource(source) && IsStableSource(originalSource) &&
               SyntaxFactory.AreEquivalent(GuardOperation.UnwrapImplicit(source).Syntax, GuardOperation.UnwrapImplicit(originalSource).Syntax)
            ? helper
            : null;
    }

    private static bool IsStableSource(IOperation operation) => GuardOperation.UnwrapImplicit(operation) switch
    {
        ILocalReferenceOperation => true,
        IParameterReferenceOperation => true,
        IInstanceReferenceOperation => true,
        IFieldReferenceOperation field => field.Instance is null || IsStableSource(field.Instance),
        _ => false,
    };

    private static string? CollectionSuggestion(string method, long limit) => (method, limit) switch
    {
        ("EqualTo", 0) or ("LessThanOrEqualTo", 0) or ("LessThan", 1) => "Empty()",
        ("NotEqualTo", 0) or ("GreaterThan", 0) or ("GreaterThanOrEqualTo", 1) => "NotEmpty()",
        _ => null,
    };

    private static string? StringSuggestion(string method, long limit) => (method, limit) switch
    {
        ("NotEqualTo", 0) or ("GreaterThan", 0) or ("GreaterThanOrEqualTo", 1) => "NotNullOrEmpty()",
        ("EqualTo", 0) => "MaxLength(0)",
        ("EqualTo", _) when limit > 0 && limit <= uint.MaxValue => $"MinLength({limit}).MaxLength({limit})",
        ("GreaterThan", _) when limit >= 0 && limit < uint.MaxValue => $"MinLength({limit + 1})",
        ("GreaterThanOrEqualTo", _) when limit > 0 && limit <= uint.MaxValue => $"MinLength({limit})",
        ("LessThan", _) when limit > 0 && limit <= (long)uint.MaxValue + 1 => $"MaxLength({limit - 1})",
        ("LessThanOrEqualTo", _) when limit >= 0 && limit <= uint.MaxValue => $"MaxLength({limit})",
        _ => null,
    };

    private static bool IsCollection(ITypeSymbol? type, Compilation compilation)
    {
        var definitions = new[]
        {
            "System.Collections.Generic.ICollection`1",
            "System.Collections.Generic.IReadOnlyCollection`1",
        };
        return type is not null && definitions.Any(name =>
        {
            var definition = compilation.GetTypeByMetadataName(name);
            return definition is not null &&
                   (SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, definition) ||
                    type.AllInterfaces.Any(implemented => SymbolEqualityComparer.Default.Equals(implemented.OriginalDefinition, definition)));
        });
    }

    private static bool IsLinq(IInvocationOperation invocation, Compilation compilation, params string[] names)
    {
        var method = invocation.TargetMethod.ReducedFrom ?? invocation.TargetMethod;
        return names.Contains(method.Name) && SymbolEqualityComparer.Default.Equals(
            method.ContainingType, compilation.GetTypeByMetadataName("System.Linq.Enumerable"));
    }

    private static IOperation? SequenceSource(IInvocationOperation invocation) =>
        invocation.TargetMethod.ReducedFrom is not null
            ? invocation.Instance
            : GuardOperation.Argument(invocation, "source");

    private static bool TryGetIntegerConstant(IOperation operation, out long value)
    {
        value = 0;
        if (!operation.ConstantValue.HasValue)
            return false;

        switch (operation.ConstantValue.Value)
        {
            case int number: value = number; return true;
            case long number: value = number; return true;
            case uint number: value = number; return true;
            default: return false;
        }
    }
}
