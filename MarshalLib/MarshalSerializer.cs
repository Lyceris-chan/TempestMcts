using System.Text;

namespace MarshalLib;

public static class MarshalSerializer
{
    public static MctsPacket DeserializePacket(Stream stream, MarshalSerializerOptions options)
    {
        var reader = new BinaryReader(stream);
        var packet = new MctsPacket();

        var zeroByte = reader.ReadByte();
        if (zeroByte != 0x0)
            throw new Exception($"Zero byte is not 0x00: {zeroByte}");
        
        var functionHash = reader.ReadUInt32();
        var function = options.FunctionMappings.Get(functionHash);

        packet.FunctionHash = functionHash;
        
        if (function != null)
        {
            packet.FunctionName = function.Name;
        }
        
        var fieldCount = reader.ReadUInt16();
        var i = 0;

        while (i < fieldCount)
        {
            var field = Deserialize(stream, options);
            
            foreach (var entry in field)
            {
                packet.Rows[entry.Key] = entry.Value;
            }
            
            i += field.Count;
        }
        
        return packet;
    }
    
    public static Dictionary<string, MarshalObject> Deserialize(Stream stream, MarshalSerializerOptions options)
    {
        var result = new Dictionary<string, MarshalObject>();
        var reader = new BinaryReader(stream);

        var entryHeader = reader.ReadUInt16();

        var headerType = entryHeader >> 12;
        var headerParam = entryHeader & 0x3F;

        switch (headerType)
        {
            case 1 or 2 or 3 or 4: // Integers
            {
                var indexes = new ushort[headerParam];
                
                for (var i = 0; i < headerParam; i++)
                {
                    indexes[i] = reader.ReadUInt16();
                }

                foreach (var i in indexes)
                {
                    var field = options.FieldMappings.Get(i);
                    
                    if (field == null) throw new Exception($"Field (index: {i}) not found");
                    
                    var value = headerType switch
                    {
                        1 => new MarshalObject(FieldType.Byte, reader.ReadByte()),
                        2 => new MarshalObject(FieldType.Short, reader.ReadUInt16()),
                        3 => new MarshalObject(FieldType.Int, reader.ReadUInt32()),
                        4 => new MarshalObject(FieldType.Long, reader.ReadUInt64()),
                        _ => throw new Exception("Invalid header type")
                    };
                    
                    result[field.Name] = value;
                }
                
                break;
            }
            case 6: // DataSet
            {
                var fieldIndex = reader.ReadUInt16();
                var field = options.FieldMappings.Get(fieldIndex);
                
                if (field == null) throw new Exception($"Field (index: {fieldIndex}) not found");

                var rowCount16 = reader.ReadUInt16();
                var rowCount = rowCount16 == ushort.MaxValue ? reader.ReadUInt32() : rowCount16;
                
                var rows = new Dictionary<string, MarshalObject>();
                var o = 0;
                
                while (o < rowCount)
                {
                    var entryCount = reader.ReadUInt16();
                    
                    for (var i = 0; i < entryCount;)
                    {
                        var rowEntry = Deserialize(stream, options);
                        
                        i += rowEntry.Count;
                        foreach (var entry in rowEntry)
                        {
                            rows[entry.Key] = entry.Value;
                        }
                    }

                    o++;
                }
                
                result[field.Name] = new MarshalObject(FieldType.DataSet, rows);
                
                break;
            }
            case 7: // GUID
            {
                var fieldIndex = reader.ReadUInt16();
                var field = options.FieldMappings.Get(fieldIndex);
                
                if (field == null) throw new Exception($"Field (index: {fieldIndex}) not found");

                if (headerParam != 1)
                {
                    var length = reader.ReadUInt16();
                    
                    if (length == 0) throw new Exception("GUID Length is zero");
                }
                
                var guid = new Guid(reader.ReadBytes(16));
                
                result[field.Name] = new MarshalObject(FieldType.Guid, guid);

                break;
            }
            case 8: // Blob
            {
                var fieldIndex = reader.ReadUInt16();
                var field = options.FieldMappings.Get(fieldIndex);
                
                if (field == null) throw new Exception($"Field (index: {fieldIndex}) not found");

                var length = reader.ReadUInt16();
                
                result[field.Name] = new MarshalObject(FieldType.Blob, reader.ReadBytes(length));
                
                break;
            }
            case 10: // String
            {
                var fieldIndex = reader.ReadUInt16();
                var length = reader.ReadUInt16();
                
                var isUtf32 = headerParam == 3;
                var field = options.FieldMappings.Get(fieldIndex);
                
                if (field == null) throw new Exception($"Field (index: {fieldIndex}) not found");
                
                var slice = reader.ReadBytes(isUtf32 ? length * 4 : length * 2);
                var flags = isUtf32 ? MarshalFlags.Utf32 : MarshalFlags.None;
                var value = isUtf32 ? Encoding.UTF32.GetString(slice) : Encoding.Unicode.GetString(slice);

                result[field.Name] = new MarshalObject(FieldType.String, value, flags);

                break;
            }
            default:
                throw new Exception($"Invalid header type: {headerType}");
        }

        return result;
    }
}