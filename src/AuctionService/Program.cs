

using AuctionService.Data;
using AuctionService.Extensions.handler;
using AuctionService.Helpers;
using AuctionService.Helpers.Filters;
using AuctionService.Middleware;
using AuctionService.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(cfg =>
{
    cfg.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutomapperProfile>();
});

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});


var app = builder.Build();
//app.UseExceptionMiddleware();
app.ProductEndpoint();


app.Run();