using System.Text.Json.Serialization;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;

public class SavingsGoalAddMoneyModel
{
    [JsonPropertyName("amount")]
    public Amount AmountToSend { get; set; }
}