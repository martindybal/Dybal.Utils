using Dybal.Utils.Guards.Analyzers;
using Microsoft.CodeAnalysis.Testing;
using Newtonsoft.Json.Linq;
using Tests.Dybal.Utils.Guards.Analyzers.Utilities;
using Xunit;

namespace Tests.Dybal.Utils.Guards.Analyzers;

public class GuardsEqualsUsageAnalyzerTests
{

    private const string dotnetUpdateSkipMessage = "You need to use ReferenceAssemblies.Net.Net70 in VerifyUtils which are not yet available";

    [Fact(Skip = dotnetUpdateSkipMessage)]
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
        await VerifyUtils<ArgumentGuardEqualsUsageAnalyzer>.Test(source);
    }
    
    [Fact(Skip = dotnetUpdateSkipMessage)]
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
        var expectedDiagnostic = new DiagnosticResult(ArgumentGuardEqualsUsageAnalyzer.UseEqualInsteadOfEqualsRule).WithLocation(0);
        await VerifyUtils<ArgumentGuardEqualsUsageAnalyzer>.Test(source, expectedDiagnostic);
    }

    [Fact(Skip = dotnetUpdateSkipMessage)]
    public async Task NoWarning_When_MultipleArgumentGuard_AtLeastOneIsNotNull_Invocation()
    {
        // Prepare
        var source = @"
using Dybal.Utils.Guards;

class Program
{
    void Method()
    {
        int value1 = 1;
        int value2 = 2;
        Guard.Arguments(value1, value2).AtLeastOneIsNotNull();
    }
}";

        // Assert
        await VerifyUtils<MultipleArgumentGuardEqualsUsageAnalyzer>.Test(source);
    }

    [Fact(Skip = dotnetUpdateSkipMessage)]
    public async Task Warning_When_MultipleArgumentGuard_Equals_Invocation()
    {
        // Prepare
        var source = @"
using Dybal.Utils.Guards;

class Program
{
    void Method()
    {
        int value1 = 1;
        int value2 = 2;
        {|#0:Guard.Arguments(value1, value2).Equals(1)|};
    }
}";

        // Assert
        var expectedDiagnostic = new DiagnosticResult(MultipleArgumentGuardEqualsUsageAnalyzer.DoNotUseEqualsRule).WithLocation(0);
        await VerifyUtils<MultipleArgumentGuardEqualsUsageAnalyzer>.Test(source, expectedDiagnostic);
    }

    [Fact]
    public async Task NoWarning_When_DifferentType_Equals_Invocation()
    {
        // Prepare
        var source = @"
using Dybal.Utils.Guards;

class Program
{
    void Method()
    {
        int value = 1;
        value.Equals(2);
    }
}";

        // Assert
        await VerifyUtils<ArgumentGuardEqualsUsageAnalyzer>.Test(source);
        await VerifyUtils<MultipleArgumentGuardEqualsUsageAnalyzer>.Test(source);
    }
}