using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class StringGuardCombinationTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_AllGuardsIsOk()
    {
        // Arrange
        string value = "abc";

        // Act
        string guardValue = Guard.Argument(value).NotNullOrWhiteSpace().MinLength(2).MaxLength(5);

        // Assert
        Assert.Equal(value, guardValue);
    }
}