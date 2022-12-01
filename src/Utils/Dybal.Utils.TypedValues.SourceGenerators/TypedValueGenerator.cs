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

        var type = GetValueType(attributeData);
        var valueType = type.ToDisplayString(TypedValueFormat);
        var isReferenceType = type.IsReferenceType;

        var valueName = GetAttributeNamedArgumentValue<string>(attributeData, "ValueName") ?? "Value";

        var converters = GetConverters(attributeData);

        return new TypedValueMetadata(recordName, recordNamespace, valueType, valueName, converters, isReferenceType, recordSymbol.IsReadOnly);
    }

    private static ITypeSymbol GetValueType(AttributeData attributeData)
    {
        if (attributeData.ConstructorArguments.Length == 1 && attributeData.ConstructorArguments[0].Value is ITypeSymbol type)
        {
            return type;
        }

        Debug.Assert(attributeData.AttributeClass is not null);
        Debug.Assert(attributeData.AttributeClass!.TypeArguments.Length == 1);

        return attributeData.AttributeClass.TypeArguments[0];
    }

    private static Converters GetConverters(AttributeData attributeData)
    {
        var argumentValue = GetAttributeNamedArgumentValue<int?>(attributeData, "Converters");

        if (argumentValue.HasValue)
        {
            return (Converters)argumentValue;
        }

        //TODO Read from analyzer options
        return Converters.None;
    }

    private static TValue? GetAttributeNamedArgumentValue<TValue>(AttributeData attributeData, string argumentName)
    {
        var argument = attributeData.NamedArguments.SingleOrDefault(argument => argument.Key == argumentName);

        if (argument.Value.Value is TValue value)
        {
            return value;
        }

        return default;
    }

    static void GenerateTypedValue(TypedValueMetadata typedValueMetadata, SourceProductionContext context)
    {
        // generate the source code and add it to the output
        var typedValueSourceCode = TypedValueCodeBuilder.GetTypedValueGeneratedCode(typedValueMetadata);
        context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}.g.cs", SourceText.From(typedValueSourceCode, Encoding.UTF8));
        
        if (typedValueMetadata.Converters.HasFlag(Converters.SystemTextJson))
        {
            var systemTextJsonSerializationSourceCode = TypedValueCodeBuilder.GetSystemTextJsonSerializationGeneratedCode(typedValueMetadata);
            context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}SystemTextJson.g.cs", SourceText.From(systemTextJsonSerializationSourceCode, Encoding.UTF8));
        }
    }
}
