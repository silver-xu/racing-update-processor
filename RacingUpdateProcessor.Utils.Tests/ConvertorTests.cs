using RaceUpdateProcessor.Utils;
using Xunit;

namespace RacingUpdateProcessor.Utils.Tests;

public class ConvertorTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("1", 1)]
    [InlineData("5", 5)]
    public void ParseNullableInt_Should_Convert_NullableString_To_Int(string? input, int? expectValue)
    {
        Assert.Equal(Converter.ParseNullableInt(input), expectValue);
    }

    public static TheoryData<string?, decimal?> DecimalTestData =>
        new()
        {
                { null, null },
                { "1.1", 1.1m },
                { "5.5", 5.5m }
        };

    [Theory]
    [MemberData(nameof(DecimalTestData))]
    public void ParseNullableDecimal_Should_Convert_NullableString_To_Decimal(string? input, decimal? expectValue)
    {
        Assert.Equal(Converter.ParseNullableDecimal(input), expectValue);
    }

    public static TheoryData<string?, DateTime?> DateTimeUTCTestData =>
         new()
         {
                { null, null },
                { "1700485200", new DateTime(2023, 11, 21, 0, 0, 0, DateTimeKind.Utc) },
                { "1700485199", new DateTime(2023, 11, 20, 23, 59, 59, DateTimeKind.Utc) }
         };

    [Theory]
    [MemberData(nameof(DateTimeUTCTestData))]
    public void ParseNullablDateTimeUtc_Should_Convert_NullableString_To_DateTime(string? input, DateTime? expectValue)
    {
        Assert.Equal(Converter.ParseNullableUTCDateTime(input), expectValue);
    }

}