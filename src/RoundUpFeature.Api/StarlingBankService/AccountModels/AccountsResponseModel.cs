using System.Text.Json.Serialization;

namespace RoundUpFeature.Api.StarlingBankService.AccountModels;

public class AccountsResponseModel
{
    [JsonPropertyName("accounts")] 
    public IEnumerable<Account> Accounts { get; set; } = new List<Account>();
}
