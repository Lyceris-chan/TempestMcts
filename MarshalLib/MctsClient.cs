using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace MarshalLib;

public class MctsClient
{
    public Guid Id { get; } = Guid.NewGuid();
    private ConcurrentQueue<MctsPacket> _queue = new();

    public void Enqueue(MctsPacket packet) => _queue.Enqueue(packet);
    public bool TryDequeue([MaybeNullWhen(false)] out MctsPacket result) => _queue.TryDequeue(out result);
    public bool TryPeek([MaybeNullWhen(false)] out MctsPacket? result) => _queue.TryPeek(out result);
}