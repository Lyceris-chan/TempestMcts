using Tempest.Protocol;

namespace Tempest.Services;

public class LobbyHost
{
    public required string Ticket { get; set; }
    public required Lobby Lobby { get; set; }
}