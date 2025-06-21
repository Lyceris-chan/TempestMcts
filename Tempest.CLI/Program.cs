using ConsoleAppFramework;
using Tempest.CLI;

var app = ConsoleApp.Create();

app.Add<MarshalCommands>("marshal");
app.Add<ProjectCommands>("project");
app.Add<UtilityCommands>();

app.Run(args);