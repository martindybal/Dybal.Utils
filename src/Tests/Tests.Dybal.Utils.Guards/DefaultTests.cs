using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class DefaultTests : TestBase
{
    [Fact]
    public void Should_NotThrows_When_VariableWithoutValue()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        Guard.Default(value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyWithoutValue()
    {
        // Arrange
        var sample = new { Value = Guid.Empty };

        // Act
        Guard.Default(sample.Value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuidWithValue()
    {
        // Arrange
        var value = Guid.NewGuid();

        void Act()
        {
            Guard.Default(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be an empty GUID. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableWithValue()
    {
        // Arrange
        int value = 155;

        void Act()
        {
            Guard.Default(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be a default value. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuidPropertyWithValue()
    {
        // Arrange
        var sample = new { Value = Guid.NewGuid() };

        void Act()
        {
            Guard.Default(sample.Value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be an empty GUID. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = 155 };
            
        void Act()
        {
            Guard.Default(sample.Value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value must be a default value. (Parameter 'sample.Value')", ex.Message);
    }
}