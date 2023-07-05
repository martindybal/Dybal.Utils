using Dybal.Utils.Guards.Analyzers.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dybal.Utils.Guards.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MultipleArgumentGuardEqualsUsageAnalyzer : ApiUsageAnalyzerBase
{
    public static DiagnosticDescriptor DoNotUseEqualsRule = new DiagnosticDescriptor(
#pragma warning disable RS2008 // Enable analyzer release tracking
        id: DiagnosticIds.MultipleArgumentGuard_DoNotUseEquals,
#pragma warning restore RS2008 // Enable analyzer release tracking
        title: "Do not use invalid equality check",
        messageFormat: @"When writing guard clause, do not use Equals method",
        DiagnosticCategories.ErrorProne,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public MultipleArgumentGuardEqualsUsageAnalyzer()
        : base(
            descriptor: DoNotUseEqualsRule,
            typeFullName: "Dybal.Utils.Guards.MultipleArgumentGuard",
            methods: new[] { "Equals" })
    {

    }

    protected override void AnalyzeInvocation(OperationAnalysisContext context, IInvocationOperation invocation, IMethodSymbol method)
    {
        // Whenever someone invokes .Equals on MultipleArgumentGuard, issue a warning
        var diagnostics = Diagnostic.Create(DoNotUseEqualsRule, invocation.Syntax.GetLocation());
        context.ReportDiagnostic(diagnostics);
    }
}
