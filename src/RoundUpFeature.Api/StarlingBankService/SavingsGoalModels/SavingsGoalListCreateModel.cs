using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;

public class SavingsGoalListCreateModel
{
    [JsonPropertyName("name")]
    [StringLength(255, MinimumLength = 1)]
    public string Name { get; set; }

    [JsonPropertyName("currency")]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; }

    [JsonPropertyName("target")]
    public Amount Target { get; set; }
}
