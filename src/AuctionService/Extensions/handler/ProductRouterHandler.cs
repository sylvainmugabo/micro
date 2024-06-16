using AuctionService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Extensions.handler;
public static class ProductExtension
{

    public static void ProductEndpoint(this IEndpointRouteBuilder app)
    {
        // app.MapGet("/api/auctions/{date:string?}", async (string? date, IAuctionRepository repository) =>
        // {
        //     return Results.Ok(await repository.GetAuctionsAsync(date));
        // });
        app.MapGet("/api/auctions/{date}", async (string date, [FromServices] IAuctionRepository repository) =>
                {
                    var aution = await repository.GetAuctionsAsync(date);

                    return Results.Ok(aution);

                });
        app.MapGet("/api/auctions/{id:Guid}", async (Guid id, [FromServices] IAuctionRepository repository) =>
        {
            var aution = await repository.GetAuctionByIdAsync(id);

            return Results.Ok(aution);

        });

    }
}