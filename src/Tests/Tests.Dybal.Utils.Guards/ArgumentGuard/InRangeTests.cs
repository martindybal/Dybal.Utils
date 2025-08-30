using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class InRangeTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_value_is_equal_to_min_and_inclusive()
    {
        // Arrange
        int value = 1;

        // Act
        int guardValue = Guard.Argument(value).InRange(1, 10);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void NotThrow_When_value_is_equal_to_max_and_inclusive()
    {
        // Arrange
        int value = 10;

        // Act
        int guardValue = Guard.Argument(value).InRange(1, 10);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Throw_ArgumentOutOfRangeException_When_value_is_less_than_min_inclusive()
    {
        // Arrange
        int value = 0;

        // Act
        void Act()
        {
            value = Guard.Argument(value).InRange(1, 10);
        }

        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(Act);
        Assert.Equal("Value of parameter 'value' (0) must be in the range [1, 10]. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void NotThrow_When_value_is_between_min_and_max_and_exclusive()
    {
        // Arrange
        int value = 5;

        // Act
        int guardValue = Guard.Argument(value).InRange(1, 10, inclusive: false);

        // Assert
        Assert.Equal(value, guardValue);
    }

    [Fact]
    public void Throw_ArgumentOutOfRangeException_When_value_is_equal_to_min_and_exclusive()
    {
        // Arrange
        int value = 1;

        // Act
        void Act()
        {
            value = Guard.Argument(value).InRange(1, 10, inclusive: false);
        }

        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(Act);
        Assert.Equal("Value of parameter 'value' (1) must be in the range (1, 10). (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void Throw_ArgumentOutOfRangeException_When_value_is_equal_to_max_and_exclusive()
    {
        // Arrange
        int value = 10;

        // Act
        void Act()
        {
            value = Guard.Argument(value).InRange(1, 10, inclusive: false);
        }

        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(Act);
        Assert.Equal("Value of parameter 'value' (10) must be in the range (1, 10). (Parameter 'value')", ex.Message);
    }
}

