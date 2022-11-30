namespace Tests.Dybal.Utils.TypedValues.SourceGenerators;

public class Writable_string_struct_generic_attribute : Should_generate_exact
{
    protected override string filename => "My.TypedValue.Tests.TestTypedString.g.cs";

    protected override string code =>
        @"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue<string>]    
    public partial record struct TestTypedString;
}";

    protected override string generated =>
        @"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedString: global::Dybal.Utils.TypedValues.ITypedValue<TestTypedString, System.String>
    {
        private System.String value;

        public System.String Value
        {
            get => this.value;
            set
            {
                ValidateValue(value);
                this.value = value;
            }
        }
        
        System.String global::Dybal.Utils.TypedValues.ITypedValue<System.String>.Value
        {
            get => Value;
            set => Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        [System.Obsolete(""Use parametric a constructor instead"", true)]
        public TestTypedString()
        {
            this.value = default(System.String)!;
        }

        public TestTypedString(System.String value)
        {
            ValidateValue(value);
            this.value = value;
        }

        static partial void ValidateValue(System.String value);

        public void Deconstruct(out System.String value)
        {
            value = Value;
        }

        public int CompareTo(TestTypedString other)
        {
            return (Value, other.Value) switch
            {
                (null, null) => 0,
                (null, _) => -1,
                (_, null) => 1,
                (_, _) => Value.CompareTo(other.Value),
            };
        }
    }
}";
}