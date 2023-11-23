using RacingUpdateProcessor.FileHandlers.Interfaces;

namespace RacingUpdateProcessor.FileHandlers;

public class JsonExportFileNameProvider : IExportFileNameProvider
{
    public string GetExportFileName(string importFileName)
    {
        return Path.ChangeExtension(importFileName, "json");
    }
}
