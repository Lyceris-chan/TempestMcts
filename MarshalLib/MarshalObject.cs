namespace MarshalLib;

public class MarshalObject(FieldType type, object value, MarshalFlags flags = MarshalFlags.None)
{
    public FieldType Type { get; set; } = type;
    public MarshalFlags Flags { get; set; } = flags;
    public object Value { get; set; } = value;
}