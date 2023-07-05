using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class CountTests : UnitTestsBase
{
    [Fact]
    public void Return_2_When_contains_2_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2);
        
        // Assert
        Assert.Equal(2, compactList.Count);
    }

    [Fact]
    public void Return_3_When_contains_3_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3);
        
        // Assert
        Assert.Equal(3, compactList.Count);
    }

    [Fact]
    public void Return_4_When_contains_4_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4);
        
        // Assert
        Assert.Equal(4, compactList.Count);
    }

    [Fact]
    public void Return_5_When_contains_5_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5);
        
        // Assert
        Assert.Equal(5, compactList.Count);
    }

    [Fact]
    public void Return_6_When_contains_6_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6);
        
        // Assert
        Assert.Equal(6, compactList.Count);
    }
    
    [Fact]
    public void Return_7_When_contains_7_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7);
        
        // Assert
        Assert.Equal(7, compactList.Count);
    }
    
    [Fact]
    public void Return_8_When_contains_8_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8);
        
        // Assert
        Assert.Equal(8, compactList.Count);
    }
    
    [Fact]
    public void Return_9_When_contains_9_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9);
        
        // Assert
        Assert.Equal(9, compactList.Count);
    }
    
    [Fact]
    public void Return_10_When_contains_10_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        
        // Assert
        Assert.Equal(10, compactList.Count);
    }
    
    [Fact]
    public void Return_11_When_contains_11_elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
        
        // Assert
        Assert.Equal(11, compactList.Count);
    }
}