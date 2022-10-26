using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards.ArgumentGuard;

public class HasValueTests : UnitTestsBase
{
    [Fact]
    public void Should_NotThrows_When_ObjectHasValue()
    {
        // Arrange
        var expectedValue = new DateTime(2009, 09, 01);
        DateTime? nullableDateTime = expectedValue;

        // Act
        var actualValue = Guard.Argument(nullableDateTime).HasValue();

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void ShouldThrows_ArgumentException_When_Null()
    {
        // Arrange
        DateTime? nullableDateTime = null;

        void Act()
        {
            Guard.Argument(nullableDateTime).HasValue();
        }

        // Assert
        var ex = Assert.Throws<ArgumentException>(Act);
        Assert.Equal("Nullable object must have a value. (Parameter 'value')", ex.Message);
    }


    [Fact]
    public void Throws_Should_ThrowCustomException()
    {
        // Arrange
        DateTime? nullableDateTime = null;

        void Act()
        {
            ThrowHelper.Register((paramName, message) => new CustomException(paramName, message));
            Guard.Argument(nullableDateTime).Throws<CustomException>().HasValue();
        }

        // Assert
        var customException = Assert.Throws<CustomException>(Act);
        Assert.Equal(nameof(nullableDateTime), customException.ParamName);
        Assert.Equal("Nullable object must have a value.", customException.Message);
    }

    class CustomException : Exception
    {
        public string ParamName { get; }

        public CustomException(string paramName, string? message)
            : base(message)
        {
            ParamName = paramName;
        }
    }
}