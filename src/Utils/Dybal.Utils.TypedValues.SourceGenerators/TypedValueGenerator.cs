using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Dybal.Utils.TypedValues.SourceGenerators;

[Generator]
public class TypedValueGenerator : IIncrementalGenerator
{
    private const string TypedValueAttributeFullName = "Dybal.Utils.TypedValues.TypedValueAttribute";
    private const string GenericTypedValueAttributeFullName = "Dybal.Utils.TypedValues.TypedValueAttribute<TValue>";

    private static readonly SymbolDisplayFormat TypedValueFormat = new(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters);
    
    private static TypedValueGeneratorOptions? config = null;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {

        var files = context.AdditionalTextsProvider
            .Where(file => file.Path.EndsWith("Dybal.Utils.TypedValues.json"))
            .Select((file, ct) => new { file.Path, Content = file.GetText(ct)!.ToString() })
            .Select((file, ct) => file.Path);

        // Do a simple filter for target records
        var targetRecords = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: IsRecordWithAttribute,
                transform: GetSemanticTargetForGeneration)
            .Where(static targetRecord => targetRecord != default);

        // Transform target records
        var transformedRecords = targetRecords
            .Select(static (record, ct) => GetTypedValueMetadata(record.Item1, record.Item2));

        // Deduplicate transform target records
        var dedupedRecords = transformedRecords
            .Collect()
            .SelectMany(static (records, ct) => records.GroupBy(static record => record))
            .Select(static (records, ct) => records.Key)
            .Combine(files.Collect());

        // Generate the source using the deduplicated target records
        context.RegisterSourceOutput(dedupedRecords, static (spc, source) => Execute(source.Left, source.Right.FirstOrDefault(), spc));
    }

    private static void Execute(TypedValueMetadata typedValueMetadata, string? configFilePath, SourceProductionContext context)
    {
        config ??= ReadConfigFromJson(configFilePath) ?? DefaultConfig();
        GenerateTypedValue(context, typedValueMetadata);
    }
    
    private static TypedValueGeneratorOptions? ReadConfigFromJson(string? configFilePath)
    {
        try
        {
            if (configFilePath is not null)
            {
                var options = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                };

                var configJson = File.ReadAllText(configFilePath);
                return JsonSerializer.Deserialize<TypedValueGeneratorOptions>(configJson, options);
            }
        }
        catch { }
        return null;
    }

    private static TypedValueGeneratorOptions DefaultConfig()
    {
        return new TypedValueGeneratorOptions(Converters.None);
    }
    
    static bool IsRecordWithAttribute(SyntaxNode node, CancellationToken ct)
        => node is RecordDeclarationSyntax { AttributeLists.Count: > 0 };

    static (INamedTypeSymbol, AttributeData) GetSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken ct)
    {
        var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node);
        if (symbol is not INamedTypeSymbol recordSymbol)
        {
            return default;
        }

        if (!TryGetAttributeData(recordSymbol, out var attributeData))
        {
            return default;
        }

        return (recordSymbol, attributeData);
    }

    static bool TryGetAttributeData(INamedTypeSymbol recordSymbol, out AttributeData attributeData)
    {
        foreach (var attribute in recordSymbol.GetAttributes())
        {
            if (attribute.AttributeClass is not null)
            {
                var metadataName = attribute.AttributeClass.ConstructedFrom.ToDisplayString();
                if (metadataName is TypedValueAttributeFullName or GenericTypedValueAttributeFullName)
                {
                    attributeData = attribute;
                    return true;
                }
            }
        }

        attributeData = null!;
        return false;
    }

    static TypedValueMetadata GetTypedValueMetadata(INamedTypeSymbol recordSymbol, AttributeData attributeData)
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

    private static Converters? GetConverters(AttributeData attributeData)
    {
        var argumentValue = GetAttributeNamedArgumentValue<int?>(attributeData, "Converters");

        if (argumentValue.HasValue)
        {
            return (Converters)argumentValue;
        }

        return null;
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
    
    static void GenerateTypedValue(SourceProductionContext context, TypedValueMetadata typedValueMetadata)
    {
        var converters = typedValueMetadata.Converters ?? config!.Converters;

        // generate the source code and add it to the output
        var typedValueSourceCode = TypedValueCodeBuilder.GetTypedValueGeneratedCode(typedValueMetadata);
        context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}.g.cs", SourceText.From(typedValueSourceCode, Encoding.UTF8));

        if (converters.HasFlag(Converters.SystemTextJson))
        {
            var systemTextJsonSerializationSourceCode = TypedValueCodeBuilder.GetSystemTextJsonSerializationGeneratedCode(typedValueMetadata);
            context.AddSource($"{typedValueMetadata.Namespace}.{typedValueMetadata.Name}SystemTextJson.g.cs", SourceText.From(systemTextJsonSerializationSourceCode, Encoding.UTF8));
        }
    }
}