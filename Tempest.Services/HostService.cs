using System.Collections.Concurrent;
using Tempest.Protocol;

namespace Tempest.Services;

public class HostService
{
    public ConcurrentBag<LobbyHost> List = new();
}