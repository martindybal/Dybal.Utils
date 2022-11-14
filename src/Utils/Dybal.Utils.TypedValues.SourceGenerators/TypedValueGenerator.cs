using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Dybal.Utils.TypedValues.SourceGenerators;

[Generator]
public class TypedValueGenerator : IIncrementalGenerator
{
    private const string TypedValueAttributeFullName = "Dybal.Utils.TypedValues.TypedValueAttribute`1";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Do a simple filter for records
        IncrementalValuesProvider<RecordDeclarationSyntax> recordDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                TypedValueAttributeFullName,
                predicate: IsRecordWithAttribute,
                transform: GetSemanticTargetForGeneration)
            .Where(static declarationSyntax => declarationSyntax is not null)!;

        IncrementalValueProvider<(Compilation, ImmutableArray<RecordDeclarationSyntax>)> compilationAndRecords
            = context.CompilationProvider.Combine(recordDeclarations.Collect());

        // Generate the source using the compilation and records
        context.RegisterSourceOutput(compilationAndRecords, static (spc, source) => GenerateTypedValue(source.Item1, source.Item2, spc));
    }

    static bool IsRecordWithAttribute(SyntaxNode node, CancellationToken ct)
        => node is RecordDeclarationSyntax { AttributeLists.Count: > 0 };

    static RecordDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        var recordDeclarationSyntax = (RecordDeclarationSyntax)context.TargetNode;
        return recordDeclarationSyntax;
    }

    static void GenerateTypedValue(Compilation compilation, IEnumerable<RecordDeclarationSyntax>? records, SourceProductionContext context)
    {
        records = records?.Distinct().ToArray();

        if (records == null || !records.Any())
        {
            return;
        }

        var recordsToGenerate = GetTypedValuesMetadata(compilation, records, context.CancellationToken);

        foreach (var typedValueMetadata in recordsToGenerate)
        {
            // generate the source code and add it to the output
            var typedValueSourceCode = TypedValueCodeBuilder.GetTypedValueGeneratedCode(typedValueMetadata);
            context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}.g.cs", SourceText.From(typedValueSourceCode, Encoding.UTF8));
        }
    }

    static IEnumerable<TypedValueMetadata> GetTypedValuesMetadata(Compilation compilation, IEnumerable<RecordDeclarationSyntax> records, CancellationToken ct)
    {
        foreach (var recordDeclarationSyntax in records)
        {
            ct.ThrowIfCancellationRequested();

            var semanticModel = compilation.GetSemanticModel(recordDeclarationSyntax.SyntaxTree);
            if (ModelExtensions.GetDeclaredSymbol(semanticModel, recordDeclarationSyntax) is INamedTypeSymbol recordSymbol)
            {
                var recordName = recordSymbol.Name;
                var recordNamespace = recordSymbol.ContainingNamespace.ToString();

                var typedValueAttribute = recordSymbol.GetAttributes().Single(attribute => attribute.AttributeClass!.Name != TypedValueAttributeFullName);
                var valueName = GetAttributePropertyValue<string>(typedValueAttribute, "ValueName");
                var valueType = GetAttributePropertyValue<Type>(typedValueAttribute, "TValueType");
                
                //TODO
                valueName ??= "Value";
                valueType ??= typeof(string);

                yield return new TypedValueMetadata(recordName, recordNamespace, valueType, valueName, recordSymbol.IsReadOnly);
            }
        }
    }

    private static TProperty GetAttributePropertyValue<TProperty>(AttributeData attribute, string propertyName)
    {
        return (TProperty)attribute.NamedArguments.SingleOrDefault(argument => argument.Key == propertyName).Value.Value!;
    }
}
