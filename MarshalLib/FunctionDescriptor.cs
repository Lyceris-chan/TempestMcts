namespace MarshalLib;

public class FunctionDescriptor
{
    public uint Header { get; set; }
    public uint Hash { get; set; }
    public required string Name { get; set; }
}