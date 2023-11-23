using Faker;
using RaceUpdateProcessor.Models.Builders;
using RacingUpdateProcessor.Models;
using Xunit;

namespace RaceUpdateProcessor.FileHandlers.Tests.Generators;

public class HorseRaceUpdateTestDataGenerator
{
    public static TheoryData<RawHorseRaceUpdate> GetValidHorseRaceUpdate()
    {
        return new()
        {
            GetFullHorseRaceUpdateTestData(),
            GetMinimalHorseRaceUpdateTestData(),
            GetMinimalHorseRaceUpdateAndMinimalRunnerTestData()
        };
    }

    public static TheoryData<RawHorseRaceUpdate> GetHorseRaceUpdateWithInvalidFields()
    {
        return new()
        {
            GetHorseRaceUpdateDataWithMissingRaceId(),
            GetHorseRaceUpdateDataWithMissingRunnerId(),
            GetHorseRaceUpdateDataWithInvalidMeetingId(),
            GetHorseRaceUpdateDataWithInvalidRaceId(),
            GetHorseRaceUpdateDataWithInvalidRaceNo(),
            GetHorseRaceUpdateDataWithInvalidPoolSize(),
            GetHorseRaceUpdateDataWithInvalidStartTime(),
            GetHorseRaceUpdateDataWithInvalidCreationTime(),
            GetHorseRaceUpdateDataWithInvalidRunnerId(),
            GetHorseRaceUpdateDataWithInvalidTabNo(),
            GetHorseRaceUpdateDataWithInvalidBarrier(),
            GetHorseRaceUpdateDataWithInvalidPrice()
        };
    }

    private static RawHorseRaceUpdate GetFullHorseRaceUpdateTestData()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .Build();
    }

    private static RawHorseRaceUpdate GetMinimalHorseRaceUpdateTestData()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithRaceId(RandomNumber.Next().ToString())
            .Build();
    }

    private static RawHorseRaceUpdate GetMinimalHorseRaceUpdateAndMinimalRunnerTestData()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithRaceId(RandomNumber.Next().ToString())
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                    .WithId(RandomNumber.Next().ToString())
                    .Build()
            })
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithMissingRaceId()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithoutRaceId()
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithMissingRunnerId()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                    .WithCompleteFakerData()
                    .WithoutId()
                    .Build()
            })
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidMeetingId()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithMeetingId("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidRaceId()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRaceId("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidRaceDistance()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithDistance("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidRaceNo()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRaceNo("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidPoolSize()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithPoolSize("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidStartTime()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithStartTime("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidCreationTime()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithCreationTime("foo")
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidRunnerId()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                    .WithCompleteFakerData()
                    .WithId("foo")
                    .Build()
            })
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidTabNo()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                    .WithCompleteFakerData()
                    .WithTabNo("foo")
                    .Build()
            })
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidBarrier()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                    .WithCompleteFakerData()
                    .WithBarrier("foo")
                    .Build()
            })
            .Build();
    }

    private static RawHorseRaceUpdate GetHorseRaceUpdateDataWithInvalidPrice()
    {
        return new RawHorseRaceUpdateBuilder()
            .WithCompleteFakerData()
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                    .WithCompleteFakerData()
                    .WithPrice("foo")
                    .Build()
            })
            .Build();
    }
}
