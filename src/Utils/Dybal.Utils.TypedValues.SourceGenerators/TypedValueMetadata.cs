namespace Dybal.Utils.TypedValues.SourceGenerators
{
    public readonly record struct TypedValueMetadata
    {
        public TypedValueMetadata(string name, string @namespace, string valueType, string valueName, Converters? converters, bool isValueReferenceType, bool @readonly, string? validationMethodName)
        {
            this.Name = name;
            this.Namespace = @namespace;
            this.ValueType = valueType;
            this.ValueName = valueName;
            this.Converters = converters;
            this.IsValueReferenceType = isValueReferenceType;
            this.Readonly = @readonly;
            this.ValidationMethodName = validationMethodName;
        }
        
        public string Name { get; }
        public string Namespace { get; }
        public string ValueType { get; }
        public string ValueName { get; }
        public Converters? Converters { get; }
        public bool IsValueReferenceType { get; }
        public bool Readonly { get; }
        public string? ValidationMethodName { get; }
    }
}