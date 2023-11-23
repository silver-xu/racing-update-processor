using Faker;
using RaceUpdateProcessor.Models.Builders;
using RacingUpdateProcessor.Models;
using Xunit;

namespace RaceUpdateProcessor.Mappers.Tests.Generators;

public class HorseRaceUpdateTestDataGenerator
{
    public static TheoryData<RawHorseRaceUpdate, HorseRaceUpdate> GetMinimalHorseRaceUpdateTestData()
    {
        var raceId = RandomNumber.Next();

        var rawHorseRaceUpdate = new RawHorseRaceUpdateBuilder()
            .WithRaceId(raceId.ToString())
            .Build();

        var expectedHorseRaceUpdate = new HorseRaceUpdateBuilder()
            .WithRaceId(raceId)
            .Build();

        return new()
        {
            { rawHorseRaceUpdate, expectedHorseRaceUpdate }
        };
    }

    public static TheoryData<RawHorseRaceUpdate, HorseRaceUpdate> GetMinimalHorseRaceUpdateWithMinimalRunnerTestData()
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

        return new()
        {
            { rawHorseRaceUpdate, expectedHorseRaceUpdate }
        };
    }


    public static TheoryData<RawHorseRaceUpdate, HorseRaceUpdate> GetFullHorseRaceUpdateTestData()
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

        return new()
        {
            { rawHorseRaceUpdate, expectedHorseRaceUpdate }
        };
    }
}
