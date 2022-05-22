using System.Text.Json.Serialization;

namespace RoundUpFeature.Api.StarlingBankService.TransactionModels;

public class Amount
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("minorUnits")]
    public decimal MinorUnits { get; set; }
}
