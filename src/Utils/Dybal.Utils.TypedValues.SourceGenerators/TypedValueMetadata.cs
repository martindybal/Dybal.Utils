namespace Dybal.Utils.TypedValues.SourceGenerators
{
    public readonly record struct TypedValueMetadata
    {
        public TypedValueMetadata(string name, string @namespace, Type valueType, string valueName, bool @readonly)
        {
            this.Name = name;
            this.Namespace = @namespace;
            this.ValueType = valueType;
            this.ValueName = valueName;
            this.Readonly = @readonly;
        }

        public string Name { get; }
        public string Namespace { get; }
        public Type ValueType { get; }
        public string ValueName { get; }
        public bool Readonly { get; }

        public void Deconstruct(out string Name, out string Namespace, out Type ValueType, out string ValueName, out bool Readonly)
        {
            Name = this.Name;
            Namespace = this.Namespace;
            ValueType = this.ValueType;
            ValueName = this.ValueName;
            Readonly = this.Readonly;
        }
    }
}