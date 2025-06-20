using ConsoleAppFramework;
using Tempest.CLI;

var app = ConsoleApp.Create();

app.Add<UtilityCommands>();

app.Run(args);