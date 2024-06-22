
using Contracts;
using SearchService.Entities;
using AutoMapper;
namespace SearchService.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<AuctionCreated, Item>();
    }

}
