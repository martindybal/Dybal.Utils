using Xunit;

namespace Tests.Dybal.Utils.TypedValues;

public class StringTypedValueTests : StringReadonlyTypedValueTestsBase<TestTypedString>
{

    [Fact]
    public void Init_Throw_ArgumentException_when_ValidateValue_fails()
    {
        // Act
        void Act()
        {
            var typedValue = CreateTypedValue("typed value");
            var _ = typedValue with { Value = " " };
        }

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }


    [Fact]
    public void Set_Throw_ArgumentException_when_ValidateValue_fails()
    {
        // Act
        void Act()
        {
            var typedValue = CreateTypedValue("typed value");
            typedValue.Value = " ";
        }

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    protected override TestTypedString CreateTypedValue(string value)
    {
        return new TestTypedString(value);
    }
}