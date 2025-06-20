using System.IO.Compression;
using ConsoleAppFramework;
using Tempest.CLI;

var app = ConsoleApp.Create();

app.Add<Commands>();

app.Run(args);

public class Commands
{
    /// <summary>Extracts uncompressed function and field token data from a PE</summary>
    /// <param name="path">Path of the DLL or EXE</param>
    /// <param name="output">Output directory</param>
    public void ExtractTokens(string path, string output)
    {
        byte[] gzipHeader = [0x1F, 0x8B, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00];
        
        if (!File.Exists(path))
        {
            Console.Error.WriteLine("EXE/DLL was not found");
        }
        
        using var file = File.Open(path, FileMode.Open);

        long headerIndex;

        while ((headerIndex = file.IndexOfBytes(gzipHeader)) != -1)
        {
            file.Position = headerIndex;

            using var decompressionStream = new GZipStream(file, CompressionMode.Decompress, leaveOpen: true);
            using var outputStream = File.OpenWrite(Path.Join(output, headerIndex.ToString()));
            
            decompressionStream.CopyTo(outputStream);

            file.Position = headerIndex + 1;
        }
    }
}