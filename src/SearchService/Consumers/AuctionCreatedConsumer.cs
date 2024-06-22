using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;


namespace SearchService.Consumers;
public class AuctionCreatedConsumer(IMapper mapper) : IConsumer<AuctionCreated>
{
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {

        Console.WriteLine("------------------> Consume message", context.Message.Id);
        var item = mapper.Map<Item>(context.Message);

        await item.SaveAsync();

    }
}