using RacingUpdateProcessor.Models.Abstract;

namespace RacingUpdateProcessor.Models;

/// <summary>
/// Internal Horse Race Update
/// </summary>
public class HorseRaceUpdate : BaseRaceUpdate
{
    public string? RaceLocation { get; set; }

    public int? Distance { get; set; }

    public int? RaceNumber { get; set; }

    public string? RaceType { get; set; }

    public string? RaceInfo { get; set; }

    public string? RaceCondition { get; set; }

    public DateTime? StartTimeUtc { get; set; }

    public Runner[]? Runners { get; set; }
}

public class Runner
{
    public int? Id { get; set; }

    public int? Number { get; set; }

    public int? Barrier { get; set; }

    public string? Name { get; set; }

    public decimal? WinPrice { get; set; }

    public string? Jockey { get; set; }

    public string? Trainer { get; set; }
}
