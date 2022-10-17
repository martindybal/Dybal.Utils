using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ContainTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionContainValue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var sourceWithFive = Guard.Argument(source).Contain(5);

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
            Guard.Argument(source).Contain(5);
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
            Guard.Argument(source).Contain(5);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain '5'. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmptyPredicate()
    {
        // Arrange
        var source = Array.Empty<string>();

        void Act()
        {
            Guard.Argument(source).Contain(static _ => false);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection does not contain required item. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_CollectionContainPredicate()
    {
        // Arrange
        var source = new[] { "a", "b", " " };

        // Act
        Guard.Argument(source).Contain(string.IsNullOrWhiteSpace);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainPredicate()
    {
        // Arrange
        var source = new[] { "a", "b" };

        void Act()
        {
            Guard.Argument(source).Contain(string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection does not contain required item. (Parameter 'source')", ex.Message);
    }
}