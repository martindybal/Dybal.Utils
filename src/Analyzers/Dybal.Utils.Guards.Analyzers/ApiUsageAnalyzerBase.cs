using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;

namespace Dybal.Utils.Guards.Analyzers;

public abstract class ApiUsageAnalyzerBase : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
    private readonly string declaringType;
    private readonly HashSet<string> targetMethods;

    public ApiUsageAnalyzerBase(
        DiagnosticDescriptor descriptor,
        string typeFullName,
        IEnumerable<string> methods)
    {
        SupportedDiagnostics = ImmutableArray.Create(descriptor);
        declaringType = typeFullName;
        targetMethods = new(methods);
    }

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterOperationAction(context =>
        {
            var declaringType = context.Compilation.GetTypeByMetadataName(this.declaringType);
            if (declaringType is null)
                return;

            if (context.Operation is IInvocationOperation invocation)
            {
                var symbol = invocation.TargetMethod;
                var containingType = symbol.ContainingType;

                containingType = (containingType.IsGenericType) ? containingType.ConstructUnboundGenericType() : containingType;
                declaringType = (declaringType.IsGenericType) ? declaringType.ConstructUnboundGenericType() : declaringType;

                if (symbol.MethodKind != MethodKind.Ordinary || !SymbolEqualityComparer.Default.Equals(declaringType, containingType) || !targetMethods.Contains(symbol.Name))
                    return;

                AnalyzeInvocation(context, invocation, symbol);
            }
        }, OperationKind.Invocation);
    }

    protected abstract void AnalyzeInvocation(
        OperationAnalysisContext context, 
        IInvocationOperation invocation, 
        IMethodSymbol method);
}