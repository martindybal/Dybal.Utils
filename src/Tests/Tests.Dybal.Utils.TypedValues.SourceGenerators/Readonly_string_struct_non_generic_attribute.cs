namespace Tests.Dybal.Utils.TypedValues.SourceGenerators;

public class Readonly_string_struct_non_generic_attribute : Should_generate_exact
{
    protected override string filename => "My.TypedValue.Tests.TestTypedReadonlyString.g.cs";

    protected override string code =>
        @"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue(typeof(string))]
    public readonly partial record struct TestTypedReadonlyString;
}";

    protected override string generated =>
        @"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedReadonlyString: global::Dybal.Utils.TypedValues.IReadonlyTypedValue<TestTypedReadonlyString, System.String>
    {
        private readonly System.String value;

        public System.String Value
        {
            get => this.value;
            init
            {
                ValidateValue(value);
                this.value = value;
            }
        }
        
        System.String global::Dybal.Utils.TypedValues.IReadonlyTypedValue<System.String>.Value
        {
            get => Value;
            init => Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        [System.Obsolete(""Use parametric a constructor instead"", true)]
        public TestTypedReadonlyString()
        {
            this.value = default(System.String)!;
        }

        public TestTypedReadonlyString(System.String value)
        {
            ValidateValue(value);
            this.value = value;
        }

        static partial void ValidateValue(System.String value);

        public void Deconstruct(out System.String value)
        {
            value = Value;
        }

        public int CompareTo(TestTypedReadonlyString other)
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