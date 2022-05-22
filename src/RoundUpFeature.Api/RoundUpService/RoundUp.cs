using RoundUpFeature.Api.StarlingBankService;
using RoundUpFeature.Api.StarlingBankService.AccountModels;
using RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.RoundUpService;

public class RoundUp : IRoundUp
{
    private readonly ILogger<IRoundUp> _logger;
    private readonly IStarlingService _starlingService;

    public RoundUp(ILogger<RoundUp> logger,
        IStarlingService apiClient)
    {
        _logger = logger;
        _starlingService = apiClient;
    }
    
    public async Task<IEnumerable<SavingsGoal>> GetAllSavingsGoalsAsync(string accountUid)
    {
        var result = await _starlingService.GetAllSavingsGoals(accountUid);
        return result.SavingGoalLists;
    }

    public async Task<SavingsGoal> CreateSavingGoalsAccountAsync(string accountUid, string name, decimal minorUnitsAmount)
    {
        var savingGoalList = new SavingsGoalListCreateModel
        {
            Name = name,
            Currency = "GBP",
            Target = new Amount
            {
                Currency = "GBP",
                MinorUnits = minorUnitsAmount
            }
        };

        var savingGoal = await _starlingService.CreateSavingsGoalAsync(accountUid, savingGoalList);
        return savingGoal;
    }

    public async Task<Account> TryGetPrimaryStarlingAccountAsync()
    {
        var accountsResponseModel = await _starlingService.GetAllAccountsAsync();
        
        var primaryAccount = accountsResponseModel.Accounts.FirstOrDefault(x => x.AccountType.ToLower() == "primary");

        return primaryAccount;
    }

    public async Task<IEnumerable<FeedItem>> TryGetAllOutTransactionsForWeekCommencingAsync(string accountUid, string defaultCategory, DateTime firstDayOfWeek)
    {
        var allTransactions = await _starlingService.GetAllTransactionsForGivenWeekAsync(accountUid, defaultCategory, firstDayOfWeek);

        var allOutTransactions = allTransactions.FeedItems.Where(x => x.Direction.ToLower() == "out");

        return allOutTransactions;
    }

    public decimal CalculateTheAmountToTransferToSavingGoals(IEnumerable<FeedItem> allOutTransactions)
    {
        decimal amountToTransferSavingGoal = 0m;
        foreach (var feedItem in allOutTransactions)
        {
            amountToTransferSavingGoal += DifferenceFromTheNearestPound(feedItem.Amount.MinorUnits);
        }
        
        var amountToTransferSavingGoalInMinorUnits = Math.Truncate(amountToTransferSavingGoal * 100);

        return amountToTransferSavingGoalInMinorUnits;
    }

    public async Task<bool> AddAmountToSavingGoalAsync(decimal amount, string accountUid, string savingsGoalUid)
    {
        var amountToSend = new Amount
        {
            Currency = "GBP",
            MinorUnits = amount
        };
        var savingGoalAddMoneyModel = new SavingsGoalAddMoneyModel
        {
            AmountToSend = amountToSend
        };
        
        var result = await _starlingService.AddMoneyToSavingsGoalAsync(accountUid, savingsGoalUid, savingGoalAddMoneyModel);
        
        return result;
    }

    public decimal DifferenceFromTheNearestPound(decimal minorUnits)
    {
        var minorUnitsInCurrencyFormat = minorUnits / 100;
        var nearestPound = Math.Round(minorUnitsInCurrencyFormat, MidpointRounding.ToPositiveInfinity);
        return nearestPound - minorUnitsInCurrencyFormat;
    }
}
