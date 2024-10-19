using AuctionSevice.Tests.Infrastructures;

namespace AuctionSevice.Tests;

public class AuctionTests(AuctionWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    private readonly AuctionWebApplicationFactory _factory = factory;

    [Fact]
    public async Task GetAuction_By_Date()
    {
        // Act
        var auctions = await auctionService.GetAuctions();

        // Assert

        Assert.NotNull(auctions);
    }
}
