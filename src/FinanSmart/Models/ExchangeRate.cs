using System.Text.Json.Serialization;

namespace FinanSmart.Models;

public class ExchangeRate
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("bid")]
    public string Bid { get; set; } = string.Empty;

    [JsonPropertyName("ask")]
    public string Ask { get; set; } = string.Empty;

    [JsonPropertyName("create_date")]
    public string UpdatedAt { get; set; } = string.Empty;

    public decimal BidValue => decimal.TryParse(
        Bid,
        System.Globalization.NumberStyles.Any,
        System.Globalization.CultureInfo.InvariantCulture,
        out var v) ? v : 0;
}

public class AwesomeApiResponse
{
    [JsonPropertyName("USDBRL")]
    public ExchangeRate? UsdBrl { get; set; }

    [JsonPropertyName("EURBRL")]
    public ExchangeRate? EurBrl { get; set; }
}
