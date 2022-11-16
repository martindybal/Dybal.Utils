using Newtonsoft.Json.Linq;
using System.Runtime.Intrinsics.X86;

namespace Tests.Dybal.Utils.TypedValues.SourceGenerators;

public class Writable_int_struct_generic_attribute : Should_generate_exact
{
    protected override string filename => "My.TypedValue.Tests.TestTypedInt.g.cs";

    protected override string code =>
@"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue<int>(ValueName = ""Percentage"")]
    public partial record struct TestTypedInt;
}";

    protected override string generated =>
@"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedInt: global::Dybal.Utils.TypedValues.ITypedValue<TestTypedInt, System.Int32>
    {
        private System.Int32 percentage;

        public static implicit operator System.Int32(TestTypedInt typed) => typed.Percentage;

        public System.Int32 Percentage
        {
            get => this.percentage;
            set
            {
                ValidatePercentage(value);
                this.percentage = value;
            }
        }
        
        System.Int32 global::Dybal.Utils.TypedValues.ITypedValue<System.Int32>.Value
        {
            get => Percentage;
            set => Percentage = value;
        }

        public override int GetHashCode()
        {
            return Percentage.GetHashCode();
        }

        [System.Obsolete(""Use parametric a constructor instead"", true)]
        public TestTypedInt()
        {
            this.percentage = default(System.Int32)!;
        }

        public TestTypedInt(System.Int32 percentage)
        {
            ValidatePercentage(percentage);
            this.percentage = percentage;
        }

        static partial void ValidatePercentage(System.Int32 percentage);

        public void Deconstruct(out System.Int32 percentage)
        {
            percentage = Percentage;
        }

        public int CompareTo(TestTypedInt other)
        {
            return Percentage.CompareTo(other.Percentage);
        }
    }
}";
}