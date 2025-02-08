using System.Text.Json;
using MarshalLib;

var input =
    "00BE056A4F573F40";

var bytes = Convert.FromHexString(input);

var fieldMappings = new FieldMappings();
var functionMappings = new FunctionMappings();

fieldMappings.Read(File.OpenRead("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\fields.dat"));
functionMappings.Read(File.OpenRead("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\functions.dat"));

var marshalSerializerOptions = new MarshalSerializerOptions
{
    FieldMappings = fieldMappings,
    FunctionMappings = functionMappings
};

var data = MarshalSerializer.DeserializePacket(new MemoryStream(bytes), marshalSerializerOptions);

Console.WriteLine($"Function: {data.FunctionName}, Field Count: {data.Rows.Count}");

var jsonOutput = JsonSerializer.Serialize(data, new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});

Console.WriteLine(jsonOutput);