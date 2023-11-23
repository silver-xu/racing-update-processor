using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RacingUpdateProcessor.Business;
using RacingUpdateProcessor.FileHandlers;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.Lambda;
using RacingUpdateProcessor.Models;

namespace RacingUpdateProcess.IntegrationTests
{
    public class RacingUpdateProcessTests
    {
        private readonly Mock<ILogger<XmlFileImportProvider<RawHorseRaceUpdate>>> _xmlFileImportProviderLogger;


        private readonly Mock<ITextFileProvider> _fileProviderMock;
        private readonly IConfiguration _configuration;

        public RacingUpdateProcessTests()
        {
            _xmlFileImportProviderLogger = new Mock<ILogger<XmlFileImportProvider<RawHorseRaceUpdate>>>();
            _fileProviderMock = new Mock<ITextFileProvider>();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
                {
                                new KeyValuePair<string, string?>("SourceFolder", "Source"),
                                new KeyValuePair<string, string?>("TargetFolder", "Target"),
                                new KeyValuePair<string, string?>("SchemaPath", "./HorseRaceUpdate.xsd"),
                })
                .Build();
        }

        public async Task Valid_RawHorseRaceUpdate_Should_Process_Successfully()
        {
            var raceUpdateMapper = new RacingUpdateProcessor.Mappers.HorseRaceUpdateMapper<RawHorseRaceUpdate, HorseRaceUpdate>()
            var importProvider = new XmlFileImportProvider<RawHorseRaceUpdate>(_fileProviderMock.Object, _xmlFileImportProviderLogger.Object);
            var exportProvider = new JsonFileExportProvider<HorseRaceUpdate>(_fileProviderMock.Object);
            var raceUpdateFileHandler = new HorseRaceUpdateFileHandler(importProvider, exportProvider, _configuration);
            var raceUpdateProcessor = new HorseRaceUpdateProcessor(raceUpdateFileHandler,)

            var function = new Function(new HorseRaceUpdateProcessor()

        }
    }
}
