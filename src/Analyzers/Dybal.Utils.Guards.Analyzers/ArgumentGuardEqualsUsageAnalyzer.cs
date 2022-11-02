using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dybal.Utils.Guards.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ArgumentGuardEqualsUsageAnalyzer : ApiUsageAnalyzerBase
{
    public static DiagnosticDescriptor UseEqualInsteadOfEqualsRule = new DiagnosticDescriptor(
#pragma warning disable RS2008 // Enable analyzer release tracking
        id: DiagnosticIds.ArgumentGuard_UseEqualToInsteadOfEquals,
#pragma warning restore RS2008 // Enable analyzer release tracking
        title: "Do not use invalid equality check",
        messageFormat: @"When checking for equality, do not use ""Equals"" - use ""EqualTo"" instead",
        DiagnosticCategories.ErrorProne,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public ArgumentGuardEqualsUsageAnalyzer()
        : base(
            descriptor: UseEqualInsteadOfEqualsRule,
            typeFullName: "Dybal.Utils.Guards.ArgumentGuard`1",
            methods: new[] { "Equals" })
    {

    }

    protected override void AnalyzeInvocation(OperationAnalysisContext context, IInvocationOperation invocation, IMethodSymbol method)
    {
        // Whenever someone invokes .Equals on ArgumentGuard, issue a warning
        var diagnostics = Diagnostic.Create(UseEqualInsteadOfEqualsRule, invocation.Syntax.GetLocation(), "Equals", "EqualTo");
        context.ReportDiagnostic(diagnostics);
    }
}
