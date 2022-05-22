using System.Text.Json.Serialization;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;

public class SavingsGoal
{
    [JsonPropertyName("savingsGoalUid")]
    public string SavingsGoalUId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("target")]
    public Amount Target { get; set; }

    [JsonPropertyName("totalSaved")]
    public Amount TotalSaved { get; set; }

    [JsonPropertyName("savedPercentage")]
    public int SavedPercentage { get; set; }
}
