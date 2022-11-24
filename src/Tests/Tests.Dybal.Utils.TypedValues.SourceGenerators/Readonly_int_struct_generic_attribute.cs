namespace Tests.Dybal.Utils.TypedValues.SourceGenerators;

public class Readonly_int_struct_generic_attribute : Should_generate_exact
{
    protected override string filename => "My.TypedValue.Tests.TestTypedReadonlyInt.g.cs";

    protected override string code =>
        @"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue<int>(ValueName = ""Percentage"")]
    public readonly partial record struct TestTypedReadonlyInt;
}";

    protected override string generated =>
@"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedReadonlyInt: global::Dybal.Utils.TypedValues.IReadonlyTypedValue<TestTypedReadonlyInt, System.Int32>
    {
        private readonly System.Int32 percentage;

        public static implicit operator System.Int32(TestTypedReadonlyInt typed) => typed.Percentage;

        public System.Int32 Percentage
        {
            get => this.percentage;
            init
            {
                ValidatePercentage(value);
                this.percentage = value;
            }
        }
        
        System.Int32 global::Dybal.Utils.TypedValues.IReadonlyTypedValue<System.Int32>.Value
        {
            get => Percentage;
            init => Percentage = value;
        }

        public override int GetHashCode()
        {
            return Percentage.GetHashCode();
        }

        [System.Obsolete(""Use parametric a constructor instead"", true)]
        public TestTypedReadonlyInt()
        {
            this.percentage = default(System.Int32)!;
        }

        public TestTypedReadonlyInt(System.Int32 percentage)
        {
            ValidatePercentage(percentage);
            this.percentage = percentage;
        }

        static partial void ValidatePercentage(System.Int32 percentage);

        public void Deconstruct(out System.Int32 percentage)
        {
            percentage = Percentage;
        }

        public int CompareTo(TestTypedReadonlyInt other)
        {
            return Percentage.CompareTo(other.Percentage);
        }
    }
}";
}