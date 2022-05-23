using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;
using RoundUpFeature.Api.StarlingBankService.TransactionModels;
using Xunit;

namespace RoundUpFeature.Api.Test.RoundUpService.Test;

public class RoundUpTests
{
    [Theory]
    [InlineData(1240, 0.6)]
    [InlineData(435, 0.65)]
    [InlineData(520, 0.80)]
    [InlineData(87, 0.13)]
    [InlineData(1, 0.99)]
    public void DecimalValue_ShouldBe_RoundedUp_To_NearestPound(decimal minorUnits, decimal expectedRoundUp)
    {
        // Arrange
        var roundUpService = new RoundUp(null, null);

        // Act
        var result = roundUpService.DifferenceFromTheNearestPound(minorUnits);
        
        // Assert
        result.Should().Be(expectedRoundUp);
    }

    [Fact]
    public void RoundUpAmounts_Should_Be_Calculated_Correctly()
    {
        var roundUpService = new RoundUp(null, null);

        var transactions = GenerateFeedItems();

        var result = roundUpService.CalculateTheAmountToTransferToSavingGoals(transactions);

        result.Should().Be(158);
    }

    [Fact]
    public async Task Should_Only_Calculate_OUT_Payments()
    {
        // Arrange
        var starlingService = new Mock<IStarlingService>();
        var transactionsResponseModel = new TransactionsResponseModel
        {
            FeedItems = GenerateFeedItems(true)
        };
        starlingService.Setup(x =>
                x.GetAllTransactionsForGivenWeekAsync(null, null, It.IsAny<DateTime>()))
            .ReturnsAsync(transactionsResponseModel);
        
        var roundUpService = new RoundUp(null, starlingService.Object);

        // Act
        var allOutTransactions = await roundUpService.TryGetAllOutTransactionsForWeekCommencingAsync(null, null, DateTime.UtcNow.AddDays(-7));

        // Assert
        allOutTransactions.Count().Should().Be(3);
    }

    private static IEnumerable<FeedItem> GenerateFeedItems(bool withDirectionIn = false)
    {
        var transactions = new List<FeedItem>();
        if (withDirectionIn)
        {
            transactions.Add(new FeedItem
            {
                Amount = new Amount
                {
                    MinorUnits = 435
                },
                Direction = "IN"
            });
        }
        
        transactions.Add(new FeedItem
        {
            Amount = new Amount
            {
                MinorUnits = 435
            },
            Direction = "OUT"
        });
        transactions.Add(new FeedItem
        {
            Amount = new Amount
            {
                MinorUnits = 520
            },
            Direction = "OUT"
        });
        transactions.Add(new FeedItem
        {
            Amount = new Amount
            {
                MinorUnits = 87
            },
            Direction = "OUT"
        });
        return transactions;
    }
}