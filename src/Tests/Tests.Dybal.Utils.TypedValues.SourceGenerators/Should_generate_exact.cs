using Xunit;
using VerifyCS = Tests.Dybal.Utils.TypedValues.SourceGenerators.Verifiers.CSharpSourceGeneratorVerifier<Dybal.Utils.TypedValues.SourceGenerators.TypedValueGenerator>;

namespace Tests.Dybal.Utils.TypedValues.SourceGenerators;

public abstract class Should_generate_exact
{
    protected abstract string code { get; }
    protected abstract string filename { get; }
    protected abstract string generated { get; }

    [Fact]
    public async Task Code()
    {
        await VerifyCS.VerifyGeneratorAsync(code, (filename, generated));
    }
}