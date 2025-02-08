using System.Reflection;

namespace MctsDetective;

public static class ResourceLoader
{
    public static Image LoadEmbeddedImage(string resourceName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        
        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) throw new Exception($"Resource not found: {resourceName}");
        return Image.FromStream(stream);
    }
}