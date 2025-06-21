using System.Text.Json.Serialization;

namespace MarshalLib;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MarshalSerializerVersion
{
    Modern,
    Legacy
}