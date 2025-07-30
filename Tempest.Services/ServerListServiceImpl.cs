using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Tempest.Services;

using Protocol;

public class ServerListServiceImpl(HostService hostService) : ServerListService.ServerListServiceBase
{
    public override async Task GetServers(GetServersRequest request, IServerStreamWriter<Server> responseStream, ServerCallContext context)
    {
        foreach (var host in hostService.List)
        {
            await responseStream.WriteAsync(host.Lobby.Server);
        }
    }
}