using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;
public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Item> Items { get; set; }
}