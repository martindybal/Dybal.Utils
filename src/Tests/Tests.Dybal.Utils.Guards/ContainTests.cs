using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class ContainTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionContainValue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        Guard.Contain(source, 5);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<int>();
            
        void Act()
        {
            Guard.Contain(source, 5);
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
            Guard.Contain(source, 5);
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
            Guard.Contain(source, _ => false);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection does not contain required item. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_CollectionContainPredicate()
    {
        // Arrange
        var source = new [] { "a", "b", " " };

        // Act
        Guard.Contain(source, string.IsNullOrWhiteSpace);

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
            Guard.Contain(source, string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection does not contain required item. (Parameter 'source')", ex.Message);
    }
}