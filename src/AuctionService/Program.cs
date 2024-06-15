

using AuctionService.Extensions.handler;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.ProductEndpoint();


app.Run();