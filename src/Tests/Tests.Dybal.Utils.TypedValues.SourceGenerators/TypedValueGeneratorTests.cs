using Xunit;
using VerifyCS = Tests.Dybal.Utils.TypedValues.SourceGenerators.Verifiers.CSharpSourceGeneratorVerifier<Dybal.Utils.TypedValues.SourceGenerators.TypedValueGenerator>;

namespace Tests.Dybal.Utils.TypedValues.SourceGenerators;

public class TypedValueGeneratorTests
{
    protected string typedValueFilename => "My.TypedValue.Tests.TypedValueTest.g.cs";

    protected string typedValueCode => @"using System;
using Dybal.Utils.TypedValues;

namespace My.TypedValue.Tests
{
    [TypedValue<int>(ValueName = ""Percentage"")]
    public readonly partial record struct TypedValueTest;
}";

    protected string typedValueGeneratedCode => @"";

    [Fact]
    public async Task Debug()
    {
        try
        {
            var generatedSources = new[]
            {
                (typedValueFilename, typedValueGeneratedCode),
            };
        
            await VerifyCS.VerifyGeneratorAsync(typedValueCode, generatedSources);
        }
        catch
        {
            // "This is not test. For debug purpose only"
        }
    }
}