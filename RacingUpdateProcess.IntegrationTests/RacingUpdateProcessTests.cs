using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RacingUpdateProcessor.Business;
using RacingUpdateProcessor.FileHandlers;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.IntegrationTests.Generators;
using RacingUpdateProcessor.Lambda;
using RacingUpdateProcessor.Mappers;
using RacingUpdateProcessor.Models;
using RacingUpdateProcessor.Notifications.Interfaces;
using RacingUpdateProcessor.Utils;
using Xunit;

namespace RacingUpdateProcess.IntegrationTests
{
    public class RacingUpdateProcessTests
    {
        private readonly Mock<ILogger<XmlFileImportProvider<RawHorseRaceUpdate>>> _xmlFileImportProviderLogger;
        private readonly Mock<ILogger<HorseRaceUpdateProcessor>> _horseRaceUpdateProcessorLogger;
        private readonly Mock<ILogger<Function>> _functionLogger;

        private readonly Mock<INotificationProvider> _notificationProvider;
        private readonly Mock<ITextFileProvider> _fileProviderMock;
        private readonly IConfiguration _configuration;

        private readonly ILambdaContext _context;

        public RacingUpdateProcessTests()
        {
            _xmlFileImportProviderLogger = new Mock<ILogger<XmlFileImportProvider<RawHorseRaceUpdate>>>();
            _horseRaceUpdateProcessorLogger = new Mock<ILogger<HorseRaceUpdateProcessor>>();
            _functionLogger = new Mock<ILogger<Function>>();
            _fileProviderMock = new Mock<ITextFileProvider>();
            _notificationProvider = new Mock<INotificationProvider>();

            _context = new TestLambdaContext
            {
                Logger = new TestLambdaLogger()
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
                {
                                new KeyValuePair<string, string?>("SourceFolder", "Source"),
                                new KeyValuePair<string, string?>("TargetFolder", "Target"),
                                new KeyValuePair<string, string?>("SchemaPath", "./HorseRaceUpdate.xsd"),
                })
                .Build();
        }

        [Theory]
        [MemberData(nameof(HorseRaceUpdateProcessorTestDataGenerator.GetValidHorseRaceUpdate), MemberType = typeof(HorseRaceUpdateProcessorTestDataGenerator))]
        public async Task Valid_HorseRaceUpdate_Should_Process_Successfully(string rawHorseRaceUpdateXml, string expectedHorseRaceUpdateJson)
        {
            _fileProviderMock.Setup(arg => arg.Download(It.IsAny<string>())).ReturnsAsync(rawHorseRaceUpdateXml);
            var function = SetupMocksAndDependencies();

            var snsEvent = GetSNSEvent();
            await function.FunctionHandler(snsEvent, _context);

            var expectedTargetPath = "Target\\foobar.json";

            _fileProviderMock.Verify(mock => mock.Upload(expectedTargetPath, expectedHorseRaceUpdateJson));
            _notificationProvider.Verify(mock => mock.Notify(expectedTargetPath));
        }

        [Theory]
        [MemberData(nameof(HorseRaceUpdateProcessorTestDataGenerator.GetHorseRaceUpdateWithInvalidFields), MemberType = typeof(HorseRaceUpdateProcessorTestDataGenerator))]
        public async Task Invalid_HorseRaceUpdate_Should_Cause_Exception(RawHorseRaceUpdate rawHorseRaceUpdate)
        {
            var rawHorseRaceUpdateXml = XmlUtils.ToXmlString(rawHorseRaceUpdate);

            _fileProviderMock.Setup(arg => arg.Download(It.IsAny<string>())).ReturnsAsync(rawHorseRaceUpdateXml);
            var function = SetupMocksAndDependencies();

            var snsEvent = GetSNSEvent();
            await Assert.ThrowsAsync<SourceFileValidationException>(async () => await function.FunctionHandler(snsEvent, _context));
        }

        private SNSEvent GetSNSEvent()
        {
            return new SNSEvent
            {
                Records = new List<SNSEvent.SNSRecord>
            {
                new SNSEvent.SNSRecord
                {
                    Sns = new SNSEvent.SNSMessage()
                    {
                        Message = "foobar"
                    }
                }
            }
            };
        }

        private Function SetupMocksAndDependencies()
        {
            var raceUpdateMapper = new HorseRaceUpdateMapper();
            var importProvider = new XmlFileImportProvider<RawHorseRaceUpdate>(_fileProviderMock.Object, _xmlFileImportProviderLogger.Object);
            var exportProvider = new JsonFileExportProvider<HorseRaceUpdate>(_fileProviderMock.Object);
            var raceUpdateFileHandler = new HorseRaceUpdateFileHandler(importProvider, exportProvider, _configuration);
            var raceUpdateProcessor = new HorseRaceUpdateProcessor(raceUpdateFileHandler, raceUpdateMapper, _notificationProvider.Object,
                _horseRaceUpdateProcessorLogger.Object);

            return new Function(raceUpdateProcessor, _functionLogger.Object, _configuration);
        }
    }
}
