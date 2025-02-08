using System.Text.Json.Serialization;

namespace MarshalLib;

[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MarshalFlags
{
    None = 0,
    Utf32 = 1 << 0,
}