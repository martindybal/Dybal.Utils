using Xunit;

using static Dybal.Utils.Guards.GuardProvider;

namespace Tests.Dybal.Utils.Guards;

public class GuardProviderTests : UnitTestsBase
{
    [Fact]
    public void Return_ArgumentGuard_with_correct_argument()
    {
        // Arrange
        DateTime? value = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard(value);

        // Assert
        AssertGuard.AssertArgument(value, guard.Argument);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_2()
    {
        // Arrange
        string? value1 = null;
        DateTime? value2 = new DateTime(2009, 09, 01);

        // Act
        var guard = Guard(value1, value2);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_3()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 5;

        // Act
        var guard = Guard(value1, value2, value3);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_4()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;

        // Act
        var guard = Guard(value1, value2, value3, value4);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_5()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;

        // Act
        var guard = Guard(value1, value2, value3, value4, value5);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_6()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;

        // Act
        var guard = Guard(value1, value2, value3, value4, value5, value6);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_7()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;

        // Act
        var guard = Guard(value1, value2, value3, value4, value5, value6, value7);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_8()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;
        int value8 = 8;

        // Act
        var guard = Guard(value1, value2, value3, value4, value5, value6, value7, value8);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
        AssertGuard.AssertArgument(value8, guard.Arguments[7]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_9()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;
        int value8 = 8;
        int value9 = 9;

        // Act
        var guard = Guard(value1, value2, value3, value4, value5, value6, value7, value8, value9);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
        AssertGuard.AssertArgument(value8, guard.Arguments[7]);
        AssertGuard.AssertArgument(value9, guard.Arguments[8]);
    }

    [Fact]
    public void Return_MultipleArgumentGuard_with_correct_argument_10()
    {
        // Arrange
        DateTime? value1 = new DateTime(2009, 09, 01);
        string? value2 = null;
        int value3 = 3;
        int value4 = 4;
        int value5 = 5;
        int value6 = 6;
        int value7 = 7;
        int value8 = 8;
        int value9 = 9;
        int value10 = 10;

        // Act
        var guard = Guard(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10);

        // Assert
        AssertGuard.AssertArgument(value1, guard.Arguments[0]);
        AssertGuard.AssertArgument(value2, guard.Arguments[1]);
        AssertGuard.AssertArgument(value3, guard.Arguments[2]);
        AssertGuard.AssertArgument(value4, guard.Arguments[3]);
        AssertGuard.AssertArgument(value5, guard.Arguments[4]);
        AssertGuard.AssertArgument(value6, guard.Arguments[5]);
        AssertGuard.AssertArgument(value7, guard.Arguments[6]);
        AssertGuard.AssertArgument(value8, guard.Arguments[7]);
        AssertGuard.AssertArgument(value9, guard.Arguments[8]);
        AssertGuard.AssertArgument(value10, guard.Arguments[9]);
    }
}