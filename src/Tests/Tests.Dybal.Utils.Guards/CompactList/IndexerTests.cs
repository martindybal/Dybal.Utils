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
    [InlineData(10)]
    [InlineData(11)]
    public void Return_item_on_index_When_index_in_range(int index)
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

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
    public void Throw_ArgumentOutOfRangeException_When_index_out_of_range(int indexOutOfRange)
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