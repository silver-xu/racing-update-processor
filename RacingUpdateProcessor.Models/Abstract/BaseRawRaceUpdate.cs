namespace RacingUpdateProcessor.Models.Abstract;

/// <summary>
/// Super class of all external RaceUpdate class
/// </summary>
public abstract class BaseRawRaceUpdate
{
    public virtual string? RaceId { get; set; }
}

