using Faker;
using RaceUpdateProcessor.Models.Builders;
using RacingUpdateProcessor.Models;
using RacingUpdateProcessor.Utils;
using System.Text.Json;
using Xunit;

namespace RacingUpdateProcessor.IntegrationTests.Generators;

public class HorseRaceUpdateProcessorTestDataGenerator
{
    public static TheoryData<string, string> GetValidHorseRaceUpdate()
    {
        var minimalHorseRaceUpdateTestData = GetMinimalHorseRaceUpdateTestData();
        var minimalHorseRaceUpdateWithMinimalRunnerTestData = GetMinimalHorseRaceUpdateWithMinimalRunnerTestData();
        var fullHorseRaceUpdateTestData = GetFullHorseRaceUpdateTestData();

        return new()
        {
            { minimalHorseRaceUpdateTestData.Item1, minimalHorseRaceUpdateTestData.Item2 },
            { minimalHorseRaceUpdateWithMinimalRunnerTestData.Item1, minimalHorseRaceUpdateWithMinimalRunnerTestData.Item2 },
            { fullHorseRaceUpdateTestData.Item1, fullHorseRaceUpdateTestData.Item2 }
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

    private static (string, string) GetMinimalHorseRaceUpdateTestData()
    {
        var raceId = RandomNumber.Next();

        var rawHorseRaceUpdate = new RawHorseRaceUpdateBuilder()
            .WithRaceId(raceId.ToString())
            .Build();

        var expectedHorseRaceUpdate = new HorseRaceUpdateBuilder()
            .WithRaceId(raceId)
            .Build();

        return (XmlUtils.ToXmlString(rawHorseRaceUpdate), JsonSerializer.Serialize(expectedHorseRaceUpdate));
    }

    private static (string, string) GetMinimalHorseRaceUpdateWithMinimalRunnerTestData()
    {
        var raceId = RandomNumber.Next();
        var runnerId = RandomNumber.Next();

        var rawHorseRaceUpdate = new RawHorseRaceUpdateBuilder()
            .WithRaceId(raceId.ToString())
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                .WithId(runnerId.ToString())
                .Build()
            })
            .Build();

        var expectedHorseRaceUpdate = new HorseRaceUpdateBuilder()
            .WithRaceId(raceId)
            .WithRunners(new Runner[]
            {
                new RunnerBuilder()
                .WithId(runnerId)
                .Build()
            })
            .Build();

        return (XmlUtils.ToXmlString(rawHorseRaceUpdate), JsonSerializer.Serialize(expectedHorseRaceUpdate));
    }


    private static (string, string) GetFullHorseRaceUpdateTestData()
    {
        var executionDateTimeOffset = DateTimeOffset.UtcNow;

        var meetingId = RandomNumber.Next();
        var raceId = RandomNumber.Next();
        var raceLocation = Address.City();
        var raceDistance = RandomNumber.Next();
        var raceNo = RandomNumber.Next();
        var raceType = Lorem.GetFirstWord();
        var raceInfo = Lorem.Sentence();
        var trackCondition = Lorem.GetFirstWord();
        var source = Lorem.GetFirstWord();
        var priceType = Lorem.GetFirstWord();
        var poolSize = RandomNumber.Next();
        var startTime = executionDateTimeOffset.ToUnixTimeSeconds();
        var creationTime = RandomNumber.Next(long.MaxValue);

        var runnerId = RandomNumber.Next();
        var runnerTabNo = RandomNumber.Next();
        var runnerBarrier = RandomNumber.Next();
        var runnerName = Name.FullName();
        var runnerPrice = decimal.Divide(RandomNumber.Next(), 100);
        var runnerJockey = Name.FullName();
        var runnerTrainer = Name.FullName();

        var rawHorseRaceUpdate = new RawHorseRaceUpdateBuilder()
            .WithMeetingId(meetingId.ToString())
            .WithRaceId(raceId.ToString())
            .WithRaceLocation(raceLocation)
            .WithDistance(raceDistance.ToString())
            .WithRaceNo(raceNo.ToString())
            .WithRaceType(raceType)
            .WithRaceInfo(raceInfo)
            .WithTrackCondition(trackCondition)
            .WithSource(source)
            .WithPriceType(priceType)
            .WithPoolSize(poolSize.ToString())
            .WithStartTime(startTime.ToString())
            .WithCreationTime(creationTime.ToString())
            .WithRunners(new RawRunner[]
            {
                new RawRunnerBuilder()
                .WithId(runnerId.ToString())
                .WithTabNo(runnerTabNo.ToString())
                .WithBarrier(runnerBarrier.ToString())
                .WithName(runnerName)
                .WithPrice(runnerPrice.ToString())
                .WithJockey(runnerJockey)
                .WithTrainer(runnerTrainer)
                .Build()
            })
            .Build();

        var expectedHorseRaceUpdate = new HorseRaceUpdateBuilder()
            .WithRaceId(raceId)
            .WithRaceLocation(raceLocation)
            .WithDistance(raceDistance)
            .WithRaceNumber(raceNo)
            .WithRaceType(raceType)
            .WithRaceInfo(raceInfo)
            .WithRaceCondition(trackCondition)
            .WithStartTimeUtc(DateTimeOffset.FromUnixTimeSeconds(startTime).LocalDateTime)
            .WithRunners(new Runner[]
            {
                new RunnerBuilder()
                .WithId(runnerId)
                .WithNumber(runnerTabNo)
                .WithBarrier(runnerBarrier)
                .WithName(runnerName)
                .WithWinPrice(runnerPrice)
                .WithJockey(runnerJockey)
                .WithTrainer(runnerTrainer)
                .Build()
            })
            .Build();

        return (XmlUtils.ToXmlString(rawHorseRaceUpdate), JsonSerializer.Serialize(expectedHorseRaceUpdate));
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
