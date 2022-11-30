using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Dybal.Utils.TypedValues.SourceGenerators;

[Generator]
public class TypedValueGenerator : IIncrementalGenerator
{
    private const string TypedValueAttributeFullName = "Dybal.Utils.TypedValues.TypedValueAttribute";
    private const string TypedValueAttributeFullName1 = "Dybal.Utils.TypedValues.TypedValueAttribute<TValue>";

    private static readonly SymbolDisplayFormat TypedValueFormat = new(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Do a simple filter for target records
        IncrementalValuesProvider<(INamedTypeSymbol, AttributeData)> targetRecords = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: IsRecordWithAttribute,
                transform: GetSemanticTargetForGeneration)
            .Where(static targetRecord => targetRecord != default);

        // Transform target records
        IncrementalValuesProvider<TypedValueMetadata> transformedRecords = targetRecords
            .Select(static (record, ct) => GetTypedValueMetadata(record.Item1, record.Item2, ct));

        // Deduplicate transform target records
        IncrementalValuesProvider<TypedValueMetadata> dedupedRecords = transformedRecords
            .Collect()
            .SelectMany(static (records, ct) => records.GroupBy(static record => record))
            .Select(static (records, ct) => records.Key);

        // Generate the source using the deduplicated target records
        context.RegisterSourceOutput(dedupedRecords, static (spc, source) => GenerateTypedValue(source, spc));
    }

    static bool IsRecordWithAttribute(SyntaxNode node, CancellationToken ct)
        => node is RecordDeclarationSyntax { AttributeLists.Count: > 0 };

    static (INamedTypeSymbol, AttributeData) GetSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken ct)
    {
        ISymbol? symbol = context.SemanticModel.GetDeclaredSymbol(context.Node);
        if (symbol is not INamedTypeSymbol recordSymbol)
        {
            return default;
        }

        if (!TryGetAttributeData(recordSymbol, context.SemanticModel, out AttributeData attributeData))
        {
            return default;
        }

        return (recordSymbol, attributeData);
    }

    static bool TryGetAttributeData(INamedTypeSymbol recordSymbol, SemanticModel semanticModel, out AttributeData attributeData)
    {
        foreach (AttributeData attribute in recordSymbol.GetAttributes())
        {
            if (attribute.AttributeClass is not null)
            {
                string metadataName = attribute.AttributeClass.ConstructedFrom.ToDisplayString();
                if (metadataName is TypedValueAttributeFullName or TypedValueAttributeFullName1)
                {
                    attributeData = attribute;
                    return true;
                }
            }
        }

        attributeData = null!;
        return false;
    }

    static TypedValueMetadata GetTypedValueMetadata(INamedTypeSymbol recordSymbol, AttributeData attributeData, CancellationToken ct)
    {
        var recordName = recordSymbol.Name;
        var recordNamespace = recordSymbol.ContainingNamespace.ToString();

        string valueType;
        bool isReferenceType;
        if (attributeData.ConstructorArguments.Length == 1
            && attributeData.ConstructorArguments[0].Value is INamedTypeSymbol type)
        {
            valueType = type.ToDisplayString(TypedValueFormat);
            isReferenceType = type.IsReferenceType;
        }
        else
        {
            Debug.Assert(attributeData.AttributeClass is not null);
            Debug.Assert(attributeData.AttributeClass!.TypeArguments.Length == 1);

            ITypeSymbol typeArgument = attributeData.AttributeClass.TypeArguments[0];
            valueType = typeArgument.ToDisplayString(TypedValueFormat);
            isReferenceType = typeArgument.IsReferenceType;
        }

        string valueName;
        if (attributeData.NamedArguments.Length == 1
            && attributeData.NamedArguments[0].Value.Value is string value)
        {
            Debug.Assert(attributeData.NamedArguments[0].Key == "ValueName");
            Debug.Assert(attributeData.NamedArguments[0].Value.Type!.SpecialType == SpecialType.System_String);

            valueName = value;
        }
        else
        {
            valueName = "Value";
        }

        return new TypedValueMetadata(recordName, recordNamespace, valueType, valueName, isReferenceType, recordSymbol.IsReadOnly);
    }

    static void GenerateTypedValue(TypedValueMetadata typedValueMetadata, SourceProductionContext context)
    {
        // generate the source code and add it to the output
        var typedValueSourceCode = TypedValueCodeBuilder.GetTypedValueGeneratedCode(typedValueMetadata);
        context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}.g.cs", SourceText.From(typedValueSourceCode, Encoding.UTF8));
        
        var systemTextJsonSerializationSourceCode = TypedValueCodeBuilder.GetSystemTextJsonSerializationGeneratedCode(typedValueMetadata);
        context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}SystemTextJson.g.cs", SourceText.From(systemTextJsonSerializationSourceCode, Encoding.UTF8));
    }
}
