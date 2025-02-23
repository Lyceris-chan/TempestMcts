namespace MarshalLib;

public class MarshalFunction
{
    public uint FunctionHash { get; set; }
    public string? FunctionName { get; set; }
    public Dictionary<string, MarshalObject> Rows { get; set; } = new();
}