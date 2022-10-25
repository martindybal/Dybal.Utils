using System.Collections;
using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class GetEnumerator : UnitTestsBase
{
    [Fact]
    public void Generic_ShouldReturn_SequenceWithAllElements()
    {
        var expectedSequence = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        var compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

        // Act
        var actualSequence = new List<int>();
        using var enumerator = compactList.GetEnumerator();
        while (enumerator.MoveNext())
        {
            actualSequence.Add(enumerator.Current);
        }
        
        Assert.Equal(actualSequence, expectedSequence);
    }

    [Fact]
    public void NonGeneric_ShouldReturn_SequenceWithAllElements()
    {
        var expectedSequence = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        IEnumerable compactList = new CompactList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

        // Act
        var actualSequence = new List<int>();
        var enumerator = compactList.GetEnumerator();
        while (enumerator.MoveNext())
        {
            actualSequence.Add((int)enumerator.Current);
        }
        
        Assert.Equal(actualSequence, expectedSequence);
    }
}