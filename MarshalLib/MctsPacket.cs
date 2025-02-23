namespace MarshalLib;

public enum PacketDirection
{
    Incoming,
    Outgoing
}

public record MctsPacket(PacketDirection Direction, MarshalFunction Function);