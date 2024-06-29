using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.Helpers.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace AuctionService.Repository;

public class AuctionRepository(ApplicationContext context, IMapper mapper) : IAuctionRepository
{
    public void AddAuction(Auction auction)
    {
        context.Auctions.Add(auction);
    }

    public async Task<AuctionDto> GetAuctionByIdAsync(Guid id)
    {
        var auction = await context.Auctions
        .ProjectTo<AuctionDto>(mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(a => a.Id == id) ?? throw new AuctionIdNotFoundException(id);
        return auction;
    }

    public async Task<Auction> GetAuctionEntityById(Guid id)
    {
        return await context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new AuctionIdNotFoundException(id);
    }

    public async Task<List<AuctionDto>> GetAuctionsAsync(string date)
    {
        var query = context.Auctions.OrderBy(o => o.Item.Make).AsQueryable();
        if (!string.IsNullOrEmpty(date))
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);

        return await query.ProjectTo<AuctionDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public void RemoveAuction(Auction auction)
    {
        context.Auctions.Remove(auction);
    }

    public async Task<bool> UpdateAuction(Auction auction)
    {
        context.Update(auction);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}

public interface IAuctionRepository
{
    Task<List<AuctionDto>> GetAuctionsAsync(string date);
    Task<AuctionDto> GetAuctionByIdAsync(Guid id);
    Task<Auction> GetAuctionEntityById(Guid id);
    void AddAuction(Auction auction);
    void RemoveAuction(Auction auction);
    Task<bool> SaveChangesAsync();
    Task<bool> UpdateAuction(Auction auction);
}
