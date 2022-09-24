using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class DoesNotContainNullTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<int>();

        // Act
        Guard.DoesNotContainNull(source);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionDoesNotContainNull()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4 };

        // Act
        Guard.DoesNotContainNull(source);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionContainNull()
    {
        // Arrange
        var source = new int?[] { 1, 2, 3, 4, 5, null };

        void Act()
        {
            Guard.DoesNotContainNull(source);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Collection cannot contain 'null'. (Parameter 'source')", ex.Message);
    }
}