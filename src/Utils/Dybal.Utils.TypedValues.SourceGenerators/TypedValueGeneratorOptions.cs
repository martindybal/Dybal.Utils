namespace Dybal.Utils.TypedValues.SourceGenerators;

public class TypedValueGeneratorOptions
{
    public Converters Converters { get; }

    public TypedValueGeneratorOptions(Converters converters)
    {
        Converters = converters;
    }
}