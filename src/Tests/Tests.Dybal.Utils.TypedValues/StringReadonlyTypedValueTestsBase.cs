﻿using Dybal.Utils.TypedValues;
using Xunit;

namespace Tests.Dybal.Utils.TypedValues;

public abstract class StringReadonlyTypedValueTestsBase<TTypedValue>
    where TTypedValue : IReadonlyTypedValue<TTypedValue, string>
{
    [Fact]
    public void Value_Should_be_equal()
    {
        // Arrange
        var value = "typed value";

        // Act
        var typedValue = CreateTypedValue(value);
        
        // Assert
        Assert.Equal(value, typedValue.Value);
    }

    [Fact]
    public void Equals_Return_true_when_value_is_same()
    {
        // Arrange
        var value = "typed value";
        var typedValue1 = CreateTypedValue(value);
        var typedValue2 = CreateTypedValue(value);

        // Act
        var areEqual = typedValue1.Equals(typedValue2);

        // Assert
        Assert.True(areEqual);
    }

    [Fact]
    public void Equals_Return_false_when_value_is_same()
    {
        // Arrange
        var typedValue1 = CreateTypedValue("typed value A");
        var typedValue2 = CreateTypedValue("typed value B");

        // Act
        var areEqual = typedValue1.Equals(typedValue2);

        // Assert
        Assert.False(areEqual);
    }

    [Fact]
    public void Constructor_Throw_ArgumentException_when_ValidateValue_fails()
    {
        // Act
        void Act()
        {
            CreateTypedValue(" ");
        }

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    [Theory]
    [InlineData("a", "a")]
    [InlineData("a", "b")]
    [InlineData("b", "a")]
    public void Compare_Return_same(string value1, string value2)
    {
        // Arrange
        var typedValue1 = CreateTypedValue(value1);
        var typedValue2 = CreateTypedValue(value2);

        // Act
        var compareResult = typedValue1.CompareTo(typedValue2);

        // Assert
        var expectedResult = value1.CompareTo(value2);
        Assert.Equal(expectedResult, compareResult);
    }
    
    [Fact]
    public void System_Text_Json_Serialize_Should_be_equal_value_Serialize()
    {
        // Arrange
        var value = "typed value";
        var expectedJson = System.Text.Json.JsonSerializer.Serialize(value);

        // Act
        var typedValue = CreateTypedValue(value);
        var typedValueJson = System.Text.Json.JsonSerializer.Serialize(typedValue);
        
        // Assert
        Assert.Equal(expectedJson, typedValueJson);
    }
    
    [Fact]
    public void System_Text_Json_Deserialize_typed_value_from_value_json()
    {
        // Arrange
        var value = "typed value";
        var expectedJson = System.Text.Json.JsonSerializer.Serialize(value);

        // Act
        var typedValue = System.Text.Json.JsonSerializer.Deserialize<TTypedValue>(expectedJson);
        
        // Assert
        Assert.NotNull(typedValue);
        Assert.Equal(value, typedValue.Value);
    }

    protected abstract TTypedValue CreateTypedValue(string value);
}