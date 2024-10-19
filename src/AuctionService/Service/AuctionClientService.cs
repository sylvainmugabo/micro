using AuctionService.DTOs;

namespace AuctionService.Service;

public class AuctionClientService(IHttpClientFactory factory) : IAuctionService
{
    public async Task<List<AuctionDto>> GetAuctions()
    {
        using var client = factory.CreateClient("auctions");
        var result = await client.GetFromJsonAsync<List<AuctionDto>>("http://localhost:7001/api/auctions/2022-12-12");
        return result;
    }
}

public interface IAuctionService
{
    Task<List<AuctionDto>> GetAuctions();
}
