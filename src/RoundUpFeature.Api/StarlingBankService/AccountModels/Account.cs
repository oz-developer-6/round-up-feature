using System.Text.Json.Serialization;

namespace RoundUpFeature.Api.StarlingBankService.AccountModels;
public class Account
{
    [JsonPropertyName("accountUid")]
    public Guid AccountUId { get; set; }

    [JsonPropertyName("accountType")]
    public string AccountType { get; set; }
    
    [JsonPropertyName("defaultCategory")]
    public string DefaultCategory { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
