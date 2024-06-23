using AuctionService.Data;
using AuctionService.Extensions.handler;
using AuctionService.Helpers.Filters;
using AuctionService.Repository;
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
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});


var app = builder.Build();
//app.UseExceptionMiddleware();
app.ProductEndpoint();


app.Run();