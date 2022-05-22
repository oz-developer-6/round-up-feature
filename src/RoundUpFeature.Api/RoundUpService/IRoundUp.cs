using RoundUpFeature.Api.StarlingBankService.AccountModels;
using RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.RoundUpService;

public interface IRoundUp
{
    Task<IEnumerable<SavingsGoal>> GetAllSavingsGoalsAsync(string accountUid);
    Task<SavingsGoal> CreateSavingGoalsAccountAsync(string accountUid, string name, decimal minorUnitsAmount);
    Task<Account> TryGetPrimaryStarlingAccountAsync();

    Task<IEnumerable<FeedItem>> TryGetAllOutTransactionsForWeekCommencingAsync(string accountUid,
        string defaultCategory, DateTime firstDayOfWeek);

    decimal CalculateTheAmountToTransferToSavingGoals(IEnumerable<FeedItem> allOutTransactions);
    Task<bool> AddAmountToSavingGoalAsync(decimal amount, string accountUid, string savingsGoalUid);
    decimal DifferenceFromTheNearestPound(decimal minorUnits);
}