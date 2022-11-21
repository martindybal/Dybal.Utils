namespace Dybal.Utils.TypedValues.SourceGenerators
{
    public readonly record struct TypedValueMetadata
    {
        public TypedValueMetadata(string name, string @namespace, string valueType, string valueName, bool referenceType, bool @readonly)
        {
            this.Name = name;
            this.Namespace = @namespace;
            this.ValueType = valueType;
            this.ValueName = valueName;
            this.ReferenceType = referenceType;
            this.Readonly = @readonly;
        }

        public string Name { get; }
        public string Namespace { get; }
        public string ValueType { get; }
        public string ValueName { get; }
        public bool ReferenceType { get; }
        public bool Readonly { get; }

        public void Deconstruct(out string Name, out string Namespace, out string ValueType, out string ValueName, out bool ReferenceType, out bool Readonly)
        {
            Name = this.Name;
            Namespace = this.Namespace;
            ValueType = this.ValueType;
            ValueName = this.ValueName;
            ReferenceType = this.ReferenceType;
            Readonly = this.Readonly;
        }
    }
}