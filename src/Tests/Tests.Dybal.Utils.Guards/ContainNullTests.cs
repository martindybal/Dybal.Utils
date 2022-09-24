using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class ContainNullTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionContainNull()
    {
        // Arrange
        var source = new int?[] { 1, 2, 3, 4, null };
            
        // Act
        Guard.ContainNull(source);

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
            Guard.ContainNull(source);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainNull()
    {
        // Arrange
        var source = new [] { 1, 2, 3, 4, 5 };

        void Act()
        {
            Guard.ContainNull(source);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to contain null. (Parameter 'source')", ex.Message);
    }
}