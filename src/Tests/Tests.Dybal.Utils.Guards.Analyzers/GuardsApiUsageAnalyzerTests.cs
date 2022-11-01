using Dybal.Utils.Guards.Analyzers;
using Microsoft.CodeAnalysis.Testing;
using Tests.Dybal.Utils.Guards.Analyzers.Utilities;
using Xunit;

namespace Tests.Dybal.Utils.Guards.Analyzers;

public class GuardsApiUsageAnalyzerTests
{
    [Fact]
    public async Task NoWarning_When_ArgumentGuard_EqualTo_Invocation()
    {
        // Prepare
        var source = @"
using Dybal.Utils.Guards;

class Program
{
    void Method()
    {
        int value = 1;
        Guard.Argument(value).EqualTo(1);
    }
}";

        // Assert
        await VerifyUtils.Test(source);
    }

    [Fact]
    public async Task Warning_When_ArgumentGuard_Equals_Invocation()
    {
        // Prepare
        var source = @"
using Dybal.Utils.Guards;

class Program
{
    void Method()
    {
        int value = 1;
        {|#0:Guard.Argument(value).Equals(1)|};
    }
}";

        // Assert
        var expectedDiagnostic = new DiagnosticResult(GuardEqualsUsageAnalyzer.UseEqualInsteadOfEqualsRule).WithLocation(0);
        await VerifyUtils.Test(source, expectedDiagnostic);
    }
}