using RaceUpdateProcessor.Mappers.Tests.Generators;
using RacingUpdateProcessor.Mappers;
using RacingUpdateProcessor.Models;
using Xunit;

namespace RaceUpdateProcessor.Mappers.Tests;

public class HorseRaceUpdateMapperTests
{
    [Theory]
    [MemberData(nameof(HorseRaceUpdateTestDataGenerator.GetMinimalHorseRaceUpdateTestData), MemberType = typeof(HorseRaceUpdateTestDataGenerator))]
    public void MinimalRawHorseRaceUpdate_Should_MapTo_MinimalHorseRaceUpdate(RawHorseRaceUpdate rawHorseRaceUpdate, HorseRaceUpdate expectedHorseRaceUpdate)
    {
        var actualHorseRaceUpdate = new HorseRaceUpdateMapper().Map(rawHorseRaceUpdate);
        Assert.Equivalent(expectedHorseRaceUpdate, actualHorseRaceUpdate);
    }

    [Theory]
    [MemberData(nameof(HorseRaceUpdateTestDataGenerator.GetMinimalHorseRaceUpdateWithMinimalRunnerTestData), MemberType = typeof(HorseRaceUpdateTestDataGenerator))]
    public void MinimalRawHorseRaceUpdateWithMinimalRunner_Should_MapTo_MinimalHorseRaceUpdateWithMinimalRunner(RawHorseRaceUpdate rawHorseRaceUpdate, HorseRaceUpdate expectedHorseRaceUpdate)
    {
        var actualHorseRaceUpdate = new HorseRaceUpdateMapper().Map(rawHorseRaceUpdate);
        Assert.Equivalent(expectedHorseRaceUpdate, actualHorseRaceUpdate);
    }

    [Theory]
    [MemberData(nameof(HorseRaceUpdateTestDataGenerator.GetFullHorseRaceUpdateTestData), MemberType = typeof(HorseRaceUpdateTestDataGenerator))]
    public void FullRawHorseRaceUpdate_Should_MapTo_FullHorseRaceUpdate(RawHorseRaceUpdate rawHorseRaceUpdate, HorseRaceUpdate expectedHorseRaceUpdate)
    {
        var actualHorseRaceUpdate = new HorseRaceUpdateMapper().Map(rawHorseRaceUpdate);
        Assert.Equivalent(expectedHorseRaceUpdate, actualHorseRaceUpdate);
    }
}