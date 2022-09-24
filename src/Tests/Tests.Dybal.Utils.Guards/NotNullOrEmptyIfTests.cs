using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotNullOrEmptyIfTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_VariableWithValueConditionFalse()
    {
        // Arrange
        var value = "non-empty";

        // Act
        Guard.NotNullOrEmptyIf(value, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_VariableWithoutValueConditionFalse()
    {
        // Arrange
        var value = string.Empty;

        // Act
        Guard.NotNullOrEmptyIf(value, false);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_VariableWithValue()
    {
        // Arrange
        var value = "non-empty";

        // Act
        Guard.NotNullOrEmptyIf(value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = "non-empty" };

        // Act
        Guard.NotNullOrEmptyIf(sample.Value, true);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableWithoutValue()
    {
        // Arrange
        var value = "";

        void Act() => Guard.NotNullOrEmptyIf(value, true);
            
        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or an empty string. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyWithoutValue()
    {
        // Arrange
        var sample = new { Value = "" };

        void Act() => Guard.NotNullOrEmptyIf(sample.Value, true);
            
        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be null or an empty string. (Parameter 'sample.Value')", ex.Message);
    }
}