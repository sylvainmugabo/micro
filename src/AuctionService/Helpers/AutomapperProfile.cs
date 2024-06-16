using AuctionService.Entities;
using AuctionService.Models;
using AutoMapper;

namespace AuctionService.Helpers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Auction, AuctionDto>();
        CreateMap<Auction, AuctionDto>().IncludeMembers(i => i.Item);
        CreateMap<Item, AuctionDto>();

    }

}
