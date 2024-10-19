using AuctionService.Data;
using AuctionService.Extensions.handler;
using AuctionService.Helpers.Filters;
using AuctionService.Repository;
using AuctionService.Service;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(cfg =>
{
    cfg.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddEntityFrameworkOutbox<ApplicationContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
    });
    cfg.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IAuctionService, AuctionClientService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpClient("auctions", (provider, client) =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");

    client.BaseAddress = new Uri("http://localhost:7001/api");

});


var app = builder.Build();

app.AuctionEndpoint();


app.Run();

public partial class Program { }