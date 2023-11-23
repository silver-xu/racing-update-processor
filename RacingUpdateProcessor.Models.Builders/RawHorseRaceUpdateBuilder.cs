using BuilderGenerator;
using Faker;
using RacingUpdateProcessor.Models;

namespace RaceUpdateProcessor.Models.Builders;

[BuilderFor(typeof(RawHorseRaceUpdate))]

public partial class RawHorseRaceUpdateBuilder
{
    public RawHorseRaceUpdateBuilder WithCompleteFakerData()
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

        return this
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
                .WithCompleteFakerData()
                .Build()
            });
    }
}
