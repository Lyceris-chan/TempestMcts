using System.Security.Cryptography;
using System.Text;
using Grpc.Core;
using Tempest.Protocol;

namespace Tempest.Services;

public class LobbyHostServiceImpl(HostService hostService) : LobbyHostService.LobbyHostServiceBase
{
    public override Task<CreateLobbyResponse> CreateLobby(CreateLobbyRequest request, ServerCallContext context)
    {
        var publicId = BitConverter.ToUInt64(SHA256.HashData(Encoding.UTF8.GetBytes(request.PrivateId)));
        var ticket = Guid.NewGuid().ToString();

        request.Server.Id = publicId;
        
        hostService.List.Add(new LobbyHost()
        {
            Ticket = ticket,
            Lobby = new Lobby()
            {
                Server = request.Server
            }
        });
        
        return Task.FromResult(new CreateLobbyResponse()
        {
            Ticket = ticket,
            Id = publicId
        });
    }
}