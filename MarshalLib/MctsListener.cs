using System.Net;
using System.Net.Sockets;

namespace MarshalLib;

public class MctsListener(IPAddress localaddr, int port)
{
    private const int MaxChunkSize = 0x7FE;
    
    private readonly TcpListener _listener = new(localaddr, port);

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
                    _ = HandleClient(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }

    private async Task HandleClient(TcpClient client)
    {
        try
        {
            await using var stream = client.GetStream();
            using var reader = new StreamReader(stream);
            
            var buffer = new byte[MaxChunkSize];
            
            while (true)
            {
                var bytesRead = await stream.ReadAsync(buffer);
                if (bytesRead == 0) break;
                
                var binaryReader = new BinaryReader(new MemoryStream(buffer));
                
                var size = binaryReader.ReadUInt16();
                var packetType = binaryReader.ReadUInt16BigEndian();
                
                // ts pmo icl </3
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Client error: {e.Message}");
        }
    }
}