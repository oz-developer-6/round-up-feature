using RoundUpFeature.Api.StarlingBankService.AccountModels;
using RoundUpFeature.Api.StarlingBankService.SavingsGoalModels;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;

namespace RoundUpFeature.Api.StarlingBankService
{
    public interface IStarlingService
    {
        Task<AccountsResponseModel> GetAllAccountsAsync();
        Task<TransactionsResponseModel> GetAllTransactionsForGivenWeekAsync(string accountUid, string categoryUid, DateTime weekCommencingOn);
        Task<SavingsGoalListResponseModel> GetAllSavingsGoals(string accountUid);
        Task<SavingsGoal> CreateSavingsGoalAsync(string accountUid, SavingsGoalListCreateModel savingGoalList);
        Task<bool> AddMoneyToSavingsGoalAsync(string accountUid, string savingsGoalUid, SavingsGoalAddMoneyModel amountToSend);
    }
}
