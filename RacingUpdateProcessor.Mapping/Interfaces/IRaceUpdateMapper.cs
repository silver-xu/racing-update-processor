using RacingUpdateProcessor.Models.Abstract;

namespace RacingUpdateProcessor.Mappers.Interfaces;

/// <summary>
/// Interface to provide mapping around RawRaceUpdate (3rd party structure) and RaceUpdate (internal structure)
/// The abstraction is around provide race type independent mapping therefore HorseRace and GreyhoundRace
/// Could be mapped differently.
/// </summary>
/// <typeparam name="TSource">Type of external Race Update Data</typeparam>
/// <typeparam name="TTarget">Type of internal Race Update Data</typeparam>
public interface IRaceUpdateMapper<TSource, TTarget>
    where TSource : BaseRawRaceUpdate
    where TTarget : BaseRaceUpdate
{
    public TTarget Map(TSource rawRaceUpdate);
}
