namespace Dybal.Utils.TypedValues.SourceGenerators;

public class TypedValueGeneratorOptions
{
    public Converters Converters { get; }

    public TypedValueGeneratorOptions(Converters converters)
    {
        Converters = converters;
    }

    public static TypedValueGeneratorOptions Default { get; } = new(Converters.None);
}