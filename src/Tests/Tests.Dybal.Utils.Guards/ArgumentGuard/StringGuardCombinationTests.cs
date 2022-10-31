using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class StringGuardCombinationTests : UnitTestsBase
{
    [Fact]
    public void NotThrow_When_all_guard_are_ok()
    {
        // Arrange
        string value = "abc";

        // Act
        string guardValue = Guard.Argument(value)
                                 .NotNullOrWhiteSpace()
                                 .MinLength(2)
                                 .MaxLength(5)
                                 .StartsWith("abc")
                                 .Contains("abc")
                                 .EndsWith("abc");

        // Assert
        Assert.Equal(value, guardValue);
    }
}