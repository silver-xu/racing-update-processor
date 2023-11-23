namespace RacingUpdateProcessor.Models.Abstract;

/// <summary>
/// Super class of all internal RaceUpdates class
/// </summary>
public abstract class BaseRaceUpdate
{
    public virtual int? RaceId { get; set; }
}
