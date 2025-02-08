namespace MarshalLib;

public class FieldMappings
{
    private List<FieldDescriptor> _fields = new();
    
    public void Read(Stream stream)
    {
        using var reader = new BinaryReader(stream);

        UInt16 index = 0;
        
        while (true)
        {
            if (reader.BaseStream.Position == reader.BaseStream.Length)
                break;
            
            var field = new FieldDescriptor
            {
                Header = reader.ReadUInt16BigEndian(),
                Type = (FieldType)reader.ReadUInt16BigEndian(),
                Name = reader.ReadCString(),
                Index = index
            };
            
            _fields.Add(field);
            index++;
        }
        
        _fields.AddRange([
            new()
            {
                Type = FieldType.Custom,
                Name = "KEY",
                Index = 3730
            },
            new()
            {
                Type = FieldType.Custom,
                Name = "IV",
                Index = 3731
            }
        ]);
    }
    
    public FieldDescriptor? Get(UInt16 index) =>
        _fields.FirstOrDefault(f => f.Index == index);
}