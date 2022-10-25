using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class CountTests : UnitTestsBase
{
    [Fact]
    public void ShouldReturn_2_When_Contains2Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2);
        
        // Assert
        Assert.Equal(2, compactList.Count);
    }

    [Fact]
    public void ShouldReturn_3_When_Contains3Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3);
        
        // Assert
        Assert.Equal(3, compactList.Count);
    }

    [Fact]
    public void ShouldReturn_4_When_Contains4Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4);
        
        // Assert
        Assert.Equal(4, compactList.Count);
    }

    [Fact]
    public void ShouldReturn_5_When_Contains5Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5);
        
        // Assert
        Assert.Equal(5, compactList.Count);
    }

    [Fact]
    public void ShouldReturn_6_When_Contains6Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6);
        
        // Assert
        Assert.Equal(6, compactList.Count);
    }
    
    [Fact]
    public void ShouldReturn_7_When_Contains7Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7);
        
        // Assert
        Assert.Equal(7, compactList.Count);
    }
    
    [Fact]
    public void ShouldReturn_8_When_Contains8Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8);
        
        // Assert
        Assert.Equal(8, compactList.Count);
    }
    
    [Fact]
    public void ShouldReturn_9_When_Contains9Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9);
        
        // Assert
        Assert.Equal(9, compactList.Count);
    }
    
    [Fact]
    public void ShouldReturn_10_When_Contains10Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        
        // Assert
        Assert.Equal(10, compactList.Count);
    }
    
    [Fact]
    public void ShouldReturn_11_When_Contains11Elements()
    {
        // Arrange
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
        
        // Assert
        Assert.Equal(11, compactList.Count);
    }
}