using Microsoft.AspNetCore.Mvc;
using RoundUpFeature.Api.RoundUpService;

namespace RoundUpFeature.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoundUpController : ControllerBase
{
    private readonly ILogger<RoundUpController> _logger;
    private readonly IRoundUp _roundUpService;

    public RoundUpController(
        ILogger<RoundUpController> logger,
        IRoundUp roundUpService)
    {
        _logger = logger;
        _roundUpService = roundUpService ?? throw new ArgumentNullException(nameof(roundUpService));
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync([FromQuery] string weekCommencingInput)
    {
        DateTime weekCommencing;
        if (!DateTime.TryParse(weekCommencingInput, out weekCommencing))
        {
            return BadRequest("Date is not valid! Valid formats are: YYYY/MM/DD or yyyy-MM-ddTHH:mm:ss.000Z");
        }

        _logger.LogInformation("Rounding up transactions");
        //var weekCommencing = DateTime.UtcNow.AddDays(-7);
        var primaryAccount = await _roundUpService.TryGetPrimaryStarlingAccountAsync();

        if (primaryAccount is null)
        {
            return BadRequest("There are not any Accounts");
        }

        var accountUid = primaryAccount.AccountUId.ToString();
        var allOutTransactions = await _roundUpService.TryGetAllOutTransactionsForWeekCommencingAsync(accountUid,
            primaryAccount.DefaultCategory, weekCommencing);

        var amountToTransferSavingGoalInMinorUnits = _roundUpService.CalculateTheAmountToTransferToSavingGoals(allOutTransactions);
        
        var allSavingGoalAccounts = await _roundUpService.GetAllSavingsGoalsAsync(accountUid);
        var savingsGoal = allSavingGoalAccounts.FirstOrDefault();
        if (savingsGoal is null)
        {
            _logger.LogInformation("There are not any Savings Goal Account, creating one");
            savingsGoal = await _roundUpService.CreateSavingGoalsAccountAsync(accountUid, "Holiday Savings", 18500);
        }

        var success = await _roundUpService.AddAmountToSavingGoalAsync(amountToTransferSavingGoalInMinorUnits, accountUid, savingsGoal.SavingsGoalUId);
        if (!success)
        {
            return Problem("Could not add the amount to savings goal account");
        }

        return Ok($"Amount MinorUnits:{amountToTransferSavingGoalInMinorUnits}, successfully added to SavingsGoal for AccountUid:{accountUid}");
    }
    
    [HttpGet("SavingsGoals")]
    public async Task<ActionResult> GetSavingsGoalsAsync([FromQuery] string accountUid)
    {
        var result = await _roundUpService.GetAllSavingsGoalsAsync(accountUid);
        return Ok(result);
    }
}
