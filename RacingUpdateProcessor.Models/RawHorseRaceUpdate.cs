using RacingUpdateProcessor.Models.Abstract;
using System.Xml.Serialization;

namespace RacingUpdateProcessor.Models;

/// <summary>
/// External Horse Race Update
/// </summary>
[XmlRoot("RaceUpdate")]
public class RawHorseRaceUpdate : BaseRawRaceUpdate
{
    [XmlElement("MeetingID")]
    public string? MeetingId { get; set; }

    [XmlElement("RaceId")]
    public override string? RaceId { get; set; }

    [XmlElement("RaceLocation")]
    public string? RaceLocation { get; set; }

    [XmlElement("RaceDistance")]
    public string? Distance { get; set; }

    [XmlElement("RaceNo")]
    public string? RaceNo { get; set; }

    [XmlElement("RaceType")]
    public string? RaceType { get; set; }

    [XmlElement("RaceInfo")]
    public string? RaceInfo { get; set; }

    [XmlElement("TrackCondition")]
    public string? TrackCondition { get; set; }

    [XmlElement("Source")]
    public string? Source { get; set; }

    [XmlElement("PriceType")]
    public string? PriceType { get; set; }

    [XmlElement("PoolSize")]
    public string? PoolSize { get; set; }

    [XmlElement("StartTime")]
    public string? StartTime { get; set; }

    [XmlElement("CreationTime")]
    public string? CreationTime { get; set; }

    [XmlArray("Runners")]
    [XmlArrayItem("Runner")]
    public RawRunner[]? Runners { get; set; }
}

public class RawRunner
{
    [XmlAttribute("Id")]
    public string? Id { get; set; }

    [XmlAttribute("TabNo")]
    public string? TabNo { get; set; }

    [XmlAttribute("Barrier")]
    public string? Barrier { get; set; }

    [XmlAttribute("Name")]
    public string? Name { get; set; }

    [XmlAttribute("Price")]
    public string? Price { get; set; }

    [XmlAttribute("Jockey")]
    public string? Jockey { get; set; }

    [XmlAttribute("Trainer")]
    public string? Trainer { get; set; }
}
