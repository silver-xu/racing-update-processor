namespace RaceUpdateProcessor.Utils;

public static class Converter
{
    /// <summary>
    /// Syntax sugar method to Parse a nullable int
    /// </summary>
    /// <param name="input">Nullable string to parse</param>
    /// <returns>Null if input is null, otherwise parsed int</returns>
    public static int? ParseNullableInt(string? input)
    {
        return input is null ? null : int.Parse(input);
    }

    /// <summary>
    /// Syntax sugar method to Parse a nullable decimal
    /// </summary>
    /// <param name="input">Nullable string to parse</param>
    /// <returns>Null if input is null, otherwise parsed decimal</returns>
    public static decimal? ParseNullableDecimal(string? input)
    {
        return input is null ? null : decimal.Parse(input);
    }

    /// <summary>
    /// Syntax sugar method to Parse a nullable unix timestamp
    /// </summary>
    /// <param name="unixTimestamp">Nullable string to parse</param>
    /// <returns>UTC Datetime</returns>
    public static DateTime? ParseNullableUTCDateTime(string? unixTimestamp)
    {
        return unixTimestamp is null ? null : DateTimeOffset.FromUnixTimeSeconds(long.Parse(unixTimestamp)).LocalDateTime;
    }
}
