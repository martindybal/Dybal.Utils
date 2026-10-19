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
#pragma warning disable RS2008
        id: DiagnosticIds.Guard_DoNotGuardComputedCondition,
#pragma warning restore RS2008
        title: "Do not guard a condition you evaluated yourself",
        messageFormat: @"Guard received the computed condition ""{0}"" instead of the original argument. An explicit argument name does not preserve the offending value. Pass the original argument to a matching guard.",
        DiagnosticCategories.ErrorProne,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Evaluating a check before handing it to the guard loses the original value. CallerArgumentExpression also captures the condition as the parameter name unless a name is supplied explicitly. Pass the original argument to helpers such as Finite, EnumDefined, EqualTo or Satisfies.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DoNotGuardComputedConditionRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterOperationAction(Analyze, OperationKind.Invocation);
    }

    private static void Analyze(OperationAnalysisContext context)
    {
        var invocation = (IInvocationOperation)context.Operation;
        if (!GuardOperation.IsEntryPoint(invocation, context.Compilation))
            return;

        foreach (var argument in GuardOperation.GuardedArguments(invocation))
        {
            if (GuardOperation.IsComputedCondition(argument))
            {
                var expression = GuardOperation.ExpressionSyntax(argument);
                context.ReportDiagnostic(Diagnostic.Create(DoNotGuardComputedConditionRule,
                    expression.GetLocation(), expression.ToString()));
            }
        }
    }
}
