using RaceUpdateProcessor.Utils;
using RacingUpdateProcessor.Mappers.Interfaces;
using RacingUpdateProcessor.Models;

namespace RacingUpdateProcessor.Mappers;

public class HorseRaceUpdateMapper : IRaceUpdateMapper<RawHorseRaceUpdate, HorseRaceUpdate>
{
    public HorseRaceUpdate Map(RawHorseRaceUpdate rawRaceUpdate)
    {
        return new HorseRaceUpdate
        {
            RaceId = Converter.ParseNullableInt(rawRaceUpdate.RaceId),
            RaceLocation = rawRaceUpdate.RaceLocation,
            Distance = Converter.ParseNullableInt(rawRaceUpdate.Distance),
            RaceNumber = Converter.ParseNullableInt(rawRaceUpdate.RaceNo),
            RaceType = rawRaceUpdate.RaceType,
            RaceInfo = rawRaceUpdate.RaceInfo,
            RaceCondition = rawRaceUpdate.TrackCondition,
            StartTimeUtc = Converter.ParseNullableUTCDateTime(rawRaceUpdate.StartTime),
            Runners = rawRaceUpdate.Runners?.Select(r => new Runner
            {
                Id = Converter.ParseNullableInt(r.Id),
                Number = Converter.ParseNullableInt(r.TabNo),
                Barrier = Converter.ParseNullableInt(r.Barrier),
                Name = r.Name,
                WinPrice = Converter.ParseNullableDecimal(r.Price),
                Jockey = r.Jockey,
                Trainer = r.Trainer
            }).ToArray()
        };
    }
}

