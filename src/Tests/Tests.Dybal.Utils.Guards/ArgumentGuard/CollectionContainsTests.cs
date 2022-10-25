using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class CollectionContainsTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionContainValue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var sourceWithFive = Guard.Argument(source).Contains(5);

        // Assert
        Assert.Equal(source, sourceWithFive);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<int>();

        void Act()
        {
            Guard.Argument(source).Contains(5);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain '5'. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainValue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };

        void Act()
        {
            Guard.Argument(source).Contains(5);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain '5'. (Parameter 'source')", ex.Message);
    }
}