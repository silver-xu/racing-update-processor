using RacingUpdateProcessor.FileHandlers.Interfaces;
using System.Text.Json;

namespace RacingUpdateProcessor.FileHandlers;

public class JsonFileExportProvider<T> : IExportProvider<T>
{
    private readonly ITextFileProvider _fileProvider;

    public JsonFileExportProvider(ITextFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task Export(string url, T data)
    {
        var jsonContent = JsonSerializer.Serialize(data);
        await _fileProvider.Upload(url, jsonContent);
    }
}
