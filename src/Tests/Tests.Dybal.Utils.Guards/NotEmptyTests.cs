using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotEmptyTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionNotEmpty()
    {
        // Arrange
        var source = new[] { 5 };

        // Act
        Guard.NotEmpty(source);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_CollectionNull()
    {
        // Arrange
        int[]? source = null;
            
        void Act()
        {
            Guard.NotEmpty(source);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmpty()
    {
        // Arrange
        var source = new int[] { };
            
        void Act()
        {
            Guard.NotEmpty(source);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot be empty. (Parameter 'source')", ex.Message);
    }
}