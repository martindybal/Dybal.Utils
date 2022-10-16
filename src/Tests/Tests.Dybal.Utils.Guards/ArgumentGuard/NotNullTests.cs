using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotNullTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_VariableHasValue()
    {
        // Arrange
        object value = "value";

        // Act
        Guard.Argument(value).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableStructVariableHasValue()
    {
        // Arrange
        int? value = 0;

        // Act
        Guard.Argument(value).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyHasValue()
    {
        // Arrange
        var sample = new { Value = (object?)"value" };

        // Act
        Guard.Argument(sample.Value).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_VariableWithoutValue()
    {
        // Arrange
        object? value = null;

        void Act()
        {
            Guard.Argument(value).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_NullableStructVariableWithoutValue()
    {
        // Arrange
        int? value = null;

        void Act()
        {
            Guard.Argument(value).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_PropertyWithoutValue()
    {
        // Arrange
        var sample = new { Value = (object?)null };

        void Act()
        {
            Guard.Argument(sample.Value).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_GuardIsNotActive()
    {
        // Arrange
        object? value = null;

        // Act
        Guard.Argument(value).If(false).NotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_GuardIsActive()
    {
        // Arrange
        object? value = null;

        void Act()
        {
            Guard.Argument(value).If(true).NotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'value')", ex.Message);
    }


    [Fact]
    public void ShouldThrows_NonNullable_When_NonNullableArgument()
    {
        // Arrange
        object value = "value";

        // Act
        value = Guard.Argument(value).NotNull();

        // Assert
        AssertNonNullableType(value);
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_NonNullable_When_NullableArgument()
    {
        // Arrange
        object? nullableValue = null;
        
        void Act()
        {
            object value = Guard.Argument(nullableValue).NotNull();
            AssertNonNullableType(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'nullableValue')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_Nullable_When_NonNullableArgument_After_If()
    {
        // Arrange
        object value = "value";

        // Act
        //NotNull after If has to return nullable TArgument? insteadof TArgument
        var nullableValue = Guard.Argument(value).If(false).NotNull();

        // Assert
        var assertNullableType = (1, nullableValue);
        // doesn't break build with error CS8625: Cannot convert null literal to non-nullable reference type.
        assertNullableType.nullableValue = null;
    }

    [Fact]
    public void ShouldThrows_Nullable_When_NullableArgument_After_If()
    {
        // Arrange
        object? nullableValue = null;

        // Act
        var returnValue = Guard.Argument(nullableValue).If(false).NotNull(); // should be nullable

        // Assert
        var assertNullableType = (1, returnValue);
        // doesn't break build with error CS8625: Cannot convert null literal to non-nullable reference type.
        assertNullableType.returnValue = null;
    }

    private void AssertNonNullableType(object nonNullableValue)
    {
        // doesn't break build with error CS8600: Converting null literal or possible null value to non-nullable type.
        // doesn't break build with error CS8603: Possible null reference return.
        // doesn't break build with error CS8604: Possible null reference argument for parameter
    }
}