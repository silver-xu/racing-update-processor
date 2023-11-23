using Microsoft.Extensions.Logging;
using RacingUpdateProcessor.Business.Interfaces;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.Mappers.Interfaces;
using RacingUpdateProcessor.Models;
using RacingUpdateProcessor.Notifications.Interfaces;

namespace RacingUpdateProcessor.Business;

/// <summary>
/// The Main class to import, process and export Horse Racing Update Files
/// </summary>
public class HorseRaceUpdateProcessor : IRaceUpdateProcessor
{
    private readonly IRaceUpdateFileHandler<RawHorseRaceUpdate, HorseRaceUpdate> _raceUpdateFileHandler;
    private readonly IRaceUpdateMapper<RawHorseRaceUpdate, HorseRaceUpdate> _raceUpdateMapper;
    private readonly INotificationProvider _notificationProvider;
    private readonly ILogger<HorseRaceUpdateProcessor> _logger;

    public HorseRaceUpdateProcessor(IRaceUpdateFileHandler<RawHorseRaceUpdate, HorseRaceUpdate> raceUpdateFileHandler,
        IRaceUpdateMapper<RawHorseRaceUpdate, HorseRaceUpdate> raceUpdateMapper,
        INotificationProvider notificationProvider,
        ILogger<HorseRaceUpdateProcessor> logger)
    {
        _raceUpdateFileHandler = raceUpdateFileHandler;
        _raceUpdateMapper = raceUpdateMapper;
        _notificationProvider = notificationProvider;

        _logger = logger;
    }

    /// <summary>
    /// Process a raw horse racing update file from a url and export to a target url
    /// </summary>
    /// <param name="sourceUrl">source file url</param>
    /// <param name="targetUrl">target file url</param>
    /// <returns></returns>
    public async Task Process(string sourceUrl, string targetUrl)
    {
        try
        {
            _logger.LogInformation($"Information: HorseRaceUpdate File ${sourceUrl} has begun to process");
            var rawHorseRaceUpdate = await _raceUpdateFileHandler.Import(sourceUrl);

            _logger.LogInformation($"Information: HorseRaceUpdate File ${sourceUrl} was successfully imported");
            var horseRaceUpdate = _raceUpdateMapper.Map(rawHorseRaceUpdate);

            await _raceUpdateFileHandler.Export(horseRaceUpdate, targetUrl);
            _logger.LogInformation($"Information: HorseRaceUpdate File ${sourceUrl} was successfully exported to ${targetUrl}");

            await _notificationProvider.Notify(targetUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: Error occured while processing ${sourceUrl}: ${ex.Message}");
            throw;
        }
    }
}
