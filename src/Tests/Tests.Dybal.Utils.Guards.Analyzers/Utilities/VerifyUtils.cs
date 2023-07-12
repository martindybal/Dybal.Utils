using Dybal.Utils.Guards;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Tests.Dybal.Utils.Guards.Analyzers.Verifiers;

namespace Tests.Dybal.Utils.Guards.Analyzers.Utilities;

public static class VerifyUtils<TAnalyzer>
    where TAnalyzer : DiagnosticAnalyzer, new()
{
    public static Task Test(string source, params DiagnosticResult[] diagnostics)
    {
        var test = new CSharpAnalyzerVerifier<TAnalyzer>.Test()
        {
            TestCode = source,
            TestState =
            {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net70,
                AdditionalReferences = { MetadataReference.CreateFromFile(typeof(Guard).Assembly.Location) }
            }
        };

        foreach (var diagnostic in diagnostics)
        {
            test.ExpectedDiagnostics.Add(diagnostic);
        }
        
        return test.RunAsync();
    }
}
