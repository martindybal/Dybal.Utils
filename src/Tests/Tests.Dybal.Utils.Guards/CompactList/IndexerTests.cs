using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class IndexerTests : UnitTestsBase
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public void ShouldReturn_ItemOnIndex_WhenIndexInRange(int index)
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

        // Act
        var actualItem = compactList[index];

        // Assert
        Assert.Equal(index + 1, actualItem);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
    public void ShouldThrow_ItemOnIndex_WhenIndexOutOfRange(int indexOutOfRange)
    {
        // Arrange
        var compactList = new CompactList<int>(1, 1, 1);

        // Act
        void Act()
        {
            var actualItem = compactList[indexOutOfRange];
        }

        // Assert
        var argumentOutOfRangeException = Assert.Throws<ArgumentOutOfRangeException>(Act);
        Assert.Equal("Provided index was outside of the valid range. (Parameter 'index')", argumentOutOfRangeException.Message);
    }
}