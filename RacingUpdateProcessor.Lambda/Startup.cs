using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RacingUpdateProcessor.Business;
using RacingUpdateProcessor.Business.Interfaces;
using RacingUpdateProcessor.FileHandlers;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.Mappers;
using RacingUpdateProcessor.Mappers.Interfaces;
using RacingUpdateProcessor.Models;
using RacingUpdateProcessor.Notifications;
using RacingUpdateProcessor.Notifications.Interfaces;

namespace RacingUpdateProcessor.Lambda;

[Amazon.Lambda.Annotations.LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
            {
                    new KeyValuePair<string, string?>("SourceFolder", "Source"),
                    new KeyValuePair<string, string?>("TargetFolder", "Target"),
                    new KeyValuePair<string, string?>("SchemaPath", "./HorseRaceUpdate.xsd"),
            })
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IRaceUpdateProcessor, HorseRaceUpdateProcessor>();
        services.AddSingleton<IRaceUpdateFileHandler<RawHorseRaceUpdate, HorseRaceUpdate>, HorseRaceUpdateFileHandler>();
        services.AddSingleton<ITextFileProvider, LocalTextFileProvider>();
        services.AddSingleton(typeof(IImportProvider<>), typeof(XmlFileImportProvider<>));
        services.AddSingleton(typeof(IExportProvider<>), typeof(JsonFileExportProvider<>));
        services.AddSingleton<IExportFileNameProvider, JsonExportFileNameProvider>();
        services.AddSingleton<IRaceUpdateMapper<RawHorseRaceUpdate, HorseRaceUpdate>, HorseRaceUpdateMapper>();
        services.AddSingleton<INotificationProvider, LogNotificationProvider>();

        services.AddLogging(builder => builder.AddConsole());
    }
}

