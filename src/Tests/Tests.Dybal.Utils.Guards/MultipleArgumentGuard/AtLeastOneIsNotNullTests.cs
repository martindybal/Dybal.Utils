using Dybal.Utils.Guards;
using Tests.Dybal.Utils.Guards.ArgumentGuard;
using Xunit;

namespace Tests.Dybal.Utils.Guards.MultipleArgumentGuard;

public class AtLeastOneIsNotNullTests : UnitTestsBase
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(1, null, null)]
    [InlineData("A", 1, null)]
    [InlineData("A", null, 1)]
    [InlineData("A", null, null)]
    [InlineData(null, "A", null)]
    [InlineData(null, null, "A")]
    public void Should_NotThrows_When_AtLeastOneIsNotNull(object value1, object value2, object value3)
    {
        // Arrange
        var sample = new
        {
            Value1 = value1,
            Value2 = value2,
            Value3 = value3
        };

        // Act
        Guard.Arguments(sample.Value1, sample.Value2, sample.Value3).AtLeastOneIsNotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_AllObjectsAreNull()
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
            Guard.Arguments(sample.Value1, sample.Value2, sample.Value3).AtLeastOneIsNotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Some of sample.Value1, sample.Value2, sample.Value3 must be not null. (Parameter 'sample.Value1, sample.Value2, sample.Value3')", ex.Message);
    }
}