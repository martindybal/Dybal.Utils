using Dybal.Utils.Guards;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class ThanIfTests : TestBase
{
    [Fact]
    public void ShouldNotThrows_When_LeftIsGreaterThanRight()
    {
        // Arrange
        var left = 155;
        var right = 1;

        // Act
        Guard.Greater(left).ThanIf(right, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldNotThrows_When_LeftIsLessThanRight_ConditionFalse()
    {
        // Arrange
        var left = 1;
        var right = 155;

        // Act
        Guard.Greater(left).ThanIf(right, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsEqualToRight()
    {
        // Arrange
        var left = 155;
        var right = 155;

        void Act()
        {
            Guard.Greater(left).ThanIf(right, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'left' (155) must be greater than value of parameter 'right' (155). (Parameter 'right')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsLessThanRight()
    {
        // Arrange
        var left = 1;
        var right = 155;

        void Act()
        {
            Guard.Greater(left).ThanIf(right, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'left' (1) must be greater than value of parameter 'right' (155). (Parameter 'right')", ex.Message);
    }
}

public class ThanIfNullableTests : TestBase
{
    [Fact]
    public void ShouldNotThrows_When_LeftIsGreaterThanRight()
    {
        // Arrange
        var left = 155;
        int? right = 1;

        // Act
        Guard.Greater(left).ThanIf(right, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldNotThrows_When_LeftIsLessThanRight_ConditionFalse()
    {
        // Arrange
        var left = 1;
        int? right = 155;

        // Act
        Guard.Greater(left).ThanIf(right, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsEqualToRight()
    {
        // Arrange
        var left = 155;
        int? right = 155;

        void Act()
        {
            Guard.Greater(left).ThanIf(right, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'left' (155) must be greater than value of parameter 'right' (155). (Parameter 'right')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsLessThanRight()
    {
        // Arrange
        var left = 1;
        int? right = 155;

        void Act()
        {
            Guard.Greater(left).ThanIf(right, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'left' (1) must be greater than value of parameter 'right' (155). (Parameter 'right')", ex.Message);
    }

    [Fact]
    public void ShouldNotThrows_When_RightIsNull_ConditionFalse()
    {
        // Arrange
        var left = 1;
        int? right = null;

        //Act
        Guard.Greater(left).ThanIf(right, right.HasValue);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_When_RightIsNull_ConditionTrue()
    {
        // Arrange
        var left = 1;
        int? right = null;

        void Act()
        {
            Guard.Greater(left).ThanIf(right, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'right')", ex.Message);
    }
}