using System.Net;
using MarshalLib;

var fieldMappings = new FieldMappings();
var functionMappings = new FunctionMappings();

fieldMappings.Read(File.OpenRead("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\fields.dat"));
functionMappings.Read(File.OpenRead("C:\\Users\\Kyiro\\Downloads\\rrserver-core-dev\\rrserver-core\\data\\functions.dat"));
    
var marshalSerializerOptions = new MarshalSerializerOptions
{
    FieldMappings = fieldMappings,
    FunctionMappings = functionMappings
};

var listener = new MctsListener(IPAddress.Any, 9000, marshalSerializerOptions);

await listener.Run();