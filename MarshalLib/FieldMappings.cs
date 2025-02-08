namespace MarshalLib;

public class FieldMappings
{
    private Dictionary<ushort, FieldDescriptor> _fields = new();
    
    public void Read(Stream stream)
    {
        using var reader = new BinaryReader(stream);

        ushort index = 0;
        
        while (true)
        {
            if (reader.BaseStream.Position == reader.BaseStream.Length)
                break;
            
            var field = new FieldDescriptor
            {
                Header = reader.ReadUInt16BigEndian(),
                Type = (FieldType)reader.ReadUInt16BigEndian(),
                Name = reader.ReadCString()
            };
            
            _fields.Add(index, field);
            index++;
        }
    }
    
    public FieldDescriptor? Get(ushort index) =>
        _fields.GetValueOrDefault(index);
}