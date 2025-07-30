using Grpc.Net.Client;
using Tempest.Protocol;

namespace Tempest.CLI;

internal class ServerCommands
{
    public async Task Open(string? privateId = null, string servicesUrl = "https://localhost:7165")
    {
        using var channel = GrpcChannel.ForAddress(servicesUrl);
        var client = new LobbyHostService.LobbyHostServiceClient(channel);

        Console.WriteLine($"Services URL: {servicesUrl}");

        var createServerResponse = await client.CreateLobbyAsync(new CreateLobbyRequest()
        {
            PrivateId = privateId ?? Guid.NewGuid().ToString(),
            Server = new Server()
            {
                Bots = 0,
                Game = "Paladins",
                Joinable = true,
                JoinInProgress = false,
                Map = "",
                MaxPlayers = 10,
                Name = "Dummy Test Server",
                Players = 5,
                Tags = "casual,4fun",
                Version = "0.57"
            }
        });

        var ticket = createServerResponse.Ticket;
        var id = createServerResponse.Id;

        Console.WriteLine($"Server created, id: {id}");

        var updates = client.ReceiveLobbyUpdates(new ReceiveLobbyUpdatesRequest());
        
        // use updates
    }
}