using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class EmptyIfTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_CollectionNullConditionFalse()
    {
        // Arrange
        int[] source = null!;

        // Act
        Guard.EmptyIf(source, false);

        // Assert
        // doesn't throw any exception
    }


    [Fact]
    public void Should_NotThrows_When_CollectionEmptyConditionFalse()
    {
        // Arrange
        var source = new int[] { };

        // Act
        Guard.EmptyIf(source, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_CollectionNotEmptyConditionFalse()
    {
        // Arrange
        var source = new[] { 5 };

        // Act
        Guard.EmptyIf(source, false);

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
            Guard.EmptyIf(source, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(Act);
        Assert.Equal("Value cannot be null. (Parameter 'source')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_CollectionEmptyConditionTrue()
    {
        // Arrange
        var source = new int[] { };

        // Act
        Guard.EmptyIf(source, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_CollectionNotConditionTrue()
    {
        // Arrange
        var source = new[] { 5 };

        void Act()
        {
            Guard.EmptyIf(source, true);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Collection has to be empty. (Parameter 'source')", ex.Message);
    }
}