using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class AnyTests : UnitTestsBase
{
    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmptyPredicate()
    {
        // Arrange
        var source = Array.Empty<string>();

        void Act()
        {
            Guard.Argument(source).Any(static _ => false);
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
        Guard.Argument(source).Any(string.IsNullOrWhiteSpace);

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
            Guard.Argument(source).Any(string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection does not contain required item. (Parameter 'source')", ex.Message);
    }
}