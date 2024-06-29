using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer(IMapper mapper) : IConsumer<AuctionUpdated>
{
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        var message = context.Message;
        var item = mapper.Map<Item>(message);
        var result = await DB.Update<Item>().Match(i => i.ID == message.Id.ToString())
            .ModifyOnly(x => new
            {
                x.Color,
                x.Make,
                x.Model,
                x.Year,
                x.Mileage
            }, item)
            .ExecuteAsync(); ;

        if (!result.IsAcknowledged)
            throw new Exception($"{typeof(AuctionUpdated)} Problem updating mongodb");
    }
}
