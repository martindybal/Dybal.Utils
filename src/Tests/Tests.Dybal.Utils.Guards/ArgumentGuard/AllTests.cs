using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class AllTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_AllItemsMatchPredicate()
    {
        // Arrange
        var source = new[] { "  ", " " };

        // Act
        Guard.Argument(source).All(string.IsNullOrWhiteSpace);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<string>();

        // Act
        Guard.Argument(source).All(static _ => false);
        
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
            Guard.Argument(source).All(string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("All items of collection must match predicate. (Parameter 'source')", ex.Message);
    }
}