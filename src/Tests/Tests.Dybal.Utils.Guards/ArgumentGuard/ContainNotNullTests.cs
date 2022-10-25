using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class ContainNotNullTests : UnitTestsBase
{
    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<int>();

        void Act()
        {
            Guard.Argument(source).ContainsNotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain an item with not default value. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_CollectionContainNotNull()
    {
        // Arrange
        var source = new int?[] { 1, null, null };

        // Act
        Guard.Argument(source).ContainsNotNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionContainOnlyNull()
    {
        // Arrange
        var source = new int?[] { null };

        void Act()
        {
            Guard.Argument(source).ContainsNotNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain an item with not default value. (Parameter 'source')", ex.Message);
    }
}