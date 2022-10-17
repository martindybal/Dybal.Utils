namespace Dybal.Utils.Guards;

public record struct Argument<TArgument>(TArgument Value, string Name);