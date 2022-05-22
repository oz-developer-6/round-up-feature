using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using RoundUpFeature.Api.StarlingBankService.AccountModels;
using RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.StarlingBankService;

public class StarlingService : IStarlingService
{
    private readonly ILogger<StarlingService> _logger;
    private readonly HttpClient _httpClient;

    public StarlingService(
        ILogger<StarlingService> logger,
        HttpClient httpClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<AccountsResponseModel> GetAllAccountsAsync()
    {
        _logger.LogInformation("Getting All Accounts");
        var httpResponseMessage = await _httpClient.GetAsync("api/v2/accounts");

        if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Could not get Accounts! Received:{httpResponseMessage}");
            return new AccountsResponseModel();
        }

        var contentAsStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<AccountsResponseModel>(contentAsStream);

        return result;
    }

    public async Task<TransactionsResponseModel> GetAllTransactionsForGivenWeekAsync(string accountUid, string categoryUid, DateTime weekCommencingOn)
    {
        _logger.LogInformation($"Getting All Transactions for the week commencing on {weekCommencingOn.ToShortDateString()}");
        
        var queryStringParameters = new Dictionary<string, string>
        {
            ["minTransactionTimestamp"] = weekCommencingOn.ToString(Helpers.TimeFormats.UtcFormat),
            ["maxTransactionTimestamp"] = weekCommencingOn.AddDays(7).ToString(Helpers.TimeFormats.UtcFormat)
        };
        var requestUri = QueryHelpers.AddQueryString($"api/v2/feed/account/{accountUid}/category/{categoryUid}/transactions-between", queryStringParameters);
        var httpResponseMessage = await _httpClient.GetAsync(requestUri);
        var contentAsStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<TransactionsResponseModel>(contentAsStream);
        return result;
    }

    public async Task<SavingsGoalListResponseModel> GetAllSavingsGoals(string accountUid)
    {
        _logger.LogInformation("Getting all of the saving spaces if there are any");
        var httpResponseMessage = await _httpClient.GetAsync($"/api/v2/account/{accountUid}/savings-goals");
        var contentAsStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<SavingsGoalListResponseModel>(contentAsStream);

        return result;
    }

    public async Task<SavingsGoal> CreateSavingsGoalAsync(string accountUid, SavingsGoalListCreateModel savingGoalList)
    {
        var json = JsonSerializer.Serialize(savingGoalList);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var requestUri = $"/api/v2/account/{accountUid}/savings-goals";
        var httpResponseMessage = await _httpClient.PutAsync(requestUri, httpContent);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        var contentAsStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<SavingsGoal>(contentAsStream);

        return result;
    }

    public async Task<bool> AddMoneyToSavingsGoalAsync(string accountUid, string savingsGoalUid, SavingsGoalAddMoneyModel amountToSend)
    {
        var transferUid = Guid.NewGuid();
        var requestUri = $"/api/v2/account/{accountUid}/savings-goals/{savingsGoalUid}/add-money/{transferUid.ToString()}";
        var json = JsonSerializer.Serialize(amountToSend);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponseMessage = await _httpClient.PutAsync(requestUri, httpContent);

        return httpResponseMessage.IsSuccessStatusCode;
    }
}
