﻿namespace Dybal.Utils.TypedValues.SourceGenerators;

public static class TypedValueCodeBuilder
{
    public static string GetTypedValueGeneratedCode(TypedValueMetadata metadata)
    {
        var typedValueInterfaceName = metadata.Readonly ? "IReadonlyTypedValue" : "ITypedValue";
        var typedValuePropertySetter = metadata.Readonly ? "init" : "set";
        var valueParameterName = ToParameterName(metadata.ValueName);

        var source = $@"namespace {metadata.Namespace}
{{
    public partial record struct {metadata.Name}: global::Dybal.Utils.TypedValues.{typedValueInterfaceName}<{metadata.Name}, {metadata.ValueType.FullName}>
    {{
        private {(metadata.Readonly ? "readonly " : string.Empty)}{metadata.ValueType.FullName} {valueParameterName};

        public static implicit operator {metadata.ValueType.FullName}({metadata.Name} typed) => typed.{metadata.ValueName};

        public {metadata.ValueType.FullName} {metadata.ValueName}
        {{
            get => this.{valueParameterName};
            {typedValuePropertySetter}
            {{
                ValidateValue(value);
                this.{valueParameterName} = value;
            }}
        }}
        
        {metadata.ValueType.FullName} global::Dybal.Utils.TypedValues.{typedValueInterfaceName}<{metadata.ValueType.FullName}>.Value
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
            this.{valueParameterName} = null!;
        }}

        public {metadata.Name}({metadata.ValueType.FullName} {valueParameterName})
        {{
            Validate{metadata.ValueName}({valueParameterName});
            this.{valueParameterName} = {valueParameterName};
        }}

        static partial void Validate{metadata.ValueName}({metadata.ValueType.FullName} {valueParameterName});

        public void Deconstruct(out {metadata.ValueType.FullName} {valueParameterName})
        {{
            {valueParameterName} = {metadata.ValueName};
        }}

        public int CompareTo({metadata.Name} other)
        {{
            return ({metadata.ValueName}, other.{metadata.ValueName}) switch
            {{
                (null, null) => 0,
                (null, _) => -1,
                (_, null) => 1,
                (_, _) => {metadata.ValueName}.CompareTo(other.{metadata.ValueName}),
            }};
        }}
    }}
}}";
        return source;
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