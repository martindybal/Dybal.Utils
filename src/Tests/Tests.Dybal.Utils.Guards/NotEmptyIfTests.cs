using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotEmptyIfTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionNotEmptyConditionFalse()
    {
        // Arrange
        var source = new[] { 5 };

        // Act
        Guard.NotEmptyIf(source, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionNullConditionFalse()
    {
        // Arrange
        int[] source = null!;

        // Act
        Guard.NotEmptyIf(source, false);

        // Assert
        // doesn't throw any exception
    }


    [Fact]
    public void Should_NotThrows_When_CollectionEmptyConditionFalse()
    {
        // Arrange
        var source = new int[] { };

        // Act
        Guard.NotEmptyIf(source, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionNotEmptyConditionTrue()
    {
        // Arrange
        var source = new[] { 5 };

        // Act
        Guard.NotEmptyIf(source, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentNullException_When_CollectionNullConditionTrue()
    {
        // Arrange
        int[]? source = null;
            
        void Act()
        {
            Guard.NotEmptyIf(source, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionEmptyConditionTrue()
    {
        // Arrange
        var source = new int[] { };
            
        void Act()
        {
            Guard.NotEmptyIf(source, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection cannot be empty. (Parameter 'source')", ex.Message);
    }
}