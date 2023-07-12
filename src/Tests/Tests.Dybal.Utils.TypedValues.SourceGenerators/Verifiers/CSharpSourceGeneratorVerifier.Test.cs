using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Tests.Dybal.Utils.TypedValues.SourceGenerators.Verifiers
{
	internal static partial class CSharpSourceGeneratorVerifier<TSourceGenerator>
		where TSourceGenerator : IIncrementalGenerator, new()
	{
		public sealed class Test : CSharpSourceGeneratorTest<EmptySourceGeneratorProvider, XUnitVerifier>
		{
			public Test()
			{
			}

			public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.Default;

			protected override IEnumerable<Type> GetSourceGenerators()
			{
				return new[] { typeof(TSourceGenerator) };
			}

			protected override CompilationOptions CreateCompilationOptions()
			{
				CompilationOptions compilationOptions = base.CreateCompilationOptions();
				return compilationOptions.WithSpecificDiagnosticOptions(
					 compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
			}

			protected override ParseOptions CreateParseOptions()
			{
				return ((CSharpParseOptions)base.CreateParseOptions()).WithLanguageVersion(LanguageVersion);
			}
		}
	}
}
