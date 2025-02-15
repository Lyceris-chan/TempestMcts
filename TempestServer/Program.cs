using System.Text.Json;
using MarshalLib;

var input = File.ReadAllBytes(
    "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Realm Royale\\RealmGame\\CookedPCConsole\\assembly.dat");
var bytes = input.Select(b => (byte)(b ^ 0x2A)).ToArray();

File.WriteAllBytes("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Realm Royale\\RealmGame\\CookedPCConsole\\assembly.unxored.dat", bytes);

var fieldMappings = new FieldMappings();
var functionMappings = new FunctionMappings();

fieldMappings.Read(File.OpenRead("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\fields.dat"));
functionMappings.Read(File.OpenRead("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\functions.dat"));
    
var marshalSerializerOptions = new MarshalSerializerOptions
{
    FieldMappings = fieldMappings,
    FunctionMappings = functionMappings
};

var original = MarshalSerializer.DeserializePacket(new MemoryStream(bytes), marshalSerializerOptions);

var reserialised = new MemoryStream();
MarshalSerializer.SerializePacket(reserialised, original, marshalSerializerOptions);

reserialised.Position = 0;
var deserialised = MarshalSerializer.DeserializePacket(reserialised, marshalSerializerOptions);

Console.WriteLine("Original: " + bytes.Length);
Console.WriteLine("Reserialised: " + reserialised.Length);

File.WriteAllText("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\assembly.json", JsonSerializer.Serialize(deserialised, new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
}));