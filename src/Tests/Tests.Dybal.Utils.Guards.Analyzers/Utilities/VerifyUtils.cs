using Dybal.Utils.Guards;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using VerifyCS = Tests.Dybal.Utils.Guards.Analyzers.CSharpAnalyzerVerifier<Dybal.Utils.Guards.Analyzers.GuardEqualsUsageAnalyzer>;

namespace Tests.Dybal.Utils.Guards.Analyzers.Utilities
{
    public static class VerifyUtils
    {
        public static Task Test(string source, params DiagnosticResult[] diagnostics)
        {
            var test = new VerifyCS.Test()
            {
                TestCode = source,
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.Net.Net60,
                    AdditionalReferences = { MetadataReference.CreateFromFile(typeof(Guard).Assembly.Location) }
                }
            };

            foreach (var diagnostic in diagnostics)
                test.ExpectedDiagnostics.Add(diagnostic);


            return test.RunAsync();
        }
    }
}
