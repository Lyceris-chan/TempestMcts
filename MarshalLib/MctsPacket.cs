namespace MarshalLib;

public class MctsPacket
{
    public uint FunctionHash { get; set; }
    public string? FunctionName { get; set; }
    public Dictionary<string, MarshalObject> Rows { get; set; } = new();
}