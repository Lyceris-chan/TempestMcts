namespace MarshalLib;

public class FieldDescriptor
{
    public ushort Header { get; set; }
    public FieldType Type { get; set; }
    public required string Name { get; set; }
}