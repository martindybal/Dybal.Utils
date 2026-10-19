using System.Collections.Immutable;
using Dybal.Utils.Guards.Analyzers.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dybal.Utils.Guards.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ArgumentGuardBooleanUsageAnalyzer : DiagnosticAnalyzer
{
    public static DiagnosticDescriptor UseBooleanGuardRule = new DiagnosticDescriptor(
#pragma warning disable RS2008
        id: DiagnosticIds.ArgumentGuard_UseBooleanGuard,
#pragma warning restore RS2008
        title: "Use a boolean guard for a boolean constant",
        messageFormat: "Use {0}() instead of {1}({2}) for this boolean argument",
        DiagnosticCategories.ErrorProne,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "True and False express boolean validation directly. A nullable boolean inequality is excluded because NotEqualTo(true) accepts null whereas False rejects it.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(UseBooleanGuardRule);

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
        if (method.Name is not ("EqualTo" or "NotEqualTo") ||
            !GuardOperation.IsGuardExtension(invocation, context.Compilation))
            return;

        var receiver = GuardOperation.ExtensionReceiver(invocation);
        var receiverType = receiver?.Type as INamedTypeSymbol;
        if (receiverType?.TypeArguments.Length != 1 || !GuardOperation.IsBoolean(receiverType.TypeArguments[0]))
            return;

        if (method.Name == "NotEqualTo" && GuardOperation.IsNullableBoolean(receiverType.TypeArguments[0]))
            return;

        var value = GuardOperation.Argument(invocation, "value");
        if (value is null || GuardOperation.Unwrap(value).ConstantValue is not { HasValue: true, Value: bool expected })
            return;

        var guardedValue = GuardOperation.GuardedValue(receiver, context.Compilation);
        if (guardedValue is not null && GuardOperation.IsComputedCondition(guardedValue))
            return;

        var requiredValue = method.Name == "EqualTo" ? expected : !expected;
        context.ReportDiagnostic(Diagnostic.Create(UseBooleanGuardRule, invocation.Syntax.GetLocation(),
            requiredValue ? "True" : "False", method.Name, expected ? "true" : "false"));
    }
}
