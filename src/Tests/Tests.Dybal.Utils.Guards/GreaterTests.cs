using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class GreaterTests : TestBase
{
    [Fact]
    public void ShouldNotThrows_When_LeftIsGreaterThanRight_int()
    {
        // Arrange
        var left = 155;
        var right = 1;

        // Act
        Guard.Greater(left).Than(right);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsEqualToRight_int()
    {
        // Arrange
        var left = 155;
        var right = 155;

        void Act()
        {
            Guard.Greater(left).Than(right);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'left' (155) must be greater than value of parameter 'right' (155). (Parameter 'right')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsLessThanRight_int()
    {
        // Arrange
        var left = 1;
        var right = 155;

        void Act()
        {
            Guard.Greater(left).Than(right);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value of parameter 'left' (1) must be greater than value of parameter 'right' (155). (Parameter 'right')", ex.Message);
    }

    [Fact]
    public void ShouldNotThrows_When_LeftIsGreaterThanRight_DateTime()
    {
        // Arrange
        var left = DateTime.MaxValue;
        var right = DateTime.MinValue;

        // Act
        Guard.Greater(left).Than(right);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsEqualToRight_DateTime()
    {
        // Arrange
        var left = DateTime.MaxValue;
        var right = DateTime.MaxValue;

        void Act()
        {
            Guard.Greater(left).Than(right);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"Value of parameter 'left' ({left}) must be greater than value of parameter 'right' ({right}). (Parameter 'right')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_LeftIsLessThanRight_DateTime()
    {
        // Arrange
        var left = DateTime.MinValue;
        var right = DateTime.MaxValue;

        void Act()
        {
            Guard.Greater(left).Than(right);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal($"Value of parameter 'left' ({left}) must be greater than value of parameter 'right' ({right}). (Parameter 'right')", ex.Message);
    }
}