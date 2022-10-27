using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class DoesNotContainNullTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionContainNull()
    {
        // Arrange
        var source = new [] { 1, 2, 3, 4 };

        // Act
        Guard.Argument(source).DoesNotContainNull();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionEmpty()
    {
        // Arrange
        var source = Array.Empty<int>();

        // Act
        Guard.Argument(source).DoesNotContainNull();

        // Assert
        // doesn't throw any exception
    }
    
    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionNotContainNull()
    {
        // Arrange
        var source = new int?[] { 1, 2, 3, 4, 5, null };

        void Act()
        {
            Guard.Argument(source).DoesNotContainNull();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collections must not contain null. (Parameter 'source')", ex.Message);
    }
}