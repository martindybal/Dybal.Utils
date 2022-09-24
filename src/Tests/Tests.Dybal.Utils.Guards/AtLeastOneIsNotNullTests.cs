using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class AtLeastOneIsNotNullTests : TestBase
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(1, null, null)]
    [InlineData("A", 1, null)]
    [InlineData("A", null, 1)]
    [InlineData("A", null, null)]
    [InlineData(null, "A", null)]
    [InlineData(null, null, "A")]
    public void Should_NotThrows_When_AtLeastOneIsNotNull_3parameters(object value1, object value2, object value3)
    {
        // Arrange
        var sample = new
        {
            Value1 = value1,
            Value2 = value2,
            Value3 = value3
        };

        // Act
        Guard.AtLeastOneIsNotNull(
            sample.Value1, sample.Value2, sample.Value3
        );

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_AllObjectsAreNull_3parameters()
    {
        // Arrange
        var sample = new
        {
            Value1 = (int?)null,
            Value2 = (string?)null,
            Value3 = (object?)null
        };
            
        void Act()
        {
            Guard.AtLeastOneIsNotNull(
                sample.Value1, sample.Value2, sample.Value3
            );
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Some parameters sample.Value1, sample.Value2, sample.Value3 must be not null.", ex.Message);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(1, null)]
    [InlineData(null, 1)]
    public void Should_NotThrows_When_AtLeastOneIsNotNull_2parameters(object value1, object value2)
    {
        // Arrange
        var sample = new
        {
            Value1 = value1,
            Value2 = value2
        };

        // Act
        Guard.AtLeastOneIsNotNull(
            sample.Value1, sample.Value2
        );

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_AllObjectsAreNull_2parameters()
    {
        // Arrange
        var sample = new
        {
            Value1 = (int?)null,
            Value2 = (string?)null
        };
            
        void Act()
        {
            Guard.AtLeastOneIsNotNull(
                sample.Value1, sample.Value2
            );
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Some parameters sample.Value1, sample.Value2 must be not null.", ex.Message);
    }
}