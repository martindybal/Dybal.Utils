﻿using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class NotDefaultTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_VariableWithValue()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        Guard.Argument(value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_NullableVariableWithValue()
    {
        // Arrange
        Guid? value = Guid.NewGuid();

        // Act
        Guard.Argument(value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_PropertyWithValue()
    {
        // Arrange
        var sample = new { Value = Guid.NewGuid() };

        // Act
        Guard.Argument(sample.Value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuidVariableHasDefaultValue()
    {
        // Arrange
        var value = Guid.Empty;

        void Act()
        {
            Guard.Argument(value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be an empty GUID. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_NullableGuidVariableNull()
    {
        // Arrange
        Guid? value = null;

        void Act()
        {
            Guard.Argument(value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Nullable object must have a value. (Parameter 'value')", ex.Message);
    }
    
    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableHasDefaultValue()
    {
        // Arrange
        var value = 0;

        void Act()
        {
            Guard.Argument(value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_GuidPropertyHasDefaultValue()
    {
        // Arrange
        var sample = new { Value = Guid.Empty };

        void Act()
        {
            Guard.Argument(sample.Value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be an empty GUID. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyHasDefaultValue()
    {
        // Arrange
        var sample = new { Value = 0 };

        void Act()
        {
            Guard.Argument(sample.Value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'sample.Value')", ex.Message);
    }

    [Fact]
    public void Should_NotThrows_When_ReferenceVariableWithValue()
    {
        // Arrange
        var value = new object();

        // Act
        Guard.Argument(value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void Should_NotThrows_When_ReferencePropertyWithValue()
    {
        // Arrange
        var sample = new { Value = new object() };

        // Act
        Guard.Argument(sample.Value).NotDefault();

        // Assert
        // doesn't throw any exception
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_VariableHasNullVariable()
    {
        // Arrange
        var value = (object)null!;

        void Act()
        {
            Guard.Argument(value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'value')", ex.Message);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_PropertyHasNullValue()
    {
        // Arrange
        var sample = new { Value = (object)null! };

        void Act()
        {
            Guard.Argument(sample.Value).NotDefault();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Value cannot be the default value. (Parameter 'sample.Value')", ex.Message);
    }
}