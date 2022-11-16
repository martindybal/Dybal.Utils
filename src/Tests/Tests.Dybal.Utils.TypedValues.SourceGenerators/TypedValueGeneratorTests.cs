using Xunit;
using VerifyCS = Tests.Dybal.Utils.TypedValues.SourceGenerators.Verifiers.CSharpSourceGeneratorVerifier<Dybal.Utils.TypedValues.SourceGenerators.TypedValueGenerator>;

namespace Tests.Dybal.Utils.TypedValues.SourceGenerators
{
    public class TypedValueGeneratorTests
    {
        [Fact]
        public async Task Generate_writable_struct_generic_attribute()
        {
            string code =
@"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue<string>]    
    public partial record struct TestTypedString;
}";

            string generated =
@"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedString: global::Dybal.Utils.TypedValues.ITypedValue<TestTypedString, System.String>
    {
        private System.String value;

        public static implicit operator System.String(TestTypedString typed) => typed.Value;

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
            this.value = null!;
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

            await VerifyCS.VerifyGeneratorAsync(code, ("My.TypedValue.Tests.TestTypedString.g.cs", generated));
        }

        [Fact]
        public async Task Generate_readonly_struct_generic_attribute()
        {
            string code =
@"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue<string>]
    public readonly partial record struct TestTypedReadonlyString;
}";

            string generated =
@"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedReadonlyString: global::Dybal.Utils.TypedValues.IReadonlyTypedValue<TestTypedReadonlyString, System.String>
    {
        private readonly System.String value;

        public static implicit operator System.String(TestTypedReadonlyString typed) => typed.Value;

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
            this.value = null!;
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

            await VerifyCS.VerifyGeneratorAsync(code, ("My.TypedValue.Tests.TestTypedReadonlyString.g.cs", generated));
        }
    
        [Fact]
        public async Task Generate_writable_struct_non_generic_attribute()
        {
            string code =
@"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue(typeof(string))]    
    public partial record struct TestTypedString;
}";

            string generated =
@"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedString: global::Dybal.Utils.TypedValues.ITypedValue<TestTypedString, System.String>
    {
        private System.String value;

        public static implicit operator System.String(TestTypedString typed) => typed.Value;

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
            this.value = null!;
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

            await VerifyCS.VerifyGeneratorAsync(code, ("My.TypedValue.Tests.TestTypedString.g.cs", generated));
        }

        [Fact]
        public async Task Generate_readonly_struct_non_generic_attribute()
        {
            string code =
@"using System;

namespace My.TypedValue.Tests
{
    [global::Dybal.Utils.TypedValues.TypedValue(typeof(string))]    
    public readonly partial record struct TestTypedReadonlyString;
}";

            string generated =
@"namespace My.TypedValue.Tests
{
    public partial record struct TestTypedReadonlyString: global::Dybal.Utils.TypedValues.IReadonlyTypedValue<TestTypedReadonlyString, System.String>
    {
        private readonly System.String value;

        public static implicit operator System.String(TestTypedReadonlyString typed) => typed.Value;

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
            this.value = null!;
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

            await VerifyCS.VerifyGeneratorAsync(code, ("My.TypedValue.Tests.TestTypedReadonlyString.g.cs", generated));
        }
    }
}