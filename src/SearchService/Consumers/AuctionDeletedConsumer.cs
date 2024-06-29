using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers;

public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        Console.WriteLine("------------------> Consumer AuctionDeleted", context.Message.Id);
        var id = context.Message.Id;

        var result = await DB.DeleteAsync<Item>(id);

        if (!result.IsAcknowledged) throw new Exception($"{typeof(AuctionDeleted)} Problem deleting auction");

    }
}
