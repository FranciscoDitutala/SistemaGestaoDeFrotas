using System.Reflection;

namespace Fleet.Data;

public static class FleetDataTools
{
    public static async Task<string> GetEmbeddedResourceStr<TSource>(string embeddedFileName) where TSource : class
    {
        var assembly = typeof(TSource).GetTypeInfo().Assembly;
        var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith(embeddedFileName, StringComparison.CurrentCultureIgnoreCase));

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream is null)
        {
            return string.Empty;
        }

        using StreamReader reader = new(stream);

        return await reader.ReadToEndAsync();
    }

    public static async Task<byte[]> GetEmbeddedResourceBytes<TSource>(string embeddedFileName) where TSource : class
    {
        var assembly = typeof(TSource).GetTypeInfo().Assembly;
        var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith($".{embeddedFileName}", StringComparison.CurrentCultureIgnoreCase));

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream is null)
        {
            return Array.Empty<byte>();
        }

        using var reader = new MemoryStream();
        await stream.CopyToAsync(reader);
        return reader.ToArray();

    }
}
