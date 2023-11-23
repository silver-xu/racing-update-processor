using Microsoft.Extensions.Logging;
using Moq;
using RaceUpdateProcessor.FileHandlers.Tests.Generators;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.Models;
using RacingUpdateProcessor.Utils;
using Xunit;

namespace RacingUpdateProcessor.FileHandlers.Tests;

public class HorseRaceUpdateValidationTests
{
    private readonly Mock<ILogger<XmlFileImportProvider<RawHorseRaceUpdate>>> _loggerMock;
    private readonly Mock<ITextFileProvider> _fileProvider;

    private const string _schema = "HorseRaceUpdate.xsd";

    public HorseRaceUpdateValidationTests()
    {
        _loggerMock = new Mock<ILogger<XmlFileImportProvider<RawHorseRaceUpdate>>>();
        _fileProvider = new Mock<ITextFileProvider>();
    }


    [Theory]
    [MemberData(nameof(HorseRaceUpdateTestDataGenerator.GetValidHorseRaceUpdate), MemberType = typeof(HorseRaceUpdateTestDataGenerator))]
    public void ValidRawHorseRaceUpdateXml_Should_Deserialize_Successfully(RawHorseRaceUpdate rawHorseRaceUpdate)
    {
        var xmlString = XmlUtils.ToXmlString(rawHorseRaceUpdate);
        var actual = new XmlFileImportProvider<RawHorseRaceUpdate>(
            _fileProvider.Object, _loggerMock.Object).ImportFromString(_schema, xmlString);

        Assert.Equivalent(rawHorseRaceUpdate, actual);
    }

    [Theory]
    [MemberData(nameof(HorseRaceUpdateTestDataGenerator.GetHorseRaceUpdateWithInvalidFields), MemberType = typeof(HorseRaceUpdateTestDataGenerator))]
    public void RawHorseRaceUpdateXml_WithInvalidFields_Should_Throw_FileValidationException(RawHorseRaceUpdate rawHorseRaceUpdate)
    {
        var xmlString = XmlUtils.ToXmlString(rawHorseRaceUpdate);
        Assert.Throws<SourceFileValidationException>(
            () => new XmlFileImportProvider<RawHorseRaceUpdate>(
                _fileProvider.Object, _loggerMock.Object).ImportFromString(_schema, xmlString));
    }

}