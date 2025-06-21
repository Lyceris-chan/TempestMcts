using System.IO.Compression;

namespace Tempest.CLI;

public class ProjectCommands
{
    /// <summary>Creates a brand new UDK project for modding</summary>
    public void Create()
    {
        byte[] zipHeader = [0x50, 0x4B, 0x03, 0x04, 0x2D, 0x00, 0x00, 0x00, 0x08, 0x00];
        var udkPath = "C:\\Users\\Kyiro\\Downloads\\UDKInstall-2013-02-BETA.exe";

        using var file = File.OpenRead(udkPath);
        
        long headerIndex;

        while ((headerIndex = file.IndexOfBytes(zipHeader)) != -1)
        {
            file.Position = headerIndex;
            
            var zip = new ZipArchive(file, ZipArchiveMode.Read);

            foreach (var entry in zip.Entries)
            {
                Console.WriteLine(entry.FullName);
            }
        }
    }
}