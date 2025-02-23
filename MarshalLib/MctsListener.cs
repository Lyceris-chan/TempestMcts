using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace MarshalLib;

public class MctsListener(IPAddress localaddr, int port, MarshalSerializerOptions serializerOptions)
{
    private readonly TcpListener _listener = new(localaddr, port);
    private ConcurrentDictionary<long, MctsClient> _clients = new();

    public Task Run()
    {
        _listener.Start();

        return Task.Run(async () =>
        {
            try
            {
                while (true)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    var mctsClient = new MctsClient();

                    var hello = MarshalSerializer.DeserializeFunction(new MemoryStream(Convert.FromHexString(
                        "0007414CB4060001800500720148002959D74B6EE761CF6046FC4FF6E3272F6333F656D3869846F04ECAB5746D791D704BA7FB8D921178AF45884D55A2DA8059371E2AED18A977A6ED0025CC26EE9626F30C618D6B1520AEC1706087A412DE4DCE5D64863EAAD5D5DF2F5BC933A2635365FB585F2C3E1CC9747BB2EFA141621E6F7118D8DC31B2FD83DB17F63FF4B185C9316574B6A04D3A4212AEE79A4CE3188F23C82533F261D0346E29DA634F4C8C3EFBAD42CFD00AC640C579CF95578C60F992CC1C749D0392F8452FC9098F7CC01A220FDC104513F56895E42E6356372962D549BAECA832FCABEAB897180059E2DB66A7F3EDC194E4DD7D148FFE38FC62F5CC98B11A532F229148FC7522B255D28AF71C29A58C6908EADB902281FE70A424C4002919450FC80B97EE1C32769E05D8AE84C1CDD8A0753F1B51917AF8550C04267AFD831CFF35D9FF676219F9DF6EBEF0B4921873FFB9BA69FD73A11580B75EAEDA246CD1112CB50282DF8A063F3C007CEB6A367B00E3C2B0509793737401700600ACE33C0D96191D4F95BC53730EE341E402100400020001010240530D110000008F071F0000000000000009000000")), serializerOptions);
                    
                    mctsClient.Enqueue(new(PacketDirection.Outgoing, hello));
                    
                    _ = HandleClient(mctsClient, client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }

    private async Task HandleClient(MctsClient mctsClient, TcpClient client)
    {
        const int maxChunkSize = 0x7FE;
        const int maxBufferSize = 0x802;
        const int maxHeaderSize = 4;
        const ushort ackPacketType = 0x0801;
        
        Console.WriteLine($"New client ({mctsClient.Id}, {client.Client.RemoteEndPoint})");
        
        try
        {
            await using var stream = client.GetStream();

            var packetData = new List<byte>();
            uint serverAck = 0;
            
            while (true)
            {
                if (stream.DataAvailable)
                {
                    var header = await ReadExactAsync(stream, maxHeaderSize);
                    if (header.Length < maxHeaderSize) break;
                
                    var size = BitConverter.ToUInt16(header, 0);
                    var packetType = BitConverter.ToUInt16([header[3], header[2]], 0);

                    if (packetType == ackPacketType)
                    {
                        var ack = new byte[8];
                        Buffer.BlockCopy(BitConverter.GetBytes(serverAck), 0, ack, 0, 4);
                        await stream.WriteAsync(ack);
                        serverAck++;
                    }

                    if (size == 0 && packetType is 0x0000 or 0x0001 or ackPacketType)
                    {
                        while (true)
                        {
                            var chunk = await ReadExactAsync(stream, maxChunkSize);
                            packetData.AddRange(chunk);

                            var nextHeader = await ReadExactAsync(stream, maxHeaderSize);
                            var nextSize = BitConverter.ToUInt16(nextHeader, 0);
                            var nextType = BitConverter.ToUInt16([nextHeader[3], nextHeader[2]], 0);

                            if (nextSize > 0)
                            {
                                var finalChunk = await ReadExactAsync(stream, nextSize);
                                packetData.AddRange(finalChunk);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var data = await ReadExactAsync(stream, size);
                        packetData.AddRange(data);
                    }
                    
                    var function = MarshalSerializer.DeserializeFunction(new MemoryStream(packetData.ToArray()), serializerOptions);
                    
                    mctsClient.Enqueue(new(PacketDirection.Incoming, function));
                }

                if (!mctsClient.TryDequeue(out var packet)) break;

                if (packet.Direction == PacketDirection.Outgoing)
                {
                    var memoryStream = new MemoryStream();
                    MarshalSerializer.SerializeFunction(memoryStream, packet.Function, serializerOptions);
                    
                    var data = memoryStream.ToArray();
                    
                    // encryption?

                    if (data.Length >= maxChunkSize)
                    {
                        int totalSent = 0;
                        while (totalSent < data.Length)
                        {
                            bool isLastChunk = (data.Length - totalSent) <= maxChunkSize;
                            int chunkSize = isLastChunk ? data.Length - totalSent : maxChunkSize;
                            
                            var header = new byte[4];
                            ushort sizeFlag = (ushort)(isLastChunk ? chunkSize : 0);
                            Buffer.BlockCopy(BitConverter.GetBytes(sizeFlag), 0, header, 0, 2);
                            header[3] = (byte)0x0;

                            var chunk = new byte[4 + chunkSize];
                            Buffer.BlockCopy(header, 0, chunk, 0, 4);
                            Buffer.BlockCopy(data, totalSent, chunk, 4, chunkSize);
                            
                            await stream.WriteAsync(chunk, 0, chunk.Length);
                            totalSent += chunkSize;
                        }
                    }
                    else
                    {
                        var header = new byte[4];
                        Buffer.BlockCopy(BitConverter.GetBytes((ushort)data.Length), 0, header, 0, 2);
                        header[3] = (byte)0x0;
                        
                        var fullPacket = new byte[4 + data.Length];
                        Buffer.BlockCopy(header, 0, fullPacket, 0, 4);
                        Buffer.BlockCopy(data, 0, fullPacket, 4, data.Length);
                        
                        await stream.WriteAsync(fullPacket, 0, fullPacket.Length);
                    }
                }
                else if (packet.Direction == PacketDirection.Incoming)
                {
                    Console.WriteLine($"Packet: {packet.Function.FunctionName}");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Client error: {e.Message}");
        }
    }
    
    private static async Task<byte[]> ReadExactAsync(NetworkStream stream, int count)
    {
        var buffer = new byte[count];
        var read = 0;
    
        while (read < count)
        {
            var remaining = count - read;
            var bytesRead = await stream.ReadAsync(buffer.AsMemory(read, remaining));
            if (bytesRead == 0) throw new EndOfStreamException();
            read += bytesRead;
        }
    
        return buffer;
    }
}