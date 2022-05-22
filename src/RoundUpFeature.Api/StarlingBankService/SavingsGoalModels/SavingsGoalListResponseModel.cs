using System.Text.Json.Serialization;

namespace RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;

public class SavingsGoalListResponseModel
{
    [JsonPropertyName("savingsGoalList")]
    public IEnumerable<SavingsGoal> SavingGoalLists { get; set; } = new List<SavingsGoal>();
}
