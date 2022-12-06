using Xunit;

namespace Tests.Dybal.Utils.TypedValues;

public class StringReadonlyTypedValueTests: StringReadonlyTypedValueTestsBase<TestTypedReadonlyString>
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

    protected override TestTypedReadonlyString CreateTypedValue(string value)
    {
        return new TestTypedReadonlyString(value);
    }
}