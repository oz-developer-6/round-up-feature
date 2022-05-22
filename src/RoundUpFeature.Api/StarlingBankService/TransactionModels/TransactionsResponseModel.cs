using System.Text.Json.Serialization;

namespace RoundUpFeature.Api.StarlingBankService.TransactionModels;

public class TransactionsResponseModel
{
    [JsonPropertyName("feedItems")]
    public IEnumerable<FeedItem> FeedItems { get; set; }
}