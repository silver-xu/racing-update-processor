
using Microsoft.Extensions.Configuration;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.Models;

namespace RacingUpdateProcessor.FileHandlers;

public class HorseRaceUpdateFileHandler : IRaceUpdateFileHandler<RawHorseRaceUpdate, HorseRaceUpdate>
{
    private readonly IImportProvider<RawHorseRaceUpdate> _importProvider;
    private readonly IExportProvider<HorseRaceUpdate> _exportProvider;

    private readonly IConfiguration _configuration;

    public HorseRaceUpdateFileHandler(IImportProvider<RawHorseRaceUpdate> importProvider,
        IExportProvider<HorseRaceUpdate> exportProvider,
        IConfiguration configuration)
    {
        _importProvider = importProvider;
        _exportProvider = exportProvider;
        _configuration = configuration;
    }

    public async Task<RawHorseRaceUpdate> Import(string url)
    {
        var schemaPath = _configuration["SchemaPath"];
        if (schemaPath is null)
        {
            throw new ArgumentException("SchemaPath has not been set in Startup.cs");
        }

        return await _importProvider.Import(schemaPath, url);
    }

    public async Task Export(HorseRaceUpdate horseRaceUpdate, string url)
    {
        await _exportProvider.Export(url, horseRaceUpdate);
    }

}
