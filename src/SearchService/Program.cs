using MassTransit;
using SearchService.Consumers;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit((config) =>
{
    config.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    config.UsingRabbitMq((context, cfg) =>
    {

        cfg.ConfigureEndpoints(context);

    });
});

await DbInitializer.InitDb(builder);

var app = builder.Build();

app.Run();