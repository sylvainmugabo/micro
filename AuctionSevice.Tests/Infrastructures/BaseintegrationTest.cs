using AuctionService.Data;
using AuctionService.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionSevice.Tests.Infrastructures;

public class BaseIntegrationTest : IClassFixture<AuctionWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ApplicationContext DbContext;
    protected readonly IAuctionService auctionService;
    protected BaseIntegrationTest(AuctionWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        auctionService = _scope.ServiceProvider.GetRequiredService<IAuctionService>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();

    }
}
