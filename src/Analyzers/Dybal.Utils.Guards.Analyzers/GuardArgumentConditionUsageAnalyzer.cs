using Dybal.Utils.Guards.Analyzers.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;

namespace Dybal.Utils.Guards.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class GuardArgumentConditionUsageAnalyzer : DiagnosticAnalyzer
{
    public static DiagnosticDescriptor DoNotGuardComputedConditionRule = new DiagnosticDescriptor(
#pragma warning disable RS2008 // Enable analyzer release tracking
        id: DiagnosticIds.Guard_DoNotGuardComputedCondition,
#pragma warning restore RS2008 // Enable analyzer release tracking
        title: "Do not guard a condition you evaluated yourself",
        messageFormat: @"Guard received the condition ""{0}"" instead of the argument itself, so ""{0}"" will be reported as the parameter name and the offending value is lost. Pass the argument and let a guard do the check.",
        DiagnosticCategories.ErrorProne,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "The argument name is filled in by [CallerArgumentExpression], which captures the source text of the expression rather than the variable behind it. Evaluating the check before handing it to the guard therefore produces a meaningless ParamName and throws the original value away.");

    private static readonly Dictionary<string, string> GuardEntryPoints = new()
    {
        ["Dybal.Utils.Guards.Guard"] = "Argument",
        ["Dybal.Utils.Guards.GuardProvider"] = "Guard",
        ["Dybal.Utils.Guards.ObjectExtensions.GuardObjectExtensionsExtensions"] = "Guard",
    };

    private const string MultipleArgumentsMethod = "Arguments";

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DoNotGuardComputedConditionRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(compilationStart =>
        {
            var entryPointTypes = new Dictionary<INamedTypeSymbol, string>(SymbolEqualityComparer.Default);
            foreach (var entryPoint in GuardEntryPoints)
            {
                var type = compilationStart.Compilation.GetTypeByMetadataName(entryPoint.Key);
                if (type is not null)
                {
                    entryPointTypes[type] = entryPoint.Value;
                }
            }

            if (entryPointTypes.Count == 0)
                return;

            compilationStart.RegisterOperationAction(operationContext => Analyze(operationContext, entryPointTypes), OperationKind.Invocation);
        });
    }

    private static void Analyze(OperationAnalysisContext context, Dictionary<INamedTypeSymbol, string> entryPointTypes)
    {
        var invocation = (IInvocationOperation)context.Operation;

        // For "value.Guard()" the target method is the reduced form, whose ContainingType is the extension target.
        var method = invocation.TargetMethod.ReducedFrom ?? invocation.TargetMethod;

        if (!entryPointTypes.TryGetValue(method.ContainingType, out var entryPointMethod))
            return;

        if (method.Name != entryPointMethod && method.Name != MultipleArgumentsMethod)
            return;

        foreach (var argument in GuardedArguments(invocation))
        {
            if (IsComputedCondition(argument))
            {
                var condition = argument.Syntax.ToString();
                context.ReportDiagnostic(Diagnostic.Create(DoNotGuardComputedConditionRule, argument.Syntax.GetLocation(), condition));
            }
        }
    }

    private static IEnumerable<IOperation> GuardedArguments(IInvocationOperation invocation)
    {
        // The [CallerArgumentExpression] parameters are filled in by the compiler, so only what the
        // caller actually wrote is worth looking at.
        foreach (var argument in invocation.Arguments)
        {
            if (argument.ArgumentKind == ArgumentKind.Explicit)
            {
                yield return Unwrap(argument.Value);
            }
        }

        // A reduced extension method exposes its receiver as Instance rather than as an argument.
        if (invocation.Instance is not null && invocation.TargetMethod.ReducedFrom is not null)
        {
            yield return Unwrap(invocation.Instance);
        }
    }

    private static IOperation Unwrap(IOperation operation)
    {
        while (operation is IConversionOperation conversion && conversion.IsImplicit)
        {
            operation = conversion.Operand;
        }

        return operation;
    }

    private static bool IsComputedCondition(IOperation operation)
    {
        // Only bool: a guard that was handed a condition has already thrown the real argument away.
        // Anything else - "Guard.Argument(GetConfig())" - still carries its value to the exception.
        if (operation.Type?.SpecialType != SpecialType.System_Boolean)
            return false;

        return operation switch
        {
            IInvocationOperation => true,
            IBinaryOperation binary => IsComparisonOrLogical(binary.OperatorKind),
            IUnaryOperation unary => unary.OperatorKind == UnaryOperatorKind.Not,
            IIsPatternOperation => true,
            IIsTypeOperation => true,
            ILiteralOperation => true,
            IConditionalOperation => true,
            _ => false,
        };
    }

    private static bool IsComparisonOrLogical(BinaryOperatorKind operatorKind) => operatorKind switch
    {
        BinaryOperatorKind.Equals => true,
        BinaryOperatorKind.NotEquals => true,
        BinaryOperatorKind.ObjectValueEquals => true,
        BinaryOperatorKind.ObjectValueNotEquals => true,
        BinaryOperatorKind.LessThan => true,
        BinaryOperatorKind.LessThanOrEqual => true,
        BinaryOperatorKind.GreaterThan => true,
        BinaryOperatorKind.GreaterThanOrEqual => true,
        BinaryOperatorKind.ConditionalAnd => true,
        BinaryOperatorKind.ConditionalOr => true,
        _ => false,
    };
}
