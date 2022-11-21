namespace Dybal.Utils.TypedValues.SourceGenerators;

public static class TypedValueCodeBuilder
{
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

        public static implicit operator {metadata.ValueType}({metadata.Name} typed) => typed.{metadata.ValueName};

        public {metadata.ValueType} {metadata.ValueName}
        {{
            get => this.{valueParameterName};
            {typedValuePropertySetter}
            {{
                Validate{metadata.ValueName}(value);
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
            Validate{metadata.ValueName}({valueParameterName});
            this.{valueParameterName} = {valueParameterName};
        }}

        static partial void Validate{metadata.ValueName}({metadata.ValueType} {valueParameterName});

        public void Deconstruct(out {metadata.ValueType} {valueParameterName})
        {{
            {valueParameterName} = {metadata.ValueName};
        }}

        public int CompareTo({metadata.Name} other)
        {{
            {(metadata.ReferenceType ? ReferenceTypeCompareTo(metadata.ValueName) : ValueTypeCompareTo(metadata.ValueName))}
        }}
    }}
}}";
        return source;
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