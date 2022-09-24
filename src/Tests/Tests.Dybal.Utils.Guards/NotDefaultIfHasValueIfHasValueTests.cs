using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class NotDefaultIfHasValueIfHasValueTests : TestBase
{
    [Fact]
    public void ShouldNotThrows_When_Null()
    {
        // Arrange
        var value = (Guid?)null;

        // Act
        Guard.NotDefaultIfHasValue(value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldNotThrows_When_VariableWithValue()
    {
        // Arrange
        Guid? value = Guid.NewGuid();

        // Act
        Guard.NotDefaultIfHasValue(value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldNotThrows_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = (Guid?)Guid.NewGuid() };

        // Act
        Guard.NotDefaultIfHasValue(sample.Value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldNotThrows_When_PropertyNull()
    {
        // Arrange
        var sample = new { Value = (Guid?)null };

        // Act
        Guard.NotDefaultIfHasValue(sample.Value);

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuidVariableHasDefaultValue()
    {
        // Arrange
        Guid? value = Guid.Empty;

        void Act()
        {
            Guard.NotDefaultIfHasValue(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be an empty GUID. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuidPropertyHasDefaultValue()
    {
        // Arrange
        var sample = new { Value = (Guid?)Guid.Empty };

        void Act()
        {
            Guard.NotDefaultIfHasValue(sample.Value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be an empty GUID. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableHasDefaultValue()
    {
        // Arrange
        int? value = 0;

        void Act()
        {
            Guard.NotDefaultIfHasValue(value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyHasDefaultValue()
    {
        // Arrange
        var sample = new { Value = (int?)0 };

        void Act()
        {
            Guard.NotDefaultIfHasValue(sample.Value);
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'sample.Value')", ex.Message);
    }
}