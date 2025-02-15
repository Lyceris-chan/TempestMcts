namespace MarshalLib;

public class FieldMappings
{
    private Dictionary<ushort, FieldDescriptor> _fields = new();
    private Dictionary<string, ushort> _fieldNames = new();
    
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
            _fieldNames.Add(field.Name, index);
            
            index++;
        }
    }
    
    public FieldDescriptor? Get(ushort index) =>
        _fields.GetValueOrDefault(index);
    
    public bool TryGetIndex(string name, out ushort index) =>
        _fieldNames.TryGetValue(name, out index);
    
    public ushort GetIndex(string name) =>
        _fieldNames.GetValueOrDefault(name);
}