namespace Dybal.Utils.TypedValues.SourceGenerators;

public static class TypedValueCodeBuilder
{
    public static string GetSystemTextJsonSerializationGeneratedCode(TypedValueMetadata metadata)
    {
        var source = $@"namespace {metadata.Namespace}
{{
    [global::System.Text.Json.Serialization.JsonConverter(typeof({metadata.Name}SystemTextJsonConverter))]
    public partial record struct {metadata.Name}
    {{

    }}

    class {metadata.Name}SystemTextJsonConverter : global::System.Text.Json.Serialization.JsonConverter<{metadata.Name}>
    {{
        public override {metadata.Name} Read(ref global::System.Text.Json.Utf8JsonReader reader, global::System.Type typeToConvert, global::System.Text.Json.JsonSerializerOptions options)
        {{
            var value = global::System.Text.Json.JsonSerializer.Deserialize<{metadata.ValueType}>(ref reader, options);
            return new {metadata.Name}(value);
        }}
        
        public override void Write(global::System.Text.Json.Utf8JsonWriter writer, {metadata.Name} value, global::System.Text.Json.JsonSerializerOptions options)
        {{
            var jsonValue = global::System.Text.Json.JsonSerializer.Serialize(value.Value, options);
            writer.WriteRawValue(jsonValue);
        }}
    }}
}}";
        return source;
    }


    public static string GetTypedValueGeneratedCode(TypedValueMetadata metadata)
    {
        var typedValueInterfaceName = metadata.Readonly ? "IReadonlyTypedValue" : "ITypedValue";
        var typedValuePropertySetter = metadata.Readonly ? "init" : "set";
        var valueParameterName = ToParameterName(metadata.ValueName);

        var source = $@"namespace {metadata.Namespace}
{{
    public partial record struct {metadata.Name}: global::Dybal.Utils.TypedValues.{typedValueInterfaceName}<{metadata.Name}, {metadata.ValueType}>
    {{
        private {(metadata.Readonly ? "readonly " : string.Empty)}{metadata.ValueType} {valueParameterName};

        public {metadata.ValueType} {metadata.ValueName}
        {{
            get => this.{valueParameterName};
            {typedValuePropertySetter}
            {{
                {RenderValidation(metadata)}
                this.{valueParameterName} = value;
            }}
        }}
        
        {metadata.ValueType} global::Dybal.Utils.TypedValues.{typedValueInterfaceName}<{metadata.ValueType}>.Value
        {{
            get => {metadata.ValueName};
            {typedValuePropertySetter} => {metadata.ValueName} = value;
        }}

        public override int GetHashCode()
        {{
            return {metadata.ValueName}.GetHashCode();
        }}

        [System.Obsolete(""Use parametric a constructor instead"", true)]
        public {metadata.Name}()
        {{
            this.{valueParameterName} = default({metadata.ValueType})!;
        }}

        public {metadata.Name}({metadata.ValueType} {valueParameterName})
        {{
            {RenderValidation(metadata)}
            this.{valueParameterName} = {valueParameterName};
        }}

        public void Deconstruct(out {metadata.ValueType} {valueParameterName})
        {{
            {valueParameterName} = {metadata.ValueName};
        }}

        public int CompareTo({metadata.Name} other)
        {{
            {(metadata.IsValueReferenceType ? ReferenceTypeCompareTo(metadata.ValueName) : ValueTypeCompareTo(metadata.ValueName))}
        }}
    }}
}}";
        return source;
    }

    private static string RenderValidation(TypedValueMetadata metadata)
    {
        return metadata.ValidationMethodName is not null ? $"{metadata.ValidationMethodName}(value);" : string.Empty;
    }

    private static string ValueTypeCompareTo(string valueName)
    {
        return $"return {valueName}.CompareTo(other.{valueName});";
    }

    private static string ReferenceTypeCompareTo(string valueName)
    {
        return 
            @$"return ({valueName}, other.{valueName}) switch
            {{
                (null, null) => 0,
                (null, _) => -1,
                (_, null) => 1,
                (_, _) => {valueName}.CompareTo(other.{valueName}),
            }};";
    }

    private static string ToParameterName(string name)
    {
        if (!char.IsUpper(name[0]))
        {
            return name;
        }

        if (name.Length == 1)
        {
            return char.ToLower(name[0]).ToString();
        }

        return char.ToLower(name[0]) + name.Substring(1);
    }
}