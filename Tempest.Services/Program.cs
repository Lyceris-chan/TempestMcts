using Tempest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<HostService>();

var app = builder.Build();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

app.MapGrpcService<LobbyHostServiceImpl>();
app.MapGrpcService<ServerListServiceImpl>();

app.Run();