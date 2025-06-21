namespace MarshalLib;

public class MarshalSerializerOptions
{
    public required FieldMappings FieldMappings { get; set; }
    public required FunctionMappings FunctionMappings { get; set; }
    public MarshalSerializerVersion Version { get; set; } = MarshalSerializerVersion.Modern;
}