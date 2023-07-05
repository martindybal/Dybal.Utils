namespace Dybal.Utils.TypedValues;

[Flags]
public enum Converters
{
    None = 0,
    
    /// <summary>
    /// Creates a System.Text.Json.Serialization.JsonConverter for serializing the value object as its primitive value
    /// </summary>
    SystemTextJson = 1 << 0
}