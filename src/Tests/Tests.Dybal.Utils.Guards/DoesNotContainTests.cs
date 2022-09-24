using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class DoesNotContainTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<int>();

        // Act
        Guard.DoesNotContain(source, 5);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionDoesNotContainValue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };

        // Act
        Guard.DoesNotContain(source, 5);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionContainValue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        void Act()
        {
            Guard.DoesNotContain(source, 5);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot contain '5'. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_CollectionEmptyPredicate()
    {
        // Arrange
        var source = Array.Empty<string>();

        // Act
        Guard.DoesNotContain(source, s => false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionDoesNotContainPredicate()
    {
        // Arrange
        var source = new [] { "a", "b" };

        // Act
        Guard.DoesNotContain(source, string.IsNullOrWhiteSpace);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionContainPredicate()
    {
        // Arrange
        var source = new[] { "a", "b", " " };

        void Act()
        {
            Guard.DoesNotContain(source, string.IsNullOrWhiteSpace);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot contain ' '. (Parameter 'source')", ex.Message);
    }
}