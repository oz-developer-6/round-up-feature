using System.Text.Json.Serialization;

namespace RoundUpFeature.Api.StarlingBankService.TransactionModels;

public class FeedItem
{
    [JsonPropertyName("feedItemUid")] 
    public string FeedItemUId { get; set; }

    [JsonPropertyName("categoryUid")]
    public string CategoryUId { get; set; }

    [JsonPropertyName("amount")]
    public Amount Amount { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; }
}
